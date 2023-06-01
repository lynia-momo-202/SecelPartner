using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecelPartner.Infrastructure.Migrations
{
    public partial class Fichier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoName",
                table: "Partenaire",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Partenaire",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoName",
                table: "Partenaire");

            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Partenaire");

            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Contact");
        }
    }
}
