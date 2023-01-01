using BHS.Domain.Enumerate;

namespace BHS.Infrastructure;

public static class StoredProcedures
{
    public static string GiftExchange { get; } =
        $@"CREATE PROCEDURE GiftExchange @userId nvarchar(max), @giftId int, @quantity int
AS
    BEGIN TRAN
BEGIN TRY
    SET NOCOUNT ON;

    DECLARE @rowUpdated int = 0, @pointOfGift int = 0, @totalPoints int = 0, @giftType int = 0, @giftQuantity int = 0,
        @fromDateOfExchange int = 0, @sourceId int = 0, @vendorId int = 0, @expirationDate datetime2, @loyaltyProgramId int = 0;

    SELECT g.*, l.*
    FROM GiftOfLoyalty g
             INNER JOIN LoyaltyProgram l on l.Id = g.LoyaltyProgramId
    WHERE QtyAvailable > 0
      AND g.Id = @giftId;

    SELECT @pointOfGift = Point,
           @giftType = g.Type,
           @giftQuantity = Quantity,
           @vendorId = l.VendorId,
           @fromDateOfExchange = g.FromDateOfExchange,
           @loyaltyProgramId = l.Id,
           @sourceId = g.SourceId
    FROM GiftOfLoyalty g
             INNER JOIN LoyaltyProgram l on l.Id = g.LoyaltyProgramId
    WHERE QtyAvailable > 0
      AND g.Id = @giftId;

    SET @totalPoints = @pointOfGift * @quantity;
    SET @expirationDate = DATEADD(day, @fromDateOfExchange, GETDATE());

    UPDATE GiftOfLoyalty SET QtyAvailable = QtyAvailable - 1 WHERE QtyAvailable > 0 AND Id = @giftId
    SELECT @rowUpdated = @@ROWCOUNT
    IF @rowUpdated > 0
        BEGIN
            IF @giftType = {GiftType.RotationLuck}
                BEGIN
                    DECLARE @totalTurns int = @giftQuantity * @quantity;

