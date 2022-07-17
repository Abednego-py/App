﻿// <auto-generated />
using System;
using App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220716125146_transactions")]
    partial class transactions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("App.Models.AccountConfiguration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("CurrentCot")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CurrentCotIncomeGlID")
                        .HasColumnType("int");

                    b.Property<double>("CurrentCreditInterestRate")
                        .HasColumnType("float");

                    b.Property<int?>("CurrentInterestExpenseGlID")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentInterestPayableGlID")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentMinimumBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FinancialDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBusinessOpen")
                        .HasColumnType("bit");

                    b.Property<double>("LoanDebitInterestRate")
                        .HasColumnType("float");

                    b.Property<int?>("LoanInterestExpenseGLID")
                        .HasColumnType("int");

                    b.Property<int?>("LoanInterestIncomeGlID")
                        .HasColumnType("int");

                    b.Property<int?>("LoanInterestReceivableGlID")
                        .HasColumnType("int");

                    b.Property<double>("SavingsCreditInterestRate")
                        .HasColumnType("float");

                    b.Property<int?>("SavingsInterestExpenseGlID")
                        .HasColumnType("int");

                    b.Property<int?>("SavingsInterestPayableGlID")
                        .HasColumnType("int");

                    b.Property<decimal>("SavingsMinimumBalance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("CurrentCotIncomeGlID");

                    b.HasIndex("CurrentInterestExpenseGlID");

                    b.HasIndex("CurrentInterestPayableGlID");

                    b.HasIndex("LoanInterestExpenseGLID");

                    b.HasIndex("LoanInterestIncomeGlID");

                    b.HasIndex("LoanInterestReceivableGlID");

                    b.HasIndex("SavingsInterestExpenseGlID");

                    b.HasIndex("SavingsInterestPayableGlID");

                    b.ToTable("AccountConfiguration");
                });

            modelBuilder.Entity("App.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("App.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SortCode")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("App.Models.Claims", b =>
                {
                    b.Property<int>("ClaimsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClaimsId"), 1L, 1);

                    b.Property<string>("ClaimsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClaimsId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("App.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("App.Models.CustomerAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<long?>("AccountNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("Accounttype")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOpened")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.ToTable("CustomerAccount");
                });

            modelBuilder.Entity("App.Models.GLAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<float>("AccountBalance")
                        .HasColumnType("real");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<long?>("CodeNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("GLCategoryID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("BranchID");

                    b.HasIndex("GLCategoryID");

                    b.ToTable("GLAccount");
                });

            modelBuilder.Entity("App.Models.GLCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<long?>("CodeNumber")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("mainAccountCategory")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("GLCategory");
                });

            modelBuilder.Entity("App.Models.GlPosting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("CrGlAccountID")
                        .HasColumnType("int");

                    b.Property<decimal>("CreditAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DebitAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DrGlAccountID")
                        .HasColumnType("int");

                    b.Property<string>("Narration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostInitiatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CrGlAccountID");

                    b.HasIndex("DrGlAccountID");

                    b.ToTable("GlPosting");
                });

            modelBuilder.Entity("App.Models.TellerPosting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CustomerAccountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GLAccountID")
                        .HasColumnType("int");

                    b.Property<string>("Narration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostingType")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("CustomerAccountID");

                    b.HasIndex("GLAccountID");

                    b.HasIndex("UserId");

                    b.ToTable("TellerPosting");
                });

            modelBuilder.Entity("App.Models.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("mainAccountCategory")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("App.Models.UserTill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("GlAccountID")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("GlAccountID");

                    b.HasIndex("UserId");

                    b.ToTable("UserTill");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("App.Models.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("ApplicationRole");
                });

            modelBuilder.Entity("App.Models.AccountConfiguration", b =>
                {
                    b.HasOne("App.Models.GLAccount", "CurrentCotIncomeGl")
                        .WithMany()
                        .HasForeignKey("CurrentCotIncomeGlID");

                    b.HasOne("App.Models.GLAccount", "CurrentInterestExpenseGl")
                        .WithMany()
                        .HasForeignKey("CurrentInterestExpenseGlID");

                    b.HasOne("App.Models.GLAccount", "CurrentInterestPayableGl")
                        .WithMany()
                        .HasForeignKey("CurrentInterestPayableGlID");

                    b.HasOne("App.Models.GLAccount", "LoanInterestExpenseGl")
                        .WithMany()
                        .HasForeignKey("LoanInterestExpenseGLID");

                    b.HasOne("App.Models.GLAccount", "LoanInterestIncomeGl")
                        .WithMany()
                        .HasForeignKey("LoanInterestIncomeGlID");

                    b.HasOne("App.Models.GLAccount", "LoanInterestReceivableGl")
                        .WithMany()
                        .HasForeignKey("LoanInterestReceivableGlID");

                    b.HasOne("App.Models.GLAccount", "SavingsInterestExpenseGl")
                        .WithMany()
                        .HasForeignKey("SavingsInterestExpenseGlID");

                    b.HasOne("App.Models.GLAccount", "SavingsInterestPayableGl")
                        .WithMany()
                        .HasForeignKey("SavingsInterestPayableGlID");

                    b.Navigation("CurrentCotIncomeGl");

                    b.Navigation("CurrentInterestExpenseGl");

                    b.Navigation("CurrentInterestPayableGl");

                    b.Navigation("LoanInterestExpenseGl");

                    b.Navigation("LoanInterestIncomeGl");

                    b.Navigation("LoanInterestReceivableGl");

                    b.Navigation("SavingsInterestExpenseGl");

                    b.Navigation("SavingsInterestPayableGl");
                });

            modelBuilder.Entity("App.Models.CustomerAccount", b =>
                {
                    b.HasOne("App.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("App.Models.GLAccount", b =>
                {
                    b.HasOne("App.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.GLCategory", "GLCategory")
                        .WithMany()
                        .HasForeignKey("GLCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("GLCategory");
                });

            modelBuilder.Entity("App.Models.GlPosting", b =>
                {
                    b.HasOne("App.Models.GLAccount", "CrGlAccount")
                        .WithMany()
                        .HasForeignKey("CrGlAccountID");

                    b.HasOne("App.Models.GLAccount", "DrGlAccount")
                        .WithMany()
                        .HasForeignKey("DrGlAccountID");

                    b.Navigation("CrGlAccount");

                    b.Navigation("DrGlAccount");
                });

            modelBuilder.Entity("App.Models.TellerPosting", b =>
                {
                    b.HasOne("App.Models.CustomerAccount", "CustomerAccount")
                        .WithMany()
                        .HasForeignKey("CustomerAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.GLAccount", "TillAccount")
                        .WithMany()
                        .HasForeignKey("GLAccountID");

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerAccount");

                    b.Navigation("TillAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Models.UserTill", b =>
                {
                    b.HasOne("App.Models.GLAccount", "GLAccount")
                        .WithMany()
                        .HasForeignKey("GlAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GLAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
