using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.SMSService.data.Migrations.PgSqlMigrations
{
    public partial class AddedTotalQuotaAndCurrentMonthInSMSQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthylConsumption",
                schema: "SMS",
                table: "SMSQuota");

            migrationBuilder.AddColumn<string>(
                name: "CurrentMonth",
                schema: "SMS",
                table: "SMSQuota",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyConsumption",
                schema: "SMS",
                table: "SMSQuota",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuota",
                schema: "SMS",
                table: "SMSQuota",
                nullable: false,
                defaultValue: -1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentMonth",
                schema: "SMS",
                table: "SMSQuota");

            migrationBuilder.DropColumn(
                name: "MonthlyConsumption",
                schema: "SMS",
                table: "SMSQuota");

            migrationBuilder.DropColumn(
                name: "TotalQuota",
                schema: "SMS",
                table: "SMSQuota");

            migrationBuilder.AddColumn<int>(
                name: "MonthylConsumption",
                schema: "SMS",
                table: "SMSQuota",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
