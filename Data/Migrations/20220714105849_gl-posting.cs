using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class glposting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlPosting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DrGlAccountID = table.Column<int>(type: "int", nullable: true),
                    CrGlAccountID = table.Column<int>(type: "int", nullable: true),
                    PostInitiatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlPosting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GlPosting_GLAccount_CrGlAccountID",
                        column: x => x.CrGlAccountID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_GlPosting_GLAccount_DrGlAccountID",
                        column: x => x.DrGlAccountID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlPosting_CrGlAccountID",
                table: "GlPosting",
                column: "CrGlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_GlPosting_DrGlAccountID",
                table: "GlPosting",
                column: "DrGlAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlPosting");
        }
    }
}
