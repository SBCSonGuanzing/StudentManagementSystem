using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveStatusToUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Users");
        }
    }
}
