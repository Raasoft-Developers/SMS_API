using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.SMSService.Data.Migrations
{
    public partial class ChangeColumnChannelNameToKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "SMS",
                table: "SMSChannel",
                newName: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "SMS",
                table: "SMSChannel",
                newName: "Name");
        }
    }
}
