using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class BulkedOutIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d6113bc-5479-493d-9411-700719e8f658");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4ce83a0-3615-4808-982c-276dff30c217");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Issues",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Issues",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                table: "Issues",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Issues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Issues",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7a16290b-c4ed-466c-a430-9a2bcbb8c303", "cbde1637-17e5-4ddf-af4a-737580a422b8", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb3de699-2657-4e78-9cbe-69a438178d13", "c1b2a2ab-b791-4f9b-a304-4d03c7f785f1", "landlord", "LANDLORD" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AuthorId",
                table: "Issues",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_AuthorId",
                table: "Issues",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_AuthorId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AuthorId",
                table: "Issues");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a16290b-c4ed-466c-a430-9a2bcbb8c303");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb3de699-2657-4e78-9cbe-69a438178d13");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsResolved",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Issues");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f4ce83a0-3615-4808-982c-276dff30c217", "30b36a68-dd2e-453a-9de0-b7f5eadda922", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9d6113bc-5479-493d-9411-700719e8f658", "f86fc83a-1bec-4742-b19f-58a115081b89", "landlord", "LANDLORD" });
        }
    }
}
