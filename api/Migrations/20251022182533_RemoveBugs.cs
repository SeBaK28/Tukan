using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4");

            migrationBuilder.AddColumn<decimal>(
                name: "Expenses",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FamillyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyBuget",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Savings",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "FamillyData",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminGroupId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamillyData", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionData",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserDataId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOperation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionData", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_TransactionData_AspNetUsers_UserDataId",
                        column: x => x.UserDataId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            // migrationBuilder.CreateTable(
            //     name: "FamillyDataUserData",
            //     columns: table => new
            //     {
            //         FamillyGroupsGroupId = table.Column<int>(type: "int", nullable: false),
            //         MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_FamillyDataUserData", x => new { x.FamillyGroupsGroupId, x.MembersId });
            //         table.ForeignKey(
            //             name: "FK_FamillyDataUserData_AspNetUsers_MembersId",
            //             column: x => x.MembersId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_FamillyDataUserData_FamillyData_FamillyGroupsGroupId",
            //             column: x => x.FamillyGroupsGroupId,
            //             principalTable: "FamillyData",
            //             principalColumn: "GroupId",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            migrationBuilder.InsertData(
                table: "UserData",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4", 0, "3270f37b-fc93-4ebb-9914-31470d41c94c", "admin@tukano.com", false, false, null, "ADMIN@TUKANO.COM", "ADMIN@TUKANO.COM", "AQAAAAIAAYagAAAAEOyly4IVBK6Ksin6gjGqcnzYLbTlHlNu+wFM3Q3lCrdclId1+0/38/ngrgbC7aJMfQ==", null, false, "b7661d19-ed7d-42d0-a237-76f10e1259f8", false, "admin@tukano.com" });

            // migrationBuilder.CreateIndex(
            //     name: "IX_FamillyDataUserData_MembersId",
            //     table: "FamillyDataUserData",
            //     column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionData_UserDataId",
                table: "TransactionData",
                column: "UserDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamillyDataUserData");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DropTable(
                name: "TransactionData");

            migrationBuilder.DropTable(
                name: "FamillyData");

            migrationBuilder.DropColumn(
                name: "Expenses",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamillyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MonthlyBuget",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Savings",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4", 0, "ef39f5d9-85ea-4bda-8576-f4b542ae1d69", "admin@tukano.com", false, false, null, "ADMIN@TUKANO.COM", "ADMIN@TUKANO.COM", "AQAAAAIAAYagAAAAEDHSxASUpvC/KFVx9nEgpT4dZz1/kh7BtKig8hwj7lIQsShU/+DAONPqAUDfdsruiw==", null, false, "84cc90b8-ad83-45a9-b930-c14aec720bf9", false, "admin@tukano.com" });
        }
    }
}
