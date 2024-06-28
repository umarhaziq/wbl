using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class AddFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6298225c-e8e5-4fc9-b94f-7218013e3a8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80860797-7622-4f10-8846-e584e3b31790");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "835af32e-bd3a-4400-bad4-7c09c6f80677");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e3a921a-6668-4d2d-b569-ac263bf2b0a8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45dde3a2-d798-4438-9940-ca65d538b471", null, "Therapist", "Therapist" },
                    { "7b6dfa6f-a770-45ee-a070-486523b90bde", null, "Parent", "Parent" },
                    { "8fd21529-754a-4525-910a-0aa8fcd366d4", null, "Admin", "Admin" },
                    { "c58b117d-6a4c-4e2a-95da-c31d41308d2f", null, "CustomerService", "CustomerService" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45dde3a2-d798-4438-9940-ca65d538b471");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b6dfa6f-a770-45ee-a070-486523b90bde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fd21529-754a-4525-910a-0aa8fcd366d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c58b117d-6a4c-4e2a-95da-c31d41308d2f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6298225c-e8e5-4fc9-b94f-7218013e3a8e", null, "CustomerService", "CustomerService" },
                    { "80860797-7622-4f10-8846-e584e3b31790", null, "Parent", "Parent" },
                    { "835af32e-bd3a-4400-bad4-7c09c6f80677", null, "Admin", "Admin" },
                    { "9e3a921a-6668-4d2d-b569-ac263bf2b0a8", null, "Therapist", "Therapist" }
                });
        }
    }
}
