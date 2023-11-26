using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    public partial class percentualinss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PercentualINSS",
                table: "TbHolerite",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PercentualIRRF",
                table: "TbHolerite",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentualINSS",
                table: "TbHolerite");

            migrationBuilder.DropColumn(
                name: "PercentualIRRF",
                table: "TbHolerite");
        }
    }
}
