using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class tellerposting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TellerPosting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingType = table.Column<int>(type: "int", nullable: false),
                    CustomerAccountID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostInitiatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GLAccountID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TellerPosting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TellerPosting_AspNetUsers_PostInitiatorId",
                        column: x => x.PostInitiatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TellerPosting_CustomerAccount_CustomerAccountID",
                        column: x => x.CustomerAccountID,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TellerPosting_GLAccount_GLAccountID",
                        column: x => x.GLAccountID,
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_CustomerAccountID",
                table: "TellerPosting",
                column: "CustomerAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_GLAccountID",
                table: "TellerPosting",
                column: "GLAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_PostInitiatorId",
                table: "TellerPosting",
                column: "PostInitiatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TellerPosting");
        }
    }
}
