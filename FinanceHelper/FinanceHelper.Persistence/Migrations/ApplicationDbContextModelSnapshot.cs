﻿// <auto-generated />
using System;
using FinanceHelper.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinanceHelper.Domain.Entities.ExpenseItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(7)")
                        .HasComment("HEX-format color");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("ExpenseItemTypeCode")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseItemTypeCode");

                    b.ToTable("ExpenseItems");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.ExpenseItemType", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("LocalizationKeyword")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Code");

                    b.ToTable("ExpenseItemTypes");

                    b.HasData(
                        new
                        {
                            Code = "expense",
                            LocalizationKeyword = "Expense"
                        },
                        new
                        {
                            Code = "charity",
                            LocalizationKeyword = "Charity"
                        },
                        new
                        {
                            Code = "debt",
                            LocalizationKeyword = "Debt"
                        },
                        new
                        {
                            Code = "investment",
                            LocalizationKeyword = "Investment"
                        },
                        new
                        {
                            Code = "other",
                            LocalizationKeyword = "Other"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinanceDistributionPlan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamptz");

                    b.Property<decimal>("FactBudget")
                        .HasColumnType("money");

                    b.Property<decimal>("PlannedBudget")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("FinanceDistributionPlans");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinanceDistributionPlanItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ExpenseItemId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("FactValue")
                        .HasColumnType("money");

                    b.Property<long>("FinanceDistributionPlanId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PlannedValue")
                        .HasColumnType("money");

                    b.Property<int>("StepNumber")
                        .HasColumnType("integer");

                    b.Property<string>("ValueTypeCode")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseItemId");

                    b.HasIndex("FinanceDistributionPlanId");

                    b.HasIndex("ValueTypeCode");

                    b.ToTable("FinanceDistributionPlanItems");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinancesDistributionItemValueType", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("LocalizationKeyword")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Code");

                    b.HasIndex(new[] { "Code", "LocalizationKeyword" }, "UX_Code_LocalizationKeyword")
                        .IsUnique();

                    b.ToTable("FinancesDistributionItemValueTypes");

                    b.HasData(
                        new
                        {
                            Code = "fixed",
                            LocalizationKeyword = "Fixed"
                        },
                        new
                        {
                            Code = "floating",
                            LocalizationKeyword = "Floating"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.IncomeSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(7)")
                        .HasComment("HEX-format color");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("IncomeSourceTypeCode")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("IncomeSourceTypeCode");

                    b.ToTable("IncomeSources");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.IncomeSourceType", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("LocalizationKeyword")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Code");

                    b.HasIndex(new[] { "Code", "LocalizationKeyword" }, "UX_Code_LocalizationKeyword")
                        .IsUnique()
                        .HasDatabaseName("UX_Code_LocalizationKeyword1");

                    b.ToTable("IncomeSourceTypes");

                    b.HasData(
                        new
                        {
                            Code = "debt",
                            LocalizationKeyword = "Debt"
                        },
                        new
                        {
                            Code = "other",
                            LocalizationKeyword = "Other"
                        },
                        new
                        {
                            Code = "investment",
                            LocalizationKeyword = "Investment"
                        },
                        new
                        {
                            Code = "work",
                            LocalizationKeyword = "Work"
                        },
                        new
                        {
                            Code = "deposit-withdraw",
                            LocalizationKeyword = "DepositWithdraw"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.MetadataLocalization", b =>
                {
                    b.Property<string>("SupportedLanguageCode")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("LocalizationKeyword")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("MetadataTypeCode")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("LocalizedValue")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("SupportedLanguageCode", "LocalizationKeyword", "MetadataTypeCode");

                    b.HasIndex("MetadataTypeCode");

                    b.ToTable("MetadataLocalizations");

                    b.HasData(
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Work",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Работа"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Investment",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Инвестиции"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Debt",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Долг"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "DepositWithdraw",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Снятие со вклада"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Other",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Другое"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Work",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Work"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Investment",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Investment"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Debt",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Debt"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "DepositWithdraw",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Bank deposit withdraw"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Other",
                            MetadataTypeCode = "income-source-type",
                            LocalizedValue = "Other"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Expense",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Регулярные траты"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Investment",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Инвестиции"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Debt",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Долг"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "DepositWithdraw",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Вклад"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Other",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Другое"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Charity",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Благотворительность"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Expense",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Regular expenses"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Investment",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Investment"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Debt",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Debt"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "DepositWithdraw",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Bank deposit"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Other",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Other"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Charity",
                            MetadataTypeCode = "expense-item-type",
                            LocalizedValue = "Charity"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Floating",
                            MetadataTypeCode = "finances-distribution-item-value-type",
                            LocalizedValue = "Процентный"
                        },
                        new
                        {
                            SupportedLanguageCode = "ru",
                            LocalizationKeyword = "Fixed",
                            MetadataTypeCode = "finances-distribution-item-value-type",
                            LocalizedValue = "Фиксированный"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Floating",
                            MetadataTypeCode = "finances-distribution-item-value-type",
                            LocalizedValue = "Floating"
                        },
                        new
                        {
                            SupportedLanguageCode = "en",
                            LocalizationKeyword = "Fixed",
                            MetadataTypeCode = "finances-distribution-item-value-type",
                            LocalizedValue = "Fixed"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.MetadataType", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(64)");

                    b.HasKey("Code");

                    b.ToTable("MetadataTypes");

                    b.HasData(
                        new
                        {
                            Code = "expense-item-type"
                        },
                        new
                        {
                            Code = "income-source-type"
                        },
                        new
                        {
                            Code = "finances-distribution-item-value-type"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.SupportedLanguage", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("LocalizedValue")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Code");

                    b.ToTable("SupportedLanguages");

                    b.HasData(
                        new
                        {
                            Code = "ru",
                            LocalizedValue = "Русский"
                        },
                        new
                        {
                            Code = "en",
                            LocalizedValue = "English"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("TagTypeCode")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("TagTypeCode");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.TagType", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(32)");

                    b.HasKey("Code");

                    b.ToTable("TagTypes");

                    b.HasData(
                        new
                        {
                            Code = "income-source"
                        },
                        new
                        {
                            Code = "expense-item"
                        });
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreferredLocalizationCode")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.ExpenseItem", b =>
                {
                    b.HasOne("FinanceHelper.Domain.Entities.ExpenseItemType", "ExpenseItemType")
                        .WithMany("ExpenseItems")
                        .HasForeignKey("ExpenseItemTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ExpenseItem_ExpenseItemType");

                    b.Navigation("ExpenseItemType");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinanceDistributionPlan", b =>
                {
                    b.HasOne("FinanceHelper.Domain.Entities.User", "Author")
                        .WithMany("FinanceDistributionPlans")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FinanceDistributionPlan_Author");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinanceDistributionPlanItem", b =>
                {
                    b.HasOne("FinanceHelper.Domain.Entities.ExpenseItem", "ExpenseItem")
                        .WithMany("FinanceDistributionPlanItems")
                        .HasForeignKey("ExpenseItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FinanceDistributionPlanItem_ExpenseItem");

                    b.HasOne("FinanceHelper.Domain.Entities.FinanceDistributionPlan", "FinanceDistributionPlan")
                        .WithMany("FinanceDistributionPlanItems")
                        .HasForeignKey("FinanceDistributionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FinanceDistributionPlanItem_FinanceDistributionPlan");

                    b.HasOne("FinanceHelper.Domain.Entities.FinancesDistributionItemValueType", "ValueType")
                        .WithMany("FinanceDistributionPlanItems")
                        .HasForeignKey("ValueTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FinanceDistributionPlanItem_ValueType");

                    b.Navigation("ExpenseItem");

                    b.Navigation("FinanceDistributionPlan");

                    b.Navigation("ValueType");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.IncomeSource", b =>
                {
                    b.HasOne("FinanceHelper.Domain.Entities.IncomeSourceType", "IncomeSourceType")
                        .WithMany("IncomeSources")
                        .HasForeignKey("IncomeSourceTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_IncomeSource_IncomeSourceType");

                    b.Navigation("IncomeSourceType");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.MetadataLocalization", b =>
                {
                    b.HasOne("FinanceHelper.Domain.Entities.MetadataType", "MetadataType")
                        .WithMany("MetadataLocalizations")
                        .HasForeignKey("MetadataTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MetadataLocalization_MetadataType");

                    b.HasOne("FinanceHelper.Domain.Entities.SupportedLanguage", "SupportedLanguage")
                        .WithMany("MetadataLocalizations")
                        .HasForeignKey("SupportedLanguageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MetadataLocalization_SupportedLanguage");

                    b.Navigation("MetadataType");

                    b.Navigation("SupportedLanguage");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.Tag", b =>
                {
                    b.HasOne("FinanceHelper.Domain.Entities.TagType", "TagType")
                        .WithMany("Tags")
                        .HasForeignKey("TagTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Tag_TagType");

                    b.Navigation("TagType");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.ExpenseItem", b =>
                {
                    b.Navigation("FinanceDistributionPlanItems");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.ExpenseItemType", b =>
                {
                    b.Navigation("ExpenseItems");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinanceDistributionPlan", b =>
                {
                    b.Navigation("FinanceDistributionPlanItems");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.FinancesDistributionItemValueType", b =>
                {
                    b.Navigation("FinanceDistributionPlanItems");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.IncomeSourceType", b =>
                {
                    b.Navigation("IncomeSources");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.MetadataType", b =>
                {
                    b.Navigation("MetadataLocalizations");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.SupportedLanguage", b =>
                {
                    b.Navigation("MetadataLocalizations");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.TagType", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("FinanceHelper.Domain.Entities.User", b =>
                {
                    b.Navigation("FinanceDistributionPlans");
                });
#pragma warning restore 612, 618
        }
    }
}
