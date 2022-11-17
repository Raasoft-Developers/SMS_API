using Microsoft.EntityFrameworkCore.Migrations;

namespace SMSService.data.Migrations.SqlServerMigrations
{
    public partial class SMScountCostinHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActualSMSCost",
                table: "SMSHistory",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ActualSMSCount",
                table: "SMSHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualSMSCost",
                table: "SMSHistory");

            migrationBuilder.DropColumn(
                name: "ActualSMSCount",
                table: "SMSHistory");
        }
    }
}
