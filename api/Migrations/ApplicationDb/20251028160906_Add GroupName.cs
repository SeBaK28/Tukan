using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddGroupName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameGroup",
                table: "FamillyDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33e7ed4a-94d3-45c1-8a8d-9ef1eabf450d", "AQAAAAIAAYagAAAAEOsYemr8iDQH4AxHD2TWRSiqT9FlkR3b3onQP+6C58Qof9otuw2cypFaiHugLuKsBQ==", "b50d283b-c26b-4ad0-aaec-26628723f5ab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameGroup",
                table: "FamillyDatas");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0b79ee0-e3f9-47e5-b797-4ab8604b32de", "AQAAAAIAAYagAAAAEP+YUSvEMSv5jI+srFbSuPNplkGu4gcfFUK4SEyorSwbluwzBKTvkIfGj0aeESLnRg==", "d767b2a1-74ad-4fee-9cc0-497fdfc40c60" });
        }
    }
}
