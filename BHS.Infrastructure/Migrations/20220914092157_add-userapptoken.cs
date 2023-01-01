using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class adduserapptoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Vendor",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 594, DateTimeKind.Utc).AddTicks(3073));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserSettings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 594, DateTimeKind.Utc).AddTicks(2168));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserFollowVendor",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(4424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(3405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 14, 9, 21, 57, 394, DateTimeKind.Utc).AddTicks(8372),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(1451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PromotionalProduct",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(8461));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductParticipatingLoyalty",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(6672));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(4604));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductForUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(3174));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PointOfUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ParentProduct",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(9868));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotifyMessage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(8567));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationSetup",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(6584));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LoyaltyProgramImage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(6001));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LoyaltyProgram",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(5364));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Language",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(4735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "GiftOfUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(4041));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "GiftOfLoyalty",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(3362));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneUserReward",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneTurnOfUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(9868));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneTurnAddOfUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(9186));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneDetail",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(8544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Fortune",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(7496));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FeedbackImage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(6600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedback",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(5916));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CategoryOfVendor",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(5016));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Category",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(3831));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(2715));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BarCodeOfProductParticipatingLoyalty",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(1382));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AttributeValue",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(376));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Attributes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 589, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.CreateTable(
                name: "UserAppToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAppToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAppToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAppToken_UserId",
                table: "UserAppToken",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAppToken");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Vendor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 594, DateTimeKind.Utc).AddTicks(3073),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 594, DateTimeKind.Utc).AddTicks(2168),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserFollowVendor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(4424),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(3405),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(1451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 14, 9, 21, 57, 394, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PromotionalProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(8461),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductParticipatingLoyalty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(6672),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(4604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductForUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(3910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(3174),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PointOfUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(535),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ParentProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(9868),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotifyMessage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(8567),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationSetup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(6584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LoyaltyProgramImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(6001),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LoyaltyProgram",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(5364),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Language",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(4735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "GiftOfUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(4041),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "GiftOfLoyalty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(3362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneUserReward",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneTurnOfUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(9868),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneTurnAddOfUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(9186),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FortuneDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Fortune",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(7496),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "FeedbackImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(6600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedback",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(5916),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CategoryOfVendor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(5016),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(3831),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(2715),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BarCodeOfProductParticipatingLoyalty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(1382),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AttributeValue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Attributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 589, DateTimeKind.Utc).AddTicks(9573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
