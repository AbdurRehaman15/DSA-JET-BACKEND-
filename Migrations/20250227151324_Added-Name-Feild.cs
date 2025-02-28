using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsaJet.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameFeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Problems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Given two integer numbers find there sum", "Find the sum of two numbers" });

            migrationBuilder.UpdateData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Given an array of size n apply the binaru search algorithm on it", "Implement Binary Search" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Problems");

            migrationBuilder.UpdateData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Find the sum of two numbers");

            migrationBuilder.UpdateData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Implement Binary Search");
        }
    }
}
