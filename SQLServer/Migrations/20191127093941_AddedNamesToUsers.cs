using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedNamesToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a16290b-c4ed-466c-a430-9a2bcbb8c303");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb3de699-2657-4e78-9cbe-69a438178d13");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c929d2eb-4d69-45e1-8fe4-80a586b4ab80", "10fa074e-736f-4c7d-b013-49efc7711c82", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84c248a2-1bf9-4181-bd1b-32efecbd66e5", "3df37124-e136-4a42-99ea-c3b57192fa97", "landlord", "LANDLORD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84c248a2-1bf9-4181-bd1b-32efecbd66e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c929d2eb-4d69-45e1-8fe4-80a586b4ab80");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7a16290b-c4ed-466c-a430-9a2bcbb8c303", "cbde1637-17e5-4ddf-af4a-737580a422b8", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb3de699-2657-4e78-9cbe-69a438178d13", "c1b2a2ab-b791-4f9b-a304-4d03c7f785f1", "landlord", "LANDLORD" });
        }
    }
}
