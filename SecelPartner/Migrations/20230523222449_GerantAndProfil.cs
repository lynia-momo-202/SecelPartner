using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecelPartner.UI.Migrations
{
    public partial class GerantAndProfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gerants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContratPartenariatId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gerants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gerants_ContratPartenariat_ContratPartenariatId",
                        column: x => x.ContratPartenariatId,
                        principalTable: "ContratPartenariat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gerants_ContratPartenariatId",
                table: "Gerants",
                column: "ContratPartenariatId");

            migrationBuilder.CreateIndex(
                name: "IX_Gerants_UsersId",
                table: "Gerants",
                column: "UserId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "Gerants");
                  
            migrationBuilder.DropColumn(
                name: "ProfilName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilPath",
                table: "AspNetUsers");
        }
    }
}
