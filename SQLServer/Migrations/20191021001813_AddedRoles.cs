using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2ada9d9-2f43-4fe8-972f-10c368556705", "b6453ceb-5d45-4ff0-ace2-ce39fccf7255", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "013f791e-a702-460a-bd36-e711f6d88187", "ceb00ddf-ebfa-46ab-8afd-fb0584d906b2", "landlord", "LANDLORD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "013f791e-a702-460a-bd36-e711f6d88187");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2ada9d9-2f43-4fe8-972f-10c368556705");
        }
    }
}
