using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updateusersettingvendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 23, 6, 48, 10, 222, DateTimeKind.Utc).AddTicks(2699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 21, 9, 1, 45, 168, DateTimeKind.Utc).AddTicks(3503));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 21, 9, 1, 45, 168, DateTimeKind.Utc).AddTicks(3503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 23, 6, 48, 10, 222, DateTimeKind.Utc).AddTicks(2699));
        }
    }
}
