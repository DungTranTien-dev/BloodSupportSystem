using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class abcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_RequestedByUserId",
                table: "BloodRequests",
                column: "RequestedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodRequests_Users_RequestedByUserId",
                table: "BloodRequests",
                column: "RequestedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodRequests_Users_RequestedByUserId",
                table: "BloodRequests");

            migrationBuilder.DropIndex(
                name: "IX_BloodRequests_RequestedByUserId",
                table: "BloodRequests");
        }
    }
}
