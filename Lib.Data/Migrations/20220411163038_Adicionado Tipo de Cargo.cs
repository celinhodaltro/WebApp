using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Data.Migrations
{
    public partial class AdicionadoTipodeCargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "Nivel",
                table: "Cargos",
                newName: "IdTipoCargo");

            migrationBuilder.AddColumn<int>(
                name: "TipoConta",
                table: "Contas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoConta",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "IdTipoCargo",
                table: "Cargos",
                newName: "Nivel");

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Contas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
