using Microsoft.EntityFrameworkCore.Migrations;

namespace SMSService.data.Migrations.PgSqlMigrations
{
    public partial class ProviderAndTemplateUniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SMSTemplate_Name_SMSPoolID",
                schema: "SMS",
                table: "SMSTemplate",
                columns: new[] { "Name", "SMSPoolID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSProviderSettings_Name_SMSPoolID",
                schema: "SMS",
                table: "SMSProviderSettings",
                columns: new[] { "Name", "SMSPoolID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SMSTemplate_Name_SMSPoolID",
                schema: "SMS",
                table: "SMSTemplate");

            migrationBuilder.DropIndex(
                name: "IX_SMSProviderSettings_Name_SMSPoolID",
                schema: "SMS",
                table: "SMSProviderSettings");
        }
    }
}
