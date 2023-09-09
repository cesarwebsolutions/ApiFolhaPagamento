using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    public partial class CargoColaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbColaborador_Usuarios_UsuarioId",
                table: "TbColaborador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "TbUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbUsuario",
                table: "TbUsuario",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TbCargoColaborador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Funcao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCargoColaborador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbTipoHolerite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoHolerite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTipoHolerite", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TbColaborador_TbUsuario_UsuarioId",
                table: "TbColaborador",
                column: "UsuarioId",
                principalTable: "TbUsuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbColaborador_TbUsuario_UsuarioId",
                table: "TbColaborador");

            migrationBuilder.DropTable(
                name: "TbCargoColaborador");

            migrationBuilder.DropTable(
                name: "TbTipoHolerite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbUsuario",
                table: "TbUsuario");

            migrationBuilder.RenameTable(
                name: "TbUsuario",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbColaborador_Usuarios_UsuarioId",
                table: "TbColaborador",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
