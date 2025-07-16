using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AdminSystemUpdateFixed : Migration
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

            migrationBuilder.RenameColumn(
                name: "ComponentType",
                table: "Blood",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.AddColumn<double>(
                name: "Latitue",
                table: "UserMedicals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitue",
                table: "UserMedicals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "BloodRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "BloodRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "BloodRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitue",
                table: "BloodRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "BloodRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitue",
                table: "BloodRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "BloodRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BloodRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Blood",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Blood",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DonationDate",
                table: "Blood",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Blood",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReportData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Filters = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroupSettings",
                columns: table => new
                {
                    BloodType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MinimumStock = table.Column<int>(type: "int", nullable: true),
                    MaximumStock = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroupSettings", x => x.BloodType);
                });

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
                name: "ContactQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdminResponse = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    HandledBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactQueries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SeparatedBloodComponent",
                columns: table => new
                {
                    SeparatedBloodComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentType = table.Column<int>(type: "int", nullable: false),
                    VolumeInML = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeparatedBloodComponent", x => x.SeparatedBloodComponentId);
                    table.ForeignKey(
                        name: "FK_SeparatedBloodComponent_Blood_BloodId",
                        column: x => x.BloodId,
                        principalTable: "Blood",
                        principalColumn: "BloodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Key);
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
                columns: new[] { "UserId", "CreatedAt", "Email", "LastLoginDate", "Password", "Role", "Status", "UpdatedAt", "UserName" },
                values: new object[] { new Guid("c5d6e7f8-9a0b-1c2d-3e4f-5a6b7c8d9e0f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "user@gmail.com", null, "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "User", "Active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TargetUserId",
                table: "Notifications",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeparatedBloodComponent_BloodId",
                table: "SeparatedBloodComponent",
                column: "BloodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedicalChronicDisease_ChronicDiseaseId",
                table: "UserMedicalChronicDisease",
                column: "ChronicDiseaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminActivityLogs");

            migrationBuilder.DropTable(
                name: "AdminReports");

            migrationBuilder.DropTable(
                name: "BloodGroupSettings");

            migrationBuilder.DropTable(
                name: "ContactQueries");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SeparatedBloodComponent");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "UserMedicalChronicDisease");

            migrationBuilder.DropTable(
                name: "ChronicDisease");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c5d6e7f8-9a0b-1c2d-3e4f-5a6b7c8d9e0f"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

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

            migrationBuilder.DropColumn(
                name: "Latitue",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "Longtitue",
                table: "UserMedicals");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "Latitue",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "Longtitue",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Blood");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Blood");

            migrationBuilder.DropColumn(
                name: "DonationDate",
                table: "Blood");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Blood");

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

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Blood",
                newName: "ComponentType");

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
