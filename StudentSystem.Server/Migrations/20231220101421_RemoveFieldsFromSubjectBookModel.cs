using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldsFromSubjectBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Books_BookId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_BookId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BookId",
                table: "Students",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Books_BookId",
                table: "Students",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
