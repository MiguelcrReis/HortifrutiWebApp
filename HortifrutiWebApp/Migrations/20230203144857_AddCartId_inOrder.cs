using Microsoft.EntityFrameworkCore.Migrations;

namespace HortifrutiWebApp.Migrations
{
    public partial class AddCartId_inOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Orders");
        }
    }
}
