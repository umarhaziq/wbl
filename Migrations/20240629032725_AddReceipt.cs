using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class AddReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
                name: "Receipt",
                table: "Invoicess",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptContentType",
                table: "Invoicess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "661d3849-6fe1-4810-a739-cdf3a6341737", null, "CustomerService", "CustomerService" },
                    { "b9a015f7-159b-4a03-96d0-6a71691f9fa0", null, "Admin", "Admin" },
                    { "c78f4969-47c6-40e4-9d6e-2e67a7105fa8", null, "Therapist", "Therapist" },
                    { "e4eb22e4-ed40-493c-bfc6-28c8e12b1dc7", null, "Parent", "Parent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "661d3849-6fe1-4810-a739-cdf3a6341737");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9a015f7-159b-4a03-96d0-6a71691f9fa0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c78f4969-47c6-40e4-9d6e-2e67a7105fa8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4eb22e4-ed40-493c-bfc6-28c8e12b1dc7");

            migrationBuilder.DropColumn(
                name: "Receipt",
                table: "Invoicess");

            migrationBuilder.DropColumn(
                name: "ReceiptContentType",
                table: "Invoicess");

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
    }
}
