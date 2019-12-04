using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4086fb97-d4b2-464c-9fa5-c4998a50f0a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d458876e-c506-474f-94d1-9af2fefa8b90");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: false),
                    IssueId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e1d3034-952d-41e4-bec0-ed3556d0b248", "9147da59-8425-437a-81fa-712ad67158e4", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f33447a-2016-40fa-a7ec-b5c7e8e19164", "d67c81de-6be1-46c1-a458-7a2ff8330c38", "landlord", "LANDLORD" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IssueId",
                table: "Comments",
                column: "IssueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e1d3034-952d-41e4-bec0-ed3556d0b248");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f33447a-2016-40fa-a7ec-b5c7e8e19164");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d458876e-c506-474f-94d1-9af2fefa8b90", "537d47d1-30f2-48ca-bbf0-34dbb2711d5e", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4086fb97-d4b2-464c-9fa5-c4998a50f0a5", "27efe17e-0663-4f31-b445-484eec0c8125", "landlord", "LANDLORD" });
        }
    }
}
