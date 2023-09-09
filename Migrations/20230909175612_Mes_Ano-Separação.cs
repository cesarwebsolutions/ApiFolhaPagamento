using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    public partial class Mes_AnoSeparação : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MesAno",
                table: "TbHolerite",
                newName: "Mes");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "TbHolerite",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "TbHolerite");

            migrationBuilder.RenameColumn(
                name: "Mes",
                table: "TbHolerite",
                newName: "MesAno");
        }
    }
}
