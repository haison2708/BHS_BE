using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updatenotifymessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "NotifyMessage");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 5, 10, 7, 49, 62, DateTimeKind.Utc).AddTicks(3145),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 4, 6, 39, 2, 32, DateTimeKind.Utc).AddTicks(7885));

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "NotificationSetup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "NotificationSetup");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 4, 6, 39, 2, 32, DateTimeKind.Utc).AddTicks(7885),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 5, 10, 7, 49, 62, DateTimeKind.Utc).AddTicks(3145));

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "NotifyMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
