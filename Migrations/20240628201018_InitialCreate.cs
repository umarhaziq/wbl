using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a4682be-3a9c-4753-8658-54e7707cdf60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d22a82b-1b16-4ac9-8e13-6bac25ad76fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca86fb60-0a23-4f0a-b786-722b0b5e2956");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcc29cfa-d8af-429a-a492-b8f8a46d3d75");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Invoicess");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d25f658-7be6-4dc7-8a5b-ad3fe3efc519", null, "Parent", "Parent" },
                    { "4a0d893d-3671-4362-9229-a632baca99c3", null, "CustomerService", "CustomerService" },
                    { "928f52ca-b860-40d3-9674-a88016f2d8b4", null, "Admin", "Admin" },
                    { "f11925aa-38eb-423a-a08b-ec8488e95133", null, "Therapist", "Therapist" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d25f658-7be6-4dc7-8a5b-ad3fe3efc519");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a0d893d-3671-4362-9229-a632baca99c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "928f52ca-b860-40d3-9674-a88016f2d8b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f11925aa-38eb-423a-a08b-ec8488e95133");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Invoicess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a4682be-3a9c-4753-8658-54e7707cdf60", null, "Therapist", "Therapist" },
                    { "4d22a82b-1b16-4ac9-8e13-6bac25ad76fb", null, "Admin", "Admin" },
                    { "ca86fb60-0a23-4f0a-b786-722b0b5e2956", null, "CustomerService", "CustomerService" },
                    { "fcc29cfa-d8af-429a-a492-b8f8a46d3d75", null, "Parent", "Parent" }
                });
        }
    }
}
