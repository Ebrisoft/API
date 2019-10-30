using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "013f791e-a702-460a-bd36-e711f6d88187");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2ada9d9-2f43-4fe8-972f-10c368556705");

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Issues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2ff4ea9-c18d-4a51-97f7-31cc83b873a5", "d01c2ea6-4388-4e5a-9048-6944bf1c3a50", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1afb7925-630b-43a2-9475-bbdcf934dc17", "822c39e2-dc0e-4c46-b7cf-d0ea4f1ee08f", "landlord", "LANDLORD" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_HouseId",
                table: "Issues",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Houses_HouseId",
                table: "Issues",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Houses_HouseId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Issues_HouseId",
                table: "Issues");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1afb7925-630b-43a2-9475-bbdcf934dc17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2ff4ea9-c18d-4a51-97f7-31cc83b873a5");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Issues");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f2ada9d9-2f43-4fe8-972f-10c368556705", "b6453ceb-5d45-4ff0-ace2-ce39fccf7255", "tenant", "TENANT" },
                    { "013f791e-a702-460a-bd36-e711f6d88187", "ceb00ddf-ebfa-46ab-8afd-fb0584d906b2", "landlord", "LANDLORD" }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "Content" },
                values: new object[,]
                {
                    { 1, "I am #1" },
                    { 2, "I am #2" }
                });
        }
    }
}
