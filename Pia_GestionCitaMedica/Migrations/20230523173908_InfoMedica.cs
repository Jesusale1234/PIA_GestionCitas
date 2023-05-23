using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pia_GestionCitaMedica.Migrations
{
    /// <inheritdoc />
    public partial class InfoMedica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_InfoMedica",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InfoMedicaId_InfoMedica",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InfoMedica",
                columns: table => new
                {
                    Id_InfoMedica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    EnfermedadesAnt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoMedica", x => x.Id_InfoMedica);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_InfoMedicaId_InfoMedica",
                table: "Pacientes",
                column: "InfoMedicaId_InfoMedica");


            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_InfoMedica_InfoMedicaId_InfoMedica",
                table: "Pacientes",
                column: "InfoMedicaId_InfoMedica",
                principalTable: "InfoMedica",
                principalColumn: "Id_InfoMedica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_InfoMedica_InfoMedicaId_InfoMedica",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "InfoMedica");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_InfoMedicaId_InfoMedica",
                table: "Pacientes");


            migrationBuilder.DropColumn(
                name: "Id_InfoMedica",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "InfoMedicaId_InfoMedica",
                table: "Pacientes");
        }
    }
}