                    DECLARE @fortuneId int = 0;
                    SELECT @fortuneId = f.Id, @expirationDate = f.ToDate
                    FROM Fortune f
                             INNER JOIN FortuneTurnOfUser fu on f.Id = fu.FortuneId
                    WHERE f.Id = @sourceId
                      AND fu.UserId = @userId;
                    IF @fortuneId = 0
                        BEGIN
                            INSERT INTO FortuneTurnOfUser(UserId, FortuneId, TurnTotal, TurnAvailable, CreatedAt)
                            VALUES (@userId, @sourceId, @totalTurns, @totalTurns, GETUTCDATE());
                        END
                    ELSE
                        BEGIN
                            UPDATE FortuneTurnOfUser
                            SET TurnAvailable = TurnAvailable + @totalTurns,
                                TurnTotal     = TurnTotal + @totalTurns
                            WHERE UserId = @userId
                              AND FortuneId = @fortuneId;
                        END
                END
            DECLARE @i int = 0
            WHILE @i < @quantity
                BEGIN
                    INSERT INTO GiftOfUser(UserId, GiftOfLoyaltyId, VendorId, ExpirationDate, IsUsed, CreatedAt)
                    VALUES (@userId, @giftId, @vendorId, @expirationDate, 0, GETUTCDATE());
                    SET @i = @i + 1;
                END
            INSERT INTO PointOfUser(VendorId, UserId, Point, Type, ProgramType, SourceId, SourceDetailId, CreatedAt)
            VALUES (@vendorId, @userId, -@totalPoints, {PointOfUserType.Deducted}, {PointOfUserType.GiftExchange}, @loyaltyProgramId, @giftId, GETUTCDATE());
            DECLARE @programId int, @programExpirationDate datetime2, @programType int, @totalPointsOfProgram int;
            IF (SELECT COUNT(*)
                FROM (SELECT SourceId, ProgramType
                      FROM PointOfUser p LEFT JOIN Fortune f on (p.ProgramType = {PointOfUserType.RotationLuck} AND p.SourceId = f.Id)
                            LEFT JOIN LoyaltyProgram l on (p.ProgramType IN ({PointOfUserType.Qr}, {PointOfUserType.Order}) AND p.SourceId = l.Id AND l.ExpirationDate > GETUTCDATE())
                      WHERE UserId = @userId
                        AND p.VendorId = @vendorId
                        AND ProgramType <> {PointOfUserType.GiftExchange}
                      GROUP BY SourceId, ProgramType
                      HAVING SUM(Point) > 0) AS PointsOfUser) = 0
                BEGIN
                    RAISERROR ('NotEnoughPoint', 16, 1);
                END
            DECLARE cursorPointOfUser CURSOR FOR SELECT SourceId, ProgramType, SUM(Point) as TotalPoints
                                                 FROM PointOfUser p
                                                          LEFT JOIN Fortune f on (p.ProgramType = {PointOfUserType.RotationLuck} AND p.SourceId = f.Id)
                                                          LEFT JOIN LoyaltyProgram l
                                                                    on (p.ProgramType IN ({PointOfUserType.Qr}, {PointOfUserType.Order}) AND
                                                                        p.SourceId = l.Id AND
                                                                        l.ExpirationDate > GETUTCDATE())
                                                 WHERE UserId = @userId
                                                   AND p.VendorId = @vendorId
                                                   AND ProgramType <> {PointOfUserType.GiftExchange}
                                                 GROUP BY SourceId, ProgramType, l.ExpirationDate
                                                 HAVING SUM(Point) > 0
                                                 ORDER BY IIF(ProgramType = {PointOfUserType.RotationLuck}, 1, 2);
            OPEN cursorPointOfUser;
            FETCH NEXT FROM cursorPointOfUser
                INTO @programId, @programType, @totalPointsOfProgram;
            WHILE @@FETCH_STATUS = 0
                BEGIN
                    IF @totalPointsOfProgram >= @totalPoints
                        BEGIN
                            INSERT INTO PointOfUser(VendorId, UserId, Point, Type, ProgramType,
                                                    SourceId, CreatedAt)
                            VALUES (@vendorId, @userId, -@totalPoints, {PointOfUserType.Deducted}, @programType,
                                    @programId, GETUTCDATE());
                            BREAK;
                        END
                    SET @totalPoints = @totalPoints - @totalPointsOfProgram;
                    INSERT INTO PointOfUser(VendorId, UserId, Point, Type, ProgramType, SourceId,
                                            CreatedAt)
                    VALUES (@vendorId, @userId, -@totalPointsOfProgram, {PointOfUserType.Deducted}, @programType,
                            @programId, GETUTCDATE());
                    FETCH NEXT FROM cursorPointOfUser
                        INTO @programId, @programExpirationDate, @programType, @totalPointsOfProgram;
                END
            CLOSE cursorPointOfUser;
            DEALLOCATE cursorPointOfUser;
            COMMIT TRAN;
            RETURN;
        END
    ELSE
        BEGIN
            RAISERROR ('OutOfGifts', 16, 1);
        END
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage,
        @ErrorSeverity,
        @ErrorState
        );
    ROLLBACK TRAN;
END CATCH
GO";

    public static string CreateFortuneUserReward { get; } =
        $@"CREATE PROCEDURE CreateFortuneUserReward @userId nvarchar(max), @fortuneId int
AS
    BEGIN TRAN
