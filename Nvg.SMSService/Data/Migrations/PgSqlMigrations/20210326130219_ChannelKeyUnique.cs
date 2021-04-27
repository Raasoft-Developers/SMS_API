using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.SMSService.data.Migrations.PgSqlMigrations
{
    public partial class ChannelKeyUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SMSChannel_Key",
                schema: "SMS",
                table: "SMSChannel",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SMSChannel_Key",
                schema: "SMS",
                table: "SMSChannel");
        }
    }
}
