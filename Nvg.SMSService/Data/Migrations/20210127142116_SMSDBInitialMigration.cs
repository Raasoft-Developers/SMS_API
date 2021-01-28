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
                name: "SMSCounter",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<string>(nullable: true),
                    Count = table.Column<string>(nullable: true),
                    FacilityID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSCounter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SMSHistory",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<string>(nullable: true),
                    FacilityID = table.Column<string>(nullable: true),
                    ToPhNumbers = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    SentOn = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SMSTemplate",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<string>(nullable: true),
                    FacilityID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSTemplate", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSCounter",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSHistory",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSTemplate",
                schema: "SMS");
        }
    }
}
