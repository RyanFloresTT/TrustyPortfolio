using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustyPortfolio.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_343 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2d56cd8-184d-4ba0-b25a-34f54a51b7c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c4ad020-b7d8-44bd-bf8d-b425bd3f031b", "AQAAAAIAAYagAAAAEISPhi07nY1Cz3tu7EdqJEY5aZWuReeJ9+oOLVgCVDhUSb460AIU7mi2BU6bJZNqmg==", "8c7f2e37-c7f5-4aa3-849d-f4d3725386e6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2d56cd8-184d-4ba0-b25a-34f54a51b7c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abd2d8af-77d5-4e55-b5d0-58362857152b", "AQAAAAIAAYagAAAAEBKEepsvnEVVY2GqRZJXePZdoRhCaTRo7jgHo90hymjX+3nxxZ41oaEjb8bBlyeAiw==", "b2e0a4c7-711f-4212-b519-663e3f46b74c" });
        }
    }
}
