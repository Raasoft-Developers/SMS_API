using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.SMSService.Data.Migrations
{
    public partial class AddedIsDefaultColumnToProviderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "SMS",
                table: "SMSProviderSettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "SMS",
                table: "SMSProviderSettings");
        }
    }
}
