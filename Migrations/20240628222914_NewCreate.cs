using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class NewCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "2dc1da8c-92bc-4bbd-a7a3-5652cec40f02", null, "CustomerService", "CustomerService" },
                    { "50ad8c70-5405-4ec5-9597-962424b005cc", null, "Admin", "Admin" },
                    { "5617dfee-6988-4897-8aa1-5ab3b13a8c57", null, "Parent", "Parent" },
                    { "f443e085-44df-4f25-8864-704b0e14a940", null, "Therapist", "Therapist" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dc1da8c-92bc-4bbd-a7a3-5652cec40f02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50ad8c70-5405-4ec5-9597-962424b005cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5617dfee-6988-4897-8aa1-5ab3b13a8c57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f443e085-44df-4f25-8864-704b0e14a940");

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
    }
}