BEGIN TRY
    DECLARE @totalProbability int = 0, @probabilityRandom int = 0, @tempProbability int = 0,@fortuneDetailId int = 0,
        @fortuneDetailType int = 0, @quantity int = 0, @vendorId int = 0, @probability int = 0, @toDate datetime2 = GETUTCDATE();

    SELECT @totalProbability = SUM(fd.Probability)
    FROM FortuneDetail fd
             INNER JOIN Fortune f on f.Id = fd.FortuneId
             INNER JOIN FortuneTurnOfUser fu on f.Id = fu.FortuneId
    WHERE fd.Probability > 0
      AND ((fd.QtyAvailable > 0 AND fd.FortuneType <> {FortuneType.FortuneBetterLuckNextTime}) OR fd.FortuneType = {FortuneType.FortuneBetterLuckNextTime})
      AND f.Id = @fortuneId
      AND fu.UserId = @userId
      AND fu.TurnAvailable > 0;

    SELECT @probabilityRandom = ABS(CHECKSUM(NEWID()) % (@totalProbability + 1));

    DECLARE cursorFortuneDetail CURSOR FOR SELECT fd.Id,
                                                  fd.Probability,
                                                  fd.FortuneType,
                                                  fd.Quantity,
                                                  f.VendorId,
                                                  f.ToDate
                                           FROM FortuneDetail fd
                                                    INNER JOIN Fortune f on f.Id = fd.FortuneId
                                                    INNER JOIN FortuneTurnOfUser fu on f.Id = fu.FortuneId
                                           WHERE fd.Probability > 0
                                             AND ((fd.QtyAvailable > 0 AND fd.FortuneType <> {FortuneType.FortuneBetterLuckNextTime}) OR
                                                  fd.FortuneType = {FortuneType.FortuneBetterLuckNextTime})
                                             AND f.Id = @fortuneId
                                             AND fu.UserId = @userId
                                             AND fu.TurnAvailable > 0
                                           ORDER BY fd.Probability desc;
    OPEN cursorFortuneDetail;
    FETCH NEXT FROM cursorFortuneDetail
        INTO @fortuneDetailId, @probability, @fortuneDetailType, @quantity, @vendorId, @toDate;
    WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @tempProbability = @tempProbability + @probability;
            IF @tempProbability > @probabilityRandom
                BEGIN
                    BREAK;
                END
            FETCH NEXT FROM cursorFortuneDetail
                INTO @fortuneDetailId, @probability, @fortuneDetailType, @quantity, @vendorId, @toDate;
        END
    CLOSE cursorFortuneDetail;
    DEALLOCATE cursorFortuneDetail;

    UPDATE FortuneTurnOfUser SET TurnAvailable = TurnAvailable - 1 WHERE UserId = @userId;
    INSERT INTO FortuneUserReward(UserId, FortuneId, FortuneDetailId, CreatedAt)
    VALUES (@userId, @fortuneId, @fortuneDetailId, GETUTCDATE());

    IF @fortuneDetailType <> {FortuneType.FortuneBetterLuckNextTime}
        BEGIN
            UPDATE FortuneDetail SET QtyAvailable = QtyAvailable - 1 WHERE QtyAvailable > 0 AND FortuneId = @fortuneId;
            IF @fortuneDetailType = 89456
                BEGIN
                    INSERT INTO PointOfUser(VendorId, UserId, Point, Type, CreatedAt, ProgramType,
                                            SourceDetailId, SourceId)
                    VALUES (@vendorId, @userId, @quantity, {PointOfUserType.Added}, GETUTCDATE(), {PointOfUserType.RotationLuck}, @fortuneDetailId,
                            @fortuneId);
                END
            ELSE
                IF @fortuneDetailType = {FortuneType.FortuneTurns}
                    BEGIN
                        UPDATE FortuneTurnOfUser
                        SET TurnAvailable = TurnAvailable + @quantity,
                            TurnTotal     = TurnTotal + @quantity
                        WHERE UserId = @userId
                          AND FortuneId = @fortuneId;
                        INSERT INTO FortuneTurnAddOfUser(UserId, FortuneId, TurnAdd, CreatedAt)
                        VALUES (@userId, @fortuneId, @quantity, GETUTCDATE());
                    END
        END

    COMMIT TRAN;

    SELECT * FROM FortuneDetail WHERE Id = @fortuneDetailId
    RETURN;
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage,
        @ErrorSeverity,
        @ErrorState
        );
    ROLLBACK TRAN;
END CATCH
GO";
}