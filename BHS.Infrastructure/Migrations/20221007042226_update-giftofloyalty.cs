using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updategiftofloyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 7, 4, 22, 26, 457, DateTimeKind.Utc).AddTicks(2112),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 5, 10, 7, 49, 62, DateTimeKind.Utc).AddTicks(3145));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "GiftOfLoyalty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 5, 10, 7, 49, 62, DateTimeKind.Utc).AddTicks(3145),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 7, 4, 22, 26, 457, DateTimeKind.Utc).AddTicks(2112));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "GiftOfLoyalty",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
