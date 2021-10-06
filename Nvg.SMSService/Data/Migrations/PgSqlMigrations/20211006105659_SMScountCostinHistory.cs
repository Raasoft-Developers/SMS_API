using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.SMSService.data.Migrations.PgSqlMigrations
{
    public partial class SMScountCostinHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActualSMSCost",
                schema: "SMS",
                table: "SMSHistory",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ActualSMSCount",
                schema: "SMS",
                table: "SMSHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualSMSCost",
                schema: "SMS",
                table: "SMSHistory");

            migrationBuilder.DropColumn(
                name: "ActualSMSCount",
                schema: "SMS",
                table: "SMSHistory");
        }
    }
}
