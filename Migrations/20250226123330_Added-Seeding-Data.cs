using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsaJet.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prerequisite_Problems_ProblemId",
                table: "Prerequisite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prerequisite",
                table: "Prerequisite");

            migrationBuilder.RenameTable(
                name: "Prerequisite",
                newName: "Prerequisites");

            migrationBuilder.RenameIndex(
                name: "IX_Prerequisite_ProblemId",
                table: "Prerequisites",
                newName: "IX_Prerequisites_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prerequisites",
                table: "Prerequisites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequisites_Problems_ProblemId",
                table: "Prerequisites",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prerequisites_Problems_ProblemId",
                table: "Prerequisites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prerequisites",
                table: "Prerequisites");

            migrationBuilder.RenameTable(
                name: "Prerequisites",
                newName: "Prerequisite");

            migrationBuilder.RenameIndex(
                name: "IX_Prerequisites_ProblemId",
                table: "Prerequisite",
                newName: "IX_Prerequisite_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prerequisite",
                table: "Prerequisite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequisite_Problems_ProblemId",
                table: "Prerequisite",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
