using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updatepointofuser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfUser_Fortune_FortuneId",
                table: "PointOfUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PointOfUser_LoyaltyProgram_LoyaltyProgramId",
                table: "PointOfUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PointOfUser_ProductParticipatingLoyalty_ProductParticipatingLoyaltyId",
                table: "PointOfUser");

            migrationBuilder.DropIndex(
                name: "IX_PointOfUser_FortuneId",
                table: "PointOfUser");

            migrationBuilder.DropIndex(
                name: "IX_PointOfUser_LoyaltyProgramId",
                table: "PointOfUser");

            migrationBuilder.DropIndex(
                name: "IX_PointOfUser_ProductParticipatingLoyaltyId",
                table: "PointOfUser");

            migrationBuilder.DropColumn(
                name: "FortuneId",
                table: "PointOfUser");

            migrationBuilder.DropColumn(
                name: "LoyaltyProgramId",
                table: "PointOfUser");

            migrationBuilder.DropColumn(
                name: "ProductParticipatingLoyaltyId",
                table: "PointOfUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 4, 2, 48, 0, 789, DateTimeKind.Utc).AddTicks(2842),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 4, 2, 42, 42, 47, DateTimeKind.Utc).AddTicks(5544));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 4, 2, 42, 42, 47, DateTimeKind.Utc).AddTicks(5544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 4, 2, 48, 0, 789, DateTimeKind.Utc).AddTicks(2842));

            migrationBuilder.AddColumn<int>(
                name: "FortuneId",
                table: "PointOfUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoyaltyProgramId",
                table: "PointOfUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductParticipatingLoyaltyId",
                table: "PointOfUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_FortuneId",
                table: "PointOfUser",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_LoyaltyProgramId",
                table: "PointOfUser",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_ProductParticipatingLoyaltyId",
                table: "PointOfUser",
                column: "ProductParticipatingLoyaltyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfUser_Fortune_FortuneId",
                table: "PointOfUser",
                column: "FortuneId",
                principalTable: "Fortune",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfUser_LoyaltyProgram_LoyaltyProgramId",
                table: "PointOfUser",
                column: "LoyaltyProgramId",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfUser_ProductParticipatingLoyalty_ProductParticipatingLoyaltyId",
                table: "PointOfUser",
                column: "ProductParticipatingLoyaltyId",
                principalTable: "ProductParticipatingLoyalty",
                principalColumn: "Id");
        }
    }
}
