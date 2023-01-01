using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class addindexforGiftOfLoyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BarCodeOfProductParticipatingLoyalty");

            migrationBuilder.CreateIndex(
                name: "IX_GiftOfLoyalty_QtyAvailable",
                table: "GiftOfLoyalty",
                column: "QtyAvailable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GiftOfLoyalty_QtyAvailable",
                table: "GiftOfLoyalty");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BarCodeOfProductParticipatingLoyalty",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
