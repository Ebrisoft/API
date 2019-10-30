using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedOwningHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1afb7925-630b-43a2-9475-bbdcf934dc17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2ff4ea9-c18d-4a51-97f7-31cc83b873a5");

            migrationBuilder.AddColumn<string>(
                name: "LandlordId",
                table: "Houses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "66c3d71c-93c4-41cb-bab3-c417ee9b0b6f", "022dac6f-7c86-48bc-9288-6fc0e8573c1a", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "351372c5-c734-44c0-a76c-8d32a74e7476", "d5f77636-395c-4f7a-be56-8b6f5457aa6a", "landlord", "LANDLORD" });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_LandlordId",
                table: "Houses",
                column: "LandlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_LandlordId",
                table: "Houses",
                column: "LandlordId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_LandlordId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_LandlordId",
                table: "Houses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "351372c5-c734-44c0-a76c-8d32a74e7476");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66c3d71c-93c4-41cb-bab3-c417ee9b0b6f");

            migrationBuilder.DropColumn(
                name: "LandlordId",
                table: "Houses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2ff4ea9-c18d-4a51-97f7-31cc83b873a5", "d01c2ea6-4388-4e5a-9048-6944bf1c3a50", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1afb7925-630b-43a2-9475-bbdcf934dc17", "822c39e2-dc0e-4c46-b7cf-d0ea4f1ee08f", "landlord", "LANDLORD" });
        }
    }
}
