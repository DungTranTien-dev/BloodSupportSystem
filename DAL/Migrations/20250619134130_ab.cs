using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserMedicals_BloodId",
                table: "UserMedicals");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedicals_BloodId",
                table: "UserMedicals",
                column: "BloodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserMedicals_BloodId",
                table: "UserMedicals");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedicals_BloodId",
                table: "UserMedicals",
                column: "BloodId",
                unique: true);
        }
    }
}
