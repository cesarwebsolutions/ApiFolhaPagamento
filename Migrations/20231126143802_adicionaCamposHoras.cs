using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    public partial class adicionaCamposHoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorHorasExtras",
                table: "TbHolerite",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValorHorasNormais",
                table: "TbHolerite",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorHorasExtras",
                table: "TbHolerite");

            migrationBuilder.DropColumn(
                name: "ValorHorasNormais",
                table: "TbHolerite");
        }
    }
}
