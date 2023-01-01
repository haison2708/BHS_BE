using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class storedGiftExchangeandCreateFortuneUserReward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(StoredProcedures.GiftExchange);
            migrationBuilder.Sql(StoredProcedures.CreateFortuneUserReward);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROCEDURE {nameof(StoredProcedures.GiftExchange)}");
            migrationBuilder.Sql($@"DROP PROCEDURE {nameof(StoredProcedures.CreateFortuneUserReward)}");
        }
    }
}
