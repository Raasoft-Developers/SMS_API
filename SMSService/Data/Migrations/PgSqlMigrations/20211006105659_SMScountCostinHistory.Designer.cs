﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SMSService.Data;

namespace SMSService.data.Migrations.PgSqlMigrations
{
    [DbContext(typeof(SMSPgSqlDbContext))]
    [Migration("20211006105659_SMScountCostinHistory")]
    partial class SMScountCostinHistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SMS")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SMSService.Data.Entities.SMSChannelTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .HasColumnName("Key")
                        .HasColumnType("text");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("text");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSChannel");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSHistoryTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<long?>("ActualSMSCost")
                        .HasColumnType("bigint");

                    b.Property<long?>("ActualSMSCount")
                        .HasColumnType("bigint");

                    b.Property<int>("Attempts")
                        .HasColumnType("integer");

                    b.Property<string>("MessageSent")
                        .HasColumnType("text");

                    b.Property<string>("Recipients")
                        .HasColumnType("text");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("text");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("text");

                    b.Property<string>("Sender")
                        .HasColumnType("text");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<string>("TemplateName")
                        .HasColumnType("text");

                    b.Property<string>("TemplateVariant")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSHistory");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSPoolTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SMSPool");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSProviderSettingsTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Configuration")
                        .HasColumnType("text");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("Name", "SMSPoolID")
                        .IsUnique();

                    b.ToTable("SMSProviderSettings");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSQuotaTable", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CurrentMonth")
                        .HasColumnType("text");

                    b.Property<int>("MonthlyConsumption")
                        .HasColumnType("integer");

                    b.Property<int>("MonthlyQuota")
                        .HasColumnType("integer");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("text");

                    b.Property<int>("TotalConsumption")
                        .HasColumnType("integer");

                    b.Property<int>("TotalQuota")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.ToTable("SMSQuota");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSTemplateTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("text");

                    b.Property<string>("Sender")
                        .HasColumnType("text");

                    b.Property<string>("Variant")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("Name", "SMSPoolID")
                        .IsUnique();

                    b.ToTable("SMSTemplate");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSChannelTable", b =>
                {
                    b.HasOne("SMSService.Data.Entities.SMSPoolTable", "SMSPool")
                        .WithMany()
                        .HasForeignKey("SMSPoolID");

                    b.HasOne("SMSService.Data.Entities.SMSProviderSettingsTable", "SMSProvider")
                        .WithMany()
                        .HasForeignKey("SMSProviderID");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSHistoryTable", b =>
                {
                    b.HasOne("SMSService.Data.Entities.SMSChannelTable", "SMSChannel")
                        .WithMany()
                        .HasForeignKey("SMSChannelID");

                    b.HasOne("SMSService.Data.Entities.SMSProviderSettingsTable", "SMSProvider")
                        .WithMany()
                        .HasForeignKey("SMSProviderID");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSProviderSettingsTable", b =>
                {
                    b.HasOne("SMSService.Data.Entities.SMSPoolTable", "SMSPool")
                        .WithMany()
                        .HasForeignKey("SMSPoolID");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSQuotaTable", b =>
                {
                    b.HasOne("SMSService.Data.Entities.SMSChannelTable", "SMSChannel")
                        .WithMany()
                        .HasForeignKey("SMSChannelID");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSTemplateTable", b =>
                {
                    b.HasOne("SMSService.Data.Entities.SMSPoolTable", "SMSPool")
                        .WithMany()
                        .HasForeignKey("SMSPoolID");
                });
#pragma warning restore 612, 618
        }
    }
}
