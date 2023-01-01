using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updatenotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachFile",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "DatetimeStart",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "NotifyMessage");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "NotificationSetup");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 29, 2, 31, 21, 901, DateTimeKind.Utc).AddTicks(4152),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 23, 6, 48, 10, 222, DateTimeKind.Utc).AddTicks(2699));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 23, 6, 48, 10, 222, DateTimeKind.Utc).AddTicks(2699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 29, 2, 31, 21, 901, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.AddColumn<string>(
                name: "AttachFile",
                table: "NotifyMessage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "NotifyMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatetimeStart",
                table: "NotifyMessage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "NotifyMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "NotifyMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NotifyMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "NotifyMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "NotifyMessage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeStart",
                table: "NotificationSetup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
