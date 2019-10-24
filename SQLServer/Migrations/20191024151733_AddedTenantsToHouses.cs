using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedTenantsToHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "351372c5-c734-44c0-a76c-8d32a74e7476");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66c3d71c-93c4-41cb-bab3-c417ee9b0b6f");

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f883f619-f8dc-4287-88fa-7ecdf356998a", "208b4de7-823e-4966-ba7c-be7b71797057", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49c221fc-c4d8-41dc-9fae-acce71d70c23", "9b072e29-f263-44a4-b78f-9f6374f8c048", "landlord", "LANDLORD" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HouseId",
                table: "AspNetUsers",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Houses_HouseId",
                table: "AspNetUsers",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Houses_HouseId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HouseId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c221fc-c4d8-41dc-9fae-acce71d70c23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f883f619-f8dc-4287-88fa-7ecdf356998a");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "66c3d71c-93c4-41cb-bab3-c417ee9b0b6f", "022dac6f-7c86-48bc-9288-6fc0e8573c1a", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "351372c5-c734-44c0-a76c-8d32a74e7476", "d5f77636-395c-4f7a-be56-8b6f5457aa6a", "landlord", "LANDLORD" });
        }
    }
}
