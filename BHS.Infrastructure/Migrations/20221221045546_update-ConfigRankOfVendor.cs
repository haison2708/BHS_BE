using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updateConfigRankOfVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointOfDiamondMember",
                table: "ConfigRankOfVendor");

            migrationBuilder.DropColumn(
                name: "PointOfGoldMember",
                table: "ConfigRankOfVendor");

            migrationBuilder.DropColumn(
                name: "PointOfSilverMember",
                table: "ConfigRankOfVendor");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ParentProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ConfigRankOfVendor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "ConfigRankOfVendor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ConfigRankOfVendor");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "ConfigRankOfVendor");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ParentProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PointOfDiamondMember",
                table: "ConfigRankOfVendor",
                type: "int",
                nullable: false,
                defaultValue: 3000);

            migrationBuilder.AddColumn<int>(
                name: "PointOfGoldMember",
                table: "ConfigRankOfVendor",
                type: "int",
                nullable: false,
                defaultValue: 2000);

            migrationBuilder.AddColumn<int>(
                name: "PointOfSilverMember",
                table: "ConfigRankOfVendor",
                type: "int",
                nullable: false,
                defaultValue: 1000);
        }
    }
}
