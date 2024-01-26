using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdtoGroupChatModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "GroupChats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "GroupChats");
        }
    }
}
