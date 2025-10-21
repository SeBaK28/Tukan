using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpdateDomainClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FamillyId",
                table: "UserDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AdminGroupId",
                table: "FamillyDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminGroupId",
                table: "FamillyDatas");

            migrationBuilder.AlterColumn<int>(
                name: "FamillyId",
                table: "UserDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
