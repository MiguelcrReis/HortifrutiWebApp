using Microsoft.EntityFrameworkCore.Migrations;

namespace HortifrutiWebApp.Migrations
{
    public partial class CreatedAddressandClientStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_Cep",
                table: "Clients",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Clients",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Complement",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Neighborhood",
                table: "Clients",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                table: "Clients",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Reference",
                table: "Clients",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Clients",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Clients",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientStatus",
                table: "Clients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Cep",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_Complement",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_Neighborhood",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_Reference",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientStatus",
                table: "Clients");
        }
    }
}
