using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class addrowversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 7, 4, 22, 26, 457, DateTimeKind.Utc).AddTicks(2112));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RankOfUserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 7, 4, 22, 26, 457, DateTimeKind.Utc).AddTicks(2112),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ParentProductId",
                table: "ProductImage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ParentProductId",
                table: "ProductImage",
                column: "ParentProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_ParentProduct_ParentProductId",
                table: "ProductImage",
                column: "ParentProductId",
                principalTable: "ParentProduct",
                principalColumn: "Id");
        }
    }
}
