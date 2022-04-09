using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Data.Migrations
{
    public partial class AdicionadoCriadoraoprojeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCriador",
                table: "Projetos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCriador",
                table: "Projetos");
        }
    }
}
