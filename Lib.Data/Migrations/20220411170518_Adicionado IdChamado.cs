using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Data.Migrations
{
    public partial class AdicionadoIdChamado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProjeto",
                table: "ChamadoConta",
                newName: "IdChamado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdChamado",
                table: "ChamadoConta",
                newName: "IdProjeto");
        }
    }
}
