using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auth.Migrations
{
    public partial class otpadded3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otp_User_userId",
                table: "Otp");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Otp",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Otp_userId",
                table: "Otp",
                newName: "IX_Otp_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Otp_User_UserId",
                table: "Otp",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otp_User_UserId",
                table: "Otp");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Otp",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Otp_UserId",
                table: "Otp",
                newName: "IX_Otp_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Otp_User_userId",
                table: "Otp",
                column: "userId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
