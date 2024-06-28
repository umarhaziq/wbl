using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c084a9a-23f1-4637-8dd7-c74602e97319");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6df5927d-523f-446c-bf3f-37f3bcb9551e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4fea969-d57d-47ee-9cfb-3d56e30b7f7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f65140c7-5159-4037-beeb-542ba0b94c6e");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tb_child",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_tb_child_Id",
                table: "tb_child",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_child_Invoicess_Id",
                table: "tb_child",
                column: "Id",
                principalTable: "Invoicess",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_child_Invoicess_Id",
                table: "tb_child");

            migrationBuilder.DropIndex(
                name: "IX_tb_child_Id",
                table: "tb_child");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tb_child");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c084a9a-23f1-4637-8dd7-c74602e97319", null, "Parent", "Parent" },
                    { "6df5927d-523f-446c-bf3f-37f3bcb9551e", null, "Therapist", "Therapist" },
                    { "e4fea969-d57d-47ee-9cfb-3d56e30b7f7a", null, "Admin", "Admin" },
                    { "f65140c7-5159-4037-beeb-542ba0b94c6e", null, "CustomerService", "CustomerService" }
                });
        }
    }
}
