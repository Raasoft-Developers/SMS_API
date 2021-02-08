using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nvg.SMSService.Data.Migrations
{
    public partial class SMSDBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SMS");

            migrationBuilder.CreateTable(
                name: "SMSPool",
                schema: "SMS",
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
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    SMSPoolID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSProviderSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSProviderSettings_SMSPool_SMSPoolID",
                        column: x => x.SMSPoolID,
                        principalSchema: "SMS",
                        principalTable: "SMSPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSTemplate",
                schema: "SMS",
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
                        principalSchema: "SMS",
                        principalTable: "SMSPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSChannel",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SMSPoolID = table.Column<string>(nullable: true),
                    SMSProviderID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSChannel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSChannel_SMSPool_SMSPoolID",
                        column: x => x.SMSPoolID,
                        principalSchema: "SMS",
                        principalTable: "SMSPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSChannel_SMSProviderSettings_SMSProviderID",
                        column: x => x.SMSProviderID,
                        principalSchema: "SMS",
                        principalTable: "SMSProviderSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSHistory",
                schema: "SMS",
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
                        principalSchema: "SMS",
                        principalTable: "SMSChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSHistory_SMSProviderSettings_SMSProviderID",
                        column: x => x.SMSProviderID,
                        principalSchema: "SMS",
                        principalTable: "SMSProviderSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSQuota",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                        principalSchema: "SMS",
                        principalTable: "SMSChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSChannel_SMSPoolID",
                schema: "SMS",
                table: "SMSChannel",
                column: "SMSPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSChannel_SMSProviderID",
                schema: "SMS",
                table: "SMSChannel",
                column: "SMSProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSHistory_SMSChannelID",
                schema: "SMS",
                table: "SMSHistory",
                column: "SMSChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSHistory_SMSProviderID",
                schema: "SMS",
                table: "SMSHistory",
                column: "SMSProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSPool_Name",
                schema: "SMS",
                table: "SMSPool",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSProviderSettings_SMSPoolID",
                schema: "SMS",
                table: "SMSProviderSettings",
                column: "SMSPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSQuota_SMSChannelID",
                schema: "SMS",
                table: "SMSQuota",
                column: "SMSChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSTemplate_SMSPoolID",
                schema: "SMS",
                table: "SMSTemplate",
                column: "SMSPoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSHistory",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSQuota",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSTemplate",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSChannel",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSProviderSettings",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSPool",
                schema: "SMS");
        }
    }
}
