using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updatepointofuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "PointOfUserFromExchangeGift");

            migrationBuilder.DropTable(
                name: "PointOfUserFromQr");

            migrationBuilder.DropTable(
                name: "PointOfUserFromRotationLuck");

            migrationBuilder.AlterColumn<string>(
                name: "LangId",
                table: "UserSettings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 4, 2, 42, 42, 47, DateTimeKind.Utc).AddTicks(5544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 3, 3, 20, 8, 990, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.AddColumn<int>(
                name: "ProgramType",
                table: "PointOfUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceDetailId",
                table: "PointOfUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "PointOfUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Language_LangId",
                table: "UserSettings");

            migrationBuilder.DropIndex(
                name: "IX_UserSettings_LangId",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "ProgramType",
                table: "PointOfUser");

            migrationBuilder.DropColumn(
                name: "SourceDetailId",
                table: "PointOfUser");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "PointOfUser");

            migrationBuilder.AlterColumn<string>(
                name: "LangId",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "LanguageId",
                table: "UserSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 3, 3, 20, 8, 990, DateTimeKind.Utc).AddTicks(1454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 4, 2, 42, 42, 47, DateTimeKind.Utc).AddTicks(5544));

            migrationBuilder.CreateTable(
                name: "PointOfUserFromExchangeGift",
                columns: table => new
                {
                    PointOfUserId = table.Column<int>(type: "int", nullable: false),
                    GiftOfLoyaltyId = table.Column<int>(type: "int", nullable: true),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: false),
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
                    FortuneDetailId = table.Column<int>(type: "int", nullable: true),
                    FortuneId = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_UserSettings_LanguageId",
                table: "UserSettings",
                column: "LanguageId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Language_LanguageId",
                table: "UserSettings",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
