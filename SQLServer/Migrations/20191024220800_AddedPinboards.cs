using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedPinboards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c221fc-c4d8-41dc-9fae-acce71d70c23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f883f619-f8dc-4287-88fa-7ecdf356998a");

            migrationBuilder.AddColumn<string>(
                name: "Pinboard",
                table: "Houses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f4ce83a0-3615-4808-982c-276dff30c217", "30b36a68-dd2e-453a-9de0-b7f5eadda922", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9d6113bc-5479-493d-9411-700719e8f658", "f86fc83a-1bec-4742-b19f-58a115081b89", "landlord", "LANDLORD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d6113bc-5479-493d-9411-700719e8f658");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4ce83a0-3615-4808-982c-276dff30c217");

            migrationBuilder.DropColumn(
                name: "Pinboard",
                table: "Houses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f883f619-f8dc-4287-88fa-7ecdf356998a", "208b4de7-823e-4966-ba7c-be7b71797057", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49c221fc-c4d8-41dc-9fae-acce71d70c23", "9b072e29-f263-44a4-b78f-9f6374f8c048", "landlord", "LANDLORD" });
        }
    }
}
