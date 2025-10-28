using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class CheckBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40038396-c9dc-4b2a-9071-7dbd557db439", "AQAAAAIAAYagAAAAENtXT1BOKfdlbgBCfhGWRbunxuwwunxtWfqIvFrVyLKoDh7HDbmBUOgsjqsGIFALGg==", "7ddbec6c-82f0-493e-b4ea-55569ede1f04" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14f47afa-c8ba-4156-a203-ed17983cda02", "AQAAAAIAAYagAAAAENXAcTNkGAf2BlSw/hxXXLFGXIHcnB7PAgXd7wK0gefNxAYNE7R9+DMhyZvZXpxvbA==", "18456597-e4b4-42dd-a3ac-d0156fe3ecaa" });
        }
    }
}
