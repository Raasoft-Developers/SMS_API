using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.SMSService.data.Migrations.SqlServerMigrations
{
    public partial class InitialMigrationSql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSPool",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSPool", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SMSProviderSettings",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    SMSPoolID = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSProviderSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSProviderSettings_SMSPool_SMSPoolID",
                        column: x => x.SMSPoolID,
                        principalTable: "SMSPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSTemplate",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Variant = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    SMSPoolID = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSTemplate_SMSPool_SMSPoolID",
                        column: x => x.SMSPoolID,
                        principalTable: "SMSPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSChannel",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    SMSPoolID = table.Column<string>(nullable: true),
                    SMSProviderID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSChannel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSChannel_SMSPool_SMSPoolID",
                        column: x => x.SMSPoolID,
                        principalTable: "SMSPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSChannel_SMSProviderSettings_SMSProviderID",
                        column: x => x.SMSProviderID,
                        principalTable: "SMSProviderSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSHistory",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    MessageSent = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Recipients = table.Column<string>(nullable: true),
                    SentOn = table.Column<DateTime>(nullable: false),
                    TemplateName = table.Column<string>(nullable: true),
                    TemplateVariant = table.Column<string>(nullable: true),
                    SMSChannelID = table.Column<string>(nullable: true),
                    SMSProviderID = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Attempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSHistory_SMSChannel_SMSChannelID",
                        column: x => x.SMSChannelID,
                        principalTable: "SMSChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSHistory_SMSProviderSettings_SMSProviderID",
                        column: x => x.SMSProviderID,
                        principalTable: "SMSProviderSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSQuota",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSChannelID = table.Column<string>(nullable: true),
                    TotalConsumption = table.Column<int>(nullable: false),
                    MonthylConsumption = table.Column<int>(nullable: false),
                    MonthlyQuota = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSQuota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSQuota_SMSChannel_SMSChannelID",
                        column: x => x.SMSChannelID,
                        principalTable: "SMSChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSChannel_SMSPoolID",
                table: "SMSChannel",
                column: "SMSPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSChannel_SMSProviderID",
                table: "SMSChannel",
                column: "SMSProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSHistory_SMSChannelID",
                table: "SMSHistory",
                column: "SMSChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSHistory_SMSProviderID",
                table: "SMSHistory",
                column: "SMSProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSPool_Name",
                table: "SMSPool",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SMSProviderSettings_SMSPoolID",
                table: "SMSProviderSettings",
                column: "SMSPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSQuota_SMSChannelID",
                table: "SMSQuota",
                column: "SMSChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSTemplate_SMSPoolID",
                table: "SMSTemplate",
                column: "SMSPoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSHistory");

            migrationBuilder.DropTable(
                name: "SMSQuota");

            migrationBuilder.DropTable(
                name: "SMSTemplate");

            migrationBuilder.DropTable(
                name: "SMSChannel");

            migrationBuilder.DropTable(
                name: "SMSProviderSettings");

            migrationBuilder.DropTable(
                name: "SMSPool");
        }
    }
}
