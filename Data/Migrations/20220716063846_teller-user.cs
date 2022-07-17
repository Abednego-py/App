using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class telleruser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TellerPosting_AspNetUsers_PostInitiatorId",
                table: "TellerPosting");

            migrationBuilder.DropIndex(
                name: "IX_TellerPosting_PostInitiatorId",
                table: "TellerPosting");

            migrationBuilder.DropColumn(
                name: "PostInitiatorId",
                table: "TellerPosting");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TellerPosting",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_UserId",
                table: "TellerPosting",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TellerPosting_AspNetUsers_UserId",
                table: "TellerPosting",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TellerPosting_AspNetUsers_UserId",
                table: "TellerPosting");

            migrationBuilder.DropIndex(
                name: "IX_TellerPosting_UserId",
                table: "TellerPosting");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TellerPosting",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PostInitiatorId",
                table: "TellerPosting",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_PostInitiatorId",
                table: "TellerPosting",
                column: "PostInitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TellerPosting_AspNetUsers_PostInitiatorId",
                table: "TellerPosting",
                column: "PostInitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
