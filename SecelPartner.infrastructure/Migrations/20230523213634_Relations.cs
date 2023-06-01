using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecelPartner.Infrastructure.Migrations
{
    public partial class Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NiveauPartenariatId",
                table: "Partenariat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypePartenariatId",
                table: "Partenariat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartenaireId",
                table: "ContratPartenariat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PartenariatId",
                table: "ContratPartenariat",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartenaireId",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PartenariatId",
                table: "ConditionRenouvelement",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartenariatId",
                table: "Condition",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartenariatId",
                table: "Avantage",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partenariat_NiveauPartenariatId",
                table: "Partenariat",
                column: "NiveauPartenariatId");

            migrationBuilder.CreateIndex(
                name: "IX_Partenariat_TypePartenariatId",
                table: "Partenariat",
                column: "TypePartenariatId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratPartenariat_PartenaireId",
                table: "ContratPartenariat",
                column: "PartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratPartenariat_PartenariatId",
                table: "ContratPartenariat",
                column: "PartenariatId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PartenaireId",
                table: "Contact",
                column: "PartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRenouvelement_PartenariatId",
                table: "ConditionRenouvelement",
                column: "PartenariatId");

            migrationBuilder.CreateIndex(
                name: "IX_Condition_PartenariatId",
                table: "Condition",
                column: "PartenariatId");

            migrationBuilder.CreateIndex(
                name: "IX_Avantage_PartenariatId",
                table: "Avantage",
                column: "PartenariatId");

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
                name: "FK_Contact_Partenaire_PartenaireId",
                table: "Contact",
                column: "PartenaireId",
                principalTable: "Partenaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratPartenariat_Partenaire_PartenaireId",
                table: "ContratPartenariat",
                column: "PartenaireId",
                principalTable: "Partenaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratPartenariat_Partenariat_PartenariatId",
                table: "ContratPartenariat",
                column: "PartenariatId",
                principalTable: "Partenariat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partenariat_NiveauPartenariat_NiveauPartenariatId",
                table: "Partenariat",
                column: "NiveauPartenariatId",
                principalTable: "NiveauPartenariat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partenariat_TypePartenariat_TypePartenariatId",
                table: "Partenariat",
                column: "TypePartenariatId",
                principalTable: "TypePartenariat",
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
                name: "FK_Contact_Partenaire_PartenaireId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratPartenariat_Partenaire_PartenaireId",
                table: "ContratPartenariat");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratPartenariat_Partenariat_PartenariatId",
                table: "ContratPartenariat");

            migrationBuilder.DropForeignKey(
                name: "FK_Partenariat_NiveauPartenariat_NiveauPartenariatId",
                table: "Partenariat");

            migrationBuilder.DropForeignKey(
                name: "FK_Partenariat_TypePartenariat_TypePartenariatId",
                table: "Partenariat");

            migrationBuilder.DropIndex(
                name: "IX_Partenariat_NiveauPartenariatId",
                table: "Partenariat");

            migrationBuilder.DropIndex(
                name: "IX_Partenariat_TypePartenariatId",
                table: "Partenariat");

            migrationBuilder.DropIndex(
                name: "IX_ContratPartenariat_PartenaireId",
                table: "ContratPartenariat");

            migrationBuilder.DropIndex(
                name: "IX_ContratPartenariat_PartenariatId",
                table: "ContratPartenariat");

            migrationBuilder.DropIndex(
                name: "IX_Contact_PartenaireId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_ConditionRenouvelement_PartenariatId",
                table: "ConditionRenouvelement");

            migrationBuilder.DropIndex(
                name: "IX_Condition_PartenariatId",
                table: "Condition");

            migrationBuilder.DropIndex(
                name: "IX_Avantage_PartenariatId",
                table: "Avantage");

            migrationBuilder.DropColumn(
                name: "NiveauPartenariatId",
                table: "Partenariat");

            migrationBuilder.DropColumn(
                name: "TypePartenariatId",
                table: "Partenariat");

            migrationBuilder.DropColumn(
                name: "PartenaireId",
                table: "ContratPartenariat");

            migrationBuilder.DropColumn(
                name: "PartenariatId",
                table: "ContratPartenariat");

            migrationBuilder.DropColumn(
                name: "PartenaireId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PartenariatId",
                table: "ConditionRenouvelement");

            migrationBuilder.DropColumn(
                name: "PartenariatId",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "PartenariatId",
                table: "Avantage");
        }
    }
}
