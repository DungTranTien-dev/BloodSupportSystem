using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasTransmissibleDisease",
                table: "UserMedicals");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "UserMedicals",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserMedicals",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "LastDonationDate",
                table: "UserMedicals",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "IsTakingMedication",
                table: "UserMedicals",
                newName: "HasDonatedBefore");

            migrationBuilder.RenameColumn(
                name: "CurrentHealthStatus",
                table: "UserMedicals",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<int>(
                name: "DonationCount",
                table: "UserMedicals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CitizenId",
                table: "UserMedicals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentAddress",
                table: "UserMedicals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiseaseDescription",
                table: "UserMedicals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserMedicals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "UserMedicals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "UserMedicals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChronicDisease",
                columns: table => new
                {
                    ChronicDiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChronicDiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChronicDisease", x => x.ChronicDiseaseId);
                });

            migrationBuilder.CreateTable(
                name: "UserMedicalChronicDisease",
                columns: table => new
                {
                    UserMedicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChronicDiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserMedicalChronicDiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMedicalChronicDisease", x => new { x.UserMedicalId, x.ChronicDiseaseId });
                    table.ForeignKey(
                        name: "FK_UserMedicalChronicDisease_ChronicDisease_ChronicDiseaseId",
                        column: x => x.ChronicDiseaseId,
                        principalTable: "ChronicDisease",
                        principalColumn: "ChronicDiseaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMedicalChronicDisease_UserMedicals_UserMedicalId",
                        column: x => x.UserMedicalId,
                        principalTable: "UserMedicals",
                        principalColumn: "UserMedicalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChronicDisease",
                columns: new[] { "ChronicDiseaseId", "ChronicDiseaseName" },
                values: new object[,]
                {
                    { new Guid("a1e2c3d4-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), "Tiểu đường" },
                    { new Guid("a7b8c9d0-1e2f-3a4b-5c6d-7e8f9a0b1c2d"), "Ung thư" },
                    { new Guid("b2d3e4f5-6a7b-8c9d-0e1f-2a3b4c5d6e7f"), "Cao huyết áp" },
                    { new Guid("b8c9d0e1-2f3a-4b5c-6d7e-8f9a0b1c2d3e"), "HIV/AIDS" },
                    { new Guid("c3d4e5f6-7a8b-9c0d-1e2f-3a4b5c6d7e8f"), "Bệnh tim mạch" },
                    { new Guid("c9d0e1f2-3a4b-5c6d-7e8f-9a0b1c2d3e4f"), "Viêm gan B/C" },
                    { new Guid("d0e1f2a3-4b5c-6d7e-8f9a-0b1c2d3e4f5a"), "Khác" },
                    { new Guid("d4e5f6a7-8b9c-0d1e-2f3a-4b5c6d7e8f9a"), "Hen suyễn" },
                    { new Guid("e5f6a7b8-9c0d-1e2f-3a4b-5c6d7e8f9a0b"), "Bệnh thận" },
                    { new Guid("f6a7b8c9-0d1e-2f3a-4b5c-6d7e8f9a0b1c"), "Bệnh gan" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "UserName" },
                values: new object[] { new Guid("c5d6e7f8-9a0b-1c2d-3e4f-5a6b7c8d9e0f"), "user@gmail.com", "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_UserMedicalChronicDisease_ChronicDiseaseId",
                table: "UserMedicalChronicDisease",
                column: "ChronicDiseaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMedicalChronicDisease");

            migrationBuilder.DropTable(
                name: "ChronicDisease");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c5d6e7f8-9a0b-1c2d-3e4f-5a6b7c8d9e0f"));

            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "CurrentAddress",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "DiseaseDescription",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserMedicals");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "UserMedicals",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "UserMedicals",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "UserMedicals",
                newName: "CurrentHealthStatus");

            migrationBuilder.RenameColumn(
                name: "HasDonatedBefore",
                table: "UserMedicals",
                newName: "IsTakingMedication");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "UserMedicals",
                newName: "LastDonationDate");

            migrationBuilder.AlterColumn<int>(
                name: "DonationCount",
                table: "UserMedicals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasTransmissibleDisease",
                table: "UserMedicals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
