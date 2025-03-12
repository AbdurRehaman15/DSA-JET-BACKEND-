using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DsaJet.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefreherTokenEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prerequisites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prerequisites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JwtRefresher = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.InsertData(
                table: "Problems",
                columns: new[] { "Id", "Description", "Difficulty", "Name", "Tag", "VideoSolutionUrl" },
                values: new object[,]
                {
                    { 1, "Given two integer numbers find there sum", "Easy", "Find the sum of two numbers", "Math", "https://example.com/sum" },
                    { 2, "Given an array of size n apply the binaru search algorithm on it", "Medium", "Implement Binary Search", "Search", "https://example.com/binary-search" }
                });

            migrationBuilder.InsertData(
                table: "Prerequisites",
                columns: new[] { "Id", "Prereq", "ProblemId" },
                values: new object[,]
                {
                    { 1, "Sorting Algorithms", 2 },
                    { 2, "Arrays", 2 }
                });

            migrationBuilder.InsertData(
                table: "Solutions",
                columns: new[] { "Id", "Language", "Problem_Name", "SolutionCode" },
                values: new object[,]
                {
                    { 1, "Python", "Find the sum of two numbers", "def sum(a, b): return a + b" },
                    { 2, "C++", "Find the sum of two numbers", "int sum(int a, int b) { return a + b; }" },
                    { 3, "Java", "Implement Binary Search", "int binarySearch(int[] arr, int x) { /* code */ }" }
                });
        }
    }
}
