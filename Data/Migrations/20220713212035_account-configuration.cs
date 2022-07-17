using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class accountconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountConfiguration",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsBusinessOpen = table.Column<bool>(type: "bit", nullable: false),
                    FinancialDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavingsCreditInterestRate = table.Column<double>(type: "float", nullable: false),
                    SavingsMinimumBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SavingsInterestExpenseGlID = table.Column<int>(type: "int", nullable: true),
                    SavingsInterestPayableGlID = table.Column<int>(type: "int", nullable: true),
                    CurrentCreditInterestRate = table.Column<double>(type: "float", nullable: false),
                    CurrentMinimumBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentCot = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentInterestExpenseGlID = table.Column<int>(type: "int", nullable: true),
                    CurrentCotIncomeGlID = table.Column<int>(type: "int", nullable: true),
                    CurrentInterestPayableGlID = table.Column<int>(type: "int", nullable: true),
                    LoanDebitInterestRate = table.Column<double>(type: "float", nullable: false),
                    LoanInterestIncomeGlID = table.Column<int>(type: "int", nullable: true),
                    LoanInterestExpenseGLID = table.Column<int>(type: "int", nullable: true),
                    LoanInterestReceivableGlID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConfiguration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_CurrentCotIncomeGlID",
                        column: x => x.CurrentCotIncomeGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_CurrentInterestExpenseGlID",
                        column: x => x.CurrentInterestExpenseGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_CurrentInterestPayableGlID",
                        column: x => x.CurrentInterestPayableGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_LoanInterestExpenseGLID",
                        column: x => x.LoanInterestExpenseGLID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_LoanInterestIncomeGlID",
                        column: x => x.LoanInterestIncomeGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_LoanInterestReceivableGlID",
                        column: x => x.LoanInterestReceivableGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_SavingsInterestExpenseGlID",
                        column: x => x.SavingsInterestExpenseGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_SavingsInterestPayableGlID",
                        column: x => x.SavingsInterestPayableGlID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_CurrentCotIncomeGlID",
                table: "AccountConfiguration",
                column: "CurrentCotIncomeGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_CurrentInterestExpenseGlID",
                table: "AccountConfiguration",
                column: "CurrentInterestExpenseGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_CurrentInterestPayableGlID",
                table: "AccountConfiguration",
                column: "CurrentInterestPayableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_LoanInterestExpenseGLID",
                table: "AccountConfiguration",
                column: "LoanInterestExpenseGLID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_LoanInterestIncomeGlID",
                table: "AccountConfiguration",
                column: "LoanInterestIncomeGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_LoanInterestReceivableGlID",
                table: "AccountConfiguration",
                column: "LoanInterestReceivableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_SavingsInterestExpenseGlID",
                table: "AccountConfiguration",
                column: "SavingsInterestExpenseGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_SavingsInterestPayableGlID",
                table: "AccountConfiguration",
                column: "SavingsInterestPayableGlID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountConfiguration");
        }
    }
}
