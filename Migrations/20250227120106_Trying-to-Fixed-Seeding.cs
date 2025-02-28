using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DsaJet.Api.Migrations
{
    /// <inheritdoc />
    public partial class TryingtoFixedSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Problems",
                columns: new[] { "Id", "Description", "Difficulty", "Tag", "VideoSolutionUrl" },
                values: new object[,]
                {
                    { 1, "Find the sum of two numbers", "Easy", "Math", "https://example.com/sum" },
                    { 2, "Implement Binary Search", "Medium", "Search", "https://example.com/binary-search" }
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
                columns: new[] { "Id", "Language", "ProblemId", "SolutionCode" },
                values: new object[,]
                {
                    { 1, "Python", 1, "def sum(a, b): return a + b" },
                    { 2, "C++", 1, "int sum(int a, int b) { return a + b; }" },
                    { 3, "Java", 2, "int binarySearch(int[] arr, int x) { /* code */ }" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
