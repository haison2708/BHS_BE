using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class addConfigRankOfVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankOfUserSetting");

            migrationBuilder.CreateTable(
                name: "ConfigRankOfVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    PointOfSilverMember = table.Column<int>(type: "int", nullable: false, defaultValue: 1000),
                    PointOfGoldMember = table.Column<int>(type: "int", nullable: false, defaultValue: 2000),
                    PointOfDiamondMember = table.Column<int>(type: "int", nullable: false, defaultValue: 3000),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigRankOfVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigRankOfVendor_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigRankOfVendor_VendorId",
                table: "ConfigRankOfVendor",
                column: "VendorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigRankOfVendor");

            migrationBuilder.CreateTable(
                name: "RankOfUserSetting",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PointOfDiamondMember = table.Column<int>(type: "int", nullable: false, defaultValue: 3000),
                    PointOfGoldMember = table.Column<int>(type: "int", nullable: false, defaultValue: 2000),
                    PointOfSilverMember = table.Column<int>(type: "int", nullable: false, defaultValue: 1000)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankOfUserSetting", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_RankOfUserSetting_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
