using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecelPartner.Infrastructure.Migrations
{
    public partial class Validations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_ContratPartenariat_Partenariat_PartenariatId",
                table: "ContratPartenariat");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "TypePartenariat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Partenariat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Partenaire",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Partenaire",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "NiveauPartenariat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "ContratPartenariat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "ContratPartenariat",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Poste",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "ConditionRenouvelement",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "Condition",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Condition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "Avantage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<int>(type: "int", nullable: false),
                    ToEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Avantage_Partenariat_PartenariatId",
                table: "Avantage",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Condition_Partenariat_PartenariatId",
                table: "Condition",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConditionRenouvelement_Partenariat_PartenariatId",
                table: "ConditionRenouvelement",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratPartenariat_Partenariat_PartenariatId",
                table: "ContratPartenariat",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_ContratPartenariat_Partenariat_PartenariatId",
                table: "ContratPartenariat");

            migrationBuilder.DropTable(
                name: "Email");

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
                name: "Titre",
                table: "ContratPartenariat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartenariatId",
                table: "ContratPartenariat",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                name: "FK_ContratPartenariat_Partenariat_PartenariatId",
                table: "ContratPartenariat",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id");
        }
    }
}
