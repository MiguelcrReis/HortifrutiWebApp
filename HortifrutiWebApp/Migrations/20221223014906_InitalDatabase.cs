using Microsoft.EntityFrameworkCore.Migrations;

namespace HortifrutiWebApp.Migrations
{
    public partial class InitalDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    IdProduct = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Descrition = table.Column<string>(maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.IdProduct);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
