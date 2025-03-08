using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsaJet.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Problems_ProblemId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_ProblemId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "Solutions");

            migrationBuilder.AddColumn<string>(
                name: "Problem_Name",
                table: "Solutions",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Problems",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Problems_Name",
                table: "Problems",
                column: "Name");

            migrationBuilder.UpdateData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Problem_Name",
                value: "Find the sum of two numbers");

            migrationBuilder.UpdateData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Problem_Name",
                value: "Find the sum of two numbers");

            migrationBuilder.UpdateData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Problem_Name",
                value: "Implement Binary Search");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_Problem_Name",
                table: "Solutions",
                column: "Problem_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_Name",
                table: "Problems",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Problems_Problem_Name",
                table: "Solutions",
                column: "Problem_Name",
                principalTable: "Problems",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Problems_Problem_Name",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_Problem_Name",
                table: "Solutions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Problems_Name",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_Name",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "Problem_Name",
                table: "Solutions");

            migrationBuilder.AddColumn<int>(
                name: "ProblemId",
                table: "Solutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Problems",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProblemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProblemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Solutions",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProblemId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_ProblemId",
                table: "Solutions",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Problems_ProblemId",
                table: "Solutions",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
