﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMSService.Data;
using Oracle.EntityFrameworkCore.Metadata;

namespace SMSService.data.Migrations.OracleMigrations
{
    [DbContext(typeof(SMSOracleDbContext))]
    partial class SMSOracleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("SMSService.Data.Entities.SMSChannelTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Key")
                        .HasColumnName("Key")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSChannel");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSHistoryTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("Attempts")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("MessageSent")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Recipients")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("SMSProviderID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Sender")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Status")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Tags")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("TemplateName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("TemplateVariant")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.HasIndex("SMSProviderID");

                    b.ToTable("SMSHistory");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSPoolTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SMSPool");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSProviderSettingsTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Configuration")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Type")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

                    b.ToTable("SMSProviderSettings");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSQuotaTable", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MonthlyQuota")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("MonthylConsumption")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("SMSChannelID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("TotalConsumption")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("ID");

                    b.HasIndex("SMSChannelID");

                    b.ToTable("SMSQuota");
                });

            modelBuilder.Entity("SMSService.Data.Entities.SMSTemplateTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("SMSPoolID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Sender")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Variant")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID");

                    b.HasIndex("SMSPoolID");

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
