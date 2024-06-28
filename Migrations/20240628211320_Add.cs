using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: "2fbdf563-9ed7-4d7d-bf49-01c3b0651a19");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b19fbb68-8731-42c1-a6e8-9a135fe32eff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cad7eea9-fba7-4195-aead-e0cd4f8c3589");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f28cce39-615e-4a0a-88cf-e4bfe1825e01");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fbdf563-9ed7-4d7d-bf49-01c3b0651a19", null, "Admin", "Admin" },
                    { "b19fbb68-8731-42c1-a6e8-9a135fe32eff", null, "CustomerService", "CustomerService" },
                    { "cad7eea9-fba7-4195-aead-e0cd4f8c3589", null, "Therapist", "Therapist" },
                    { "f28cce39-615e-4a0a-88cf-e4bfe1825e01", null, "Parent", "Parent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_child_Id",
                table: "tb_child",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_child_Invoicess_Id",
                table: "tb_child",
                column: "Id",
                principalTable: "Invoicess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
