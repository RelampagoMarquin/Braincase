using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Question",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "isPrivate",
                table: "Question",
                newName: "IsPrivate");

            migrationBuilder.RenameColumn(
                name: "dificult",
                table: "Question",
                newName: "Dificult");

            migrationBuilder.RenameColumn(
                name: "own",
                table: "Favorites",
                newName: "Own");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comment",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Text = table.Column<string>(type: "longtext", maxLength: 2147483647, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Justify = table.Column<string>(type: "longtext", maxLength: 2147483647, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClassName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUse = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LogoUrl = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionTest",
                columns: table => new
                {
                    QuestionsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTest", x => new { x.QuestionsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_QuestionTest_Question_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTest_Test_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTest_TestsId",
                table: "QuestionTest",
                column: "TestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "QuestionTest");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Question",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "Question",
                newName: "isPrivate");

            migrationBuilder.RenameColumn(
                name: "Dificult",
                table: "Question",
                newName: "dificult");

            migrationBuilder.RenameColumn(
                name: "Own",
                table: "Favorites",
                newName: "own");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comment",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
