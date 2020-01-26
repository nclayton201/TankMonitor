using Microsoft.EntityFrameworkCore.Migrations;

namespace TankMonitor.Migrations
{
    public partial class addname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "43a2bb17-69a9-4693-abba-a887aa28b2df", "a8c05204-f32c-42b6-bd8d-3d9b6c0b5bfc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4e2b0dd7-f3cf-44f3-8e92-b9cd7016896b", "31af4ca7-620b-404c-8b74-930f65fcf2a7" });

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f882929-3163-4a4f-b396-234babfe838a", "4b42c017-1518-41d1-a0ce-e6d133cf4064", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e677cdbf-f69c-4a37-a092-720c6ab3b04d", "1cfe3a1d-55cc-4746-af82-93c58b9c2f24", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9f882929-3163-4a4f-b396-234babfe838a", "4b42c017-1518-41d1-a0ce-e6d133cf4064" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e677cdbf-f69c-4a37-a092-720c6ab3b04d", "1cfe3a1d-55cc-4746-af82-93c58b9c2f24" });

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43a2bb17-69a9-4693-abba-a887aa28b2df", "a8c05204-f32c-42b6-bd8d-3d9b6c0b5bfc", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e2b0dd7-f3cf-44f3-8e92-b9cd7016896b", "31af4ca7-620b-404c-8b74-930f65fcf2a7", "User", "USER" });
        }
    }
}
