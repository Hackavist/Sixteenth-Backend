using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Addresses_AddressId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_AddressId",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BranchId",
                table: "Addresses",
                column: "BranchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_BranchId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_AddressId",
                table: "Branches",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Addresses_AddressId",
                table: "Branches",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
