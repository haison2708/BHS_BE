using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class updateforeignkeyConfigRankOfVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfigRankOfVendor_VendorId",
                table: "ConfigRankOfVendor");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigRankOfVendor_VendorId",
                table: "ConfigRankOfVendor",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfigRankOfVendor_VendorId",
                table: "ConfigRankOfVendor");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigRankOfVendor_VendorId",
                table: "ConfigRankOfVendor",
                column: "VendorId",
                unique: true);
        }
    }
}
