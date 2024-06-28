using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wbl.Migrations
{
    /// <inheritdoc />
    public partial class AddChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "3d25f658-7be6-4dc7-8a5b-ad3fe3efc519", null, "Parent", "Parent" },
                    { "4a0d893d-3671-4362-9229-a632baca99c3", null, "CustomerService", "CustomerService" },
                    { "928f52ca-b860-40d3-9674-a88016f2d8b4", null, "Admin", "Admin" },
                    { "f11925aa-38eb-423a-a08b-ec8488e95133", null, "Therapist", "Therapist" }
                });
        }
    }
}
