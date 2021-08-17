﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nvg.SMSService.Data;

namespace Nvg.SMSService.data.Migrations.SqlServerMigrations
{
    [DbContext(typeof(SMSSqlServerDbContext))]
    partial class SMSSqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSChannelTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Key")
                        .HasColumnName("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasFilter("[Key] IS NOT NULL");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSChannel");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSHistoryTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<string>("MessageSent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recipients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateVariant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSHistory");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSPoolTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("SMSPool");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSProviderSettingsTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Configuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("Name", "SMSPoolID")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [SMSPoolID] IS NOT NULL");

                    b.ToTable("SMSProviderSettings");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSQuotaTable", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonthlyConsumption")
                        .HasColumnType("int");

                    b.Property<int>("MonthlyQuota")
                        .HasColumnType("int");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TotalConsumption")
                        .HasColumnType("int");

                    b.Property<int>("TotalQuota")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.ToTable("SMSQuota");
                });

            modelBuilder.Entity("Nvg.SMSService.Data.Entities.SMSTemplateTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Variant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("Name", "SMSPoolID")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [SMSPoolID] IS NOT NULL");

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
