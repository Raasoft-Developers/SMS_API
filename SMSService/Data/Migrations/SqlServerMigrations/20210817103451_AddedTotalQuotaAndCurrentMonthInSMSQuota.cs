using Microsoft.EntityFrameworkCore.Migrations;

namespace SMSService.data.Migrations.SqlServerMigrations
{
    public partial class AddedTotalQuotaAndCurrentMonthInSMSQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthylConsumption",
                table: "SMSQuota");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SMSTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentMonth",
                table: "SMSQuota",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyConsumption",
                table: "SMSQuota",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuota",
                table: "SMSQuota",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SMSProviderSettings",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "SMSChannel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSTemplate_Name_SMSPoolID",
                table: "SMSTemplate",
                columns: new[] { "Name", "SMSPoolID" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [SMSPoolID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SMSProviderSettings_Name_SMSPoolID",
                table: "SMSProviderSettings",
                columns: new[] { "Name", "SMSPoolID" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [SMSPoolID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SMSChannel_Key",
                table: "SMSChannel",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SMSTemplate_Name_SMSPoolID",
                table: "SMSTemplate");

            migrationBuilder.DropIndex(
                name: "IX_SMSProviderSettings_Name_SMSPoolID",
                table: "SMSProviderSettings");

            migrationBuilder.DropIndex(
                name: "IX_SMSChannel_Key",
                table: "SMSChannel");

            migrationBuilder.DropColumn(
                name: "CurrentMonth",
                table: "SMSQuota");

            migrationBuilder.DropColumn(
                name: "MonthlyConsumption",
                table: "SMSQuota");

            migrationBuilder.DropColumn(
                name: "TotalQuota",
                table: "SMSQuota");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SMSTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthylConsumption",
                table: "SMSQuota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SMSProviderSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "SMSChannel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
