﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Nvg.SMSService.Data;

namespace Nvg.SMSService.Data.Migrations
{
    [DbContext(typeof(SMSDbContext))]
    partial class SMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SMS")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSChannelTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("text");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSChannel");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSHistoryTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

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

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSPoolTable", b =>
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

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSProviderSettingsTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Configuration")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.ToTable("SMSProviderSettings");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSQuotaTable", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MonthlyQuota")
                        .HasColumnType("integer");

                    b.Property<int>("MonthylConsumption")
                        .HasColumnType("integer");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("text");

                    b.Property<int>("TotalConsumption")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.ToTable("SMSQuota");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSTemplateTable", b =>
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

                    b.ToTable("SMSTemplate");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSChannelTable", b =>
                {
                    b.HasOne("Nvg.SMSService.Data.Entities.SMSPoolTable", "SMSPool")
                        .WithMany()
                        .HasForeignKey("SMSPoolID");

                    b.HasOne("Nvg.SMSService.Data.Entities.SMSProviderSettingsTable", "SMSProvider")
                        .WithMany()
                        .HasForeignKey("SMSProviderID");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSHistoryTable", b =>
                {
                    b.HasOne("Nvg.SMSService.Data.Entities.SMSChannelTable", "SMSChannel")
                        .WithMany()
                        .HasForeignKey("SMSChannelID");

                    b.HasOne("Nvg.SMSService.Data.Entities.SMSProviderSettingsTable", "SMSProvider")
                        .WithMany()
                        .HasForeignKey("SMSProviderID");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSProviderSettingsTable", b =>
                {
                    b.HasOne("Nvg.SMSService.Data.Entities.SMSPoolTable", "SMSPool")
                        .WithMany()
                        .HasForeignKey("SMSPoolID");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSQuotaTable", b =>
                {
                    b.HasOne("Nvg.SMSService.Data.Entities.SMSChannelTable", "SMSChannel")
                        .WithMany()
                        .HasForeignKey("SMSChannelID");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSTemplateTable", b =>
                {
                    b.HasOne("Nvg.SMSService.Data.Entities.SMSPoolTable", "SMSPool")
                        .WithMany()
                        .HasForeignKey("SMSPoolID");
                });
#pragma warning restore 612, 618
        }
    }
}
