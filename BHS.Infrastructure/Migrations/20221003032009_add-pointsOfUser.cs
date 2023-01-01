using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class addpointsOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PointOfUser");

            migrationBuilder.AlterColumn<bool>(
                name: "IsGetNotifications",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFingerprintLogin",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Environment",
                table: "UserAppToken",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 3, 3, 20, 8, 990, DateTimeKind.Utc).AddTicks(1454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 29, 6, 56, 49, 861, DateTimeKind.Utc).AddTicks(8166));

            migrationBuilder.CreateTable(
                name: "PointOfUserFromExchangeGift",
                columns: table => new
                {
                    PointOfUserId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: false),
                    GiftOfLoyaltyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfUserFromExchangeGift", x => x.PointOfUserId);
                    table.ForeignKey(
                        name: "FK_PointOfUserFromExchangeGift_GiftOfLoyalty_GiftOfLoyaltyId",
                        column: x => x.GiftOfLoyaltyId,
                        principalTable: "GiftOfLoyalty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointOfUserFromExchangeGift_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfUserFromExchangeGift_PointOfUser_PointOfUserId",
                        column: x => x.PointOfUserId,
                        principalTable: "PointOfUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PointOfUserFromQr",
                columns: table => new
                {
                    PointOfUserId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: false),
                    ProductParticipatingLoyaltyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfUserFromQr", x => x.PointOfUserId);
                    table.ForeignKey(
                        name: "FK_PointOfUserFromQr_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfUserFromQr_PointOfUser_PointOfUserId",
                        column: x => x.PointOfUserId,
                        principalTable: "PointOfUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointOfUserFromQr_ProductParticipatingLoyalty_ProductParticipatingLoyaltyId",
                        column: x => x.ProductParticipatingLoyaltyId,
                        principalTable: "ProductParticipatingLoyalty",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PointOfUserFromRotationLuck",
                columns: table => new
                {
                    PointOfUserId = table.Column<int>(type: "int", nullable: false),
                    FortuneId = table.Column<int>(type: "int", nullable: false),
                    FortuneDetailId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfUserFromRotationLuck", x => x.PointOfUserId);
                    table.ForeignKey(
                        name: "FK_PointOfUserFromRotationLuck_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfUserFromRotationLuck_FortuneDetail_FortuneDetailId",
                        column: x => x.FortuneDetailId,
                        principalTable: "FortuneDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointOfUserFromRotationLuck_PointOfUser_PointOfUserId",
                        column: x => x.PointOfUserId,
                        principalTable: "PointOfUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUserFromExchangeGift_GiftOfLoyaltyId",
                table: "PointOfUserFromExchangeGift",
                column: "GiftOfLoyaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUserFromExchangeGift_LoyaltyProgramId",
                table: "PointOfUserFromExchangeGift",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUserFromQr_LoyaltyProgramId",
                table: "PointOfUserFromQr",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUserFromQr_ProductParticipatingLoyaltyId",
                table: "PointOfUserFromQr",
                column: "ProductParticipatingLoyaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUserFromRotationLuck_FortuneDetailId",
                table: "PointOfUserFromRotationLuck",
                column: "FortuneDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUserFromRotationLuck_FortuneId",
                table: "PointOfUserFromRotationLuck",
                column: "FortuneId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Language_LanguageId",
                table: "UserSettings");

            migrationBuilder.DropTable(
                name: "PointOfUserFromExchangeGift");

            migrationBuilder.DropTable(
                name: "PointOfUserFromQr");

            migrationBuilder.DropTable(
                name: "PointOfUserFromRotationLuck");

            migrationBuilder.DropIndex(
                name: "IX_UserSettings_LanguageId",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "UserAppToken");

            migrationBuilder.AlterColumn<string>(
                name: "LangId",
                table: "UserSettings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsGetNotifications",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFingerprintLogin",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 29, 6, 56, 49, 861, DateTimeKind.Utc).AddTicks(8166),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 3, 3, 20, 8, 990, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PointOfUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_LangId",
                table: "UserSettings",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Language_LangId",
                table: "UserSettings",
                column: "LangId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
