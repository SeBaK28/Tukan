using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddFamillyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0b79ee0-e3f9-47e5-b797-4ab8604b32de", "AQAAAAIAAYagAAAAEP+YUSvEMSv5jI+srFbSuPNplkGu4gcfFUK4SEyorSwbluwzBKTvkIfGj0aeESLnRg==", "d767b2a1-74ad-4fee-9cc0-497fdfc40c60" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40038396-c9dc-4b2a-9071-7dbd557db439", "AQAAAAIAAYagAAAAENtXT1BOKfdlbgBCfhGWRbunxuwwunxtWfqIvFrVyLKoDh7HDbmBUOgsjqsGIFALGg==", "7ddbec6c-82f0-493e-b4ea-55569ede1f04" });
        }
    }
}
