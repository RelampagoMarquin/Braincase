using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class correcaoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "User",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "Test",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Test",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "Comment",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Justify",
                table: "Answer",
                type: "longtext",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 2147483647)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserId",
                table: "User",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_UserId",
                table: "Test",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_QuestionId",
                table: "Comment",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Question_QuestionId",
                table: "Comment",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Test_User_UserId",
                table: "Test",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UserId",
                table: "User",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Question_QuestionId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_User_UserId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Test_UserId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Comment_QuestionId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Comment");

            migrationBuilder.UpdateData(
                table: "Test",
                keyColumn: "LogoUrl",
                keyValue: null,
                column: "LogoUrl",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "Test",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Justify",
                keyValue: null,
                column: "Justify",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Justify",
                table: "Answer",
                type: "longtext",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 2147483647,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
