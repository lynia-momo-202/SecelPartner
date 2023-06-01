using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecelPartner.UI.Migrations
{
    public partial class Validation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Gerants_AspNetUsers_UserId",
                table: "Gerants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Gerants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Gerants_UserId",
                table: "Gerants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gerants_AspNetUsers_UserId",
                table: "Gerants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avantage_Partenariat_PartenariatId",
                table: "Avantage");

            migrationBuilder.DropForeignKey(
                name: "FK_Condition_Partenariat_PartenariatId",
                table: "Condition");

            migrationBuilder.DropForeignKey(
                name: "FK_ConditionRenouvelement_Partenariat_PartenariatId",
                table: "ConditionRenouvelement");

            migrationBuilder.DropForeignKey(
                name: "FK_Gerants_AspNetUsers_UserId",
                table: "Gerants");

            migrationBuilder.DropIndex(
                name: "IX_Gerants_UserId",
                table: "Gerants");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "TypePartenariat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Partenariat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Partenaire",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Partenaire",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "NiveauPartenariat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Gerants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Gerants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "ContratPartenariat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Poste",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "ConditionRenouvelement",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "Condition",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Condition",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "Avantage",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Gerants_UsersId",
                table: "Gerants",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avantage_Partenariat_PartenariatId",
                table: "Avantage",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Condition_Partenariat_PartenariatId",
                table: "Condition",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConditionRenouvelement_Partenariat_PartenariatId",
                table: "ConditionRenouvelement",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gerants_AspNetUsers_UsersId",
                table: "Gerants",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
