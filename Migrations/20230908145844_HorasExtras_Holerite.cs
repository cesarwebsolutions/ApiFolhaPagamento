using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    public partial class HorasExtras_Holerite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "HorasNormais",
                table: "TbHolerite",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "HorasExtras",
                table: "TbHolerite",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorasExtras",
                table: "TbHolerite");

            migrationBuilder.AlterColumn<double>(
                name: "HorasNormais",
                table: "TbHolerite",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
