using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class justifyCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Justify",
                table: "Answer");

            migrationBuilder.AddColumn<string>(
                name: "Justify",
                table: "Question",
                type: "longtext",
                maxLength: 2147483647,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tag",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Name",
                table: "Subject",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_Name",
                table: "Institution",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tag_Name",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Subject_Name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Institution_Name",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "Justify",
                table: "Question");

            migrationBuilder.AddColumn<string>(
                name: "Justify",
                table: "Answer",
                type: "longtext",
                maxLength: 2147483647,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
