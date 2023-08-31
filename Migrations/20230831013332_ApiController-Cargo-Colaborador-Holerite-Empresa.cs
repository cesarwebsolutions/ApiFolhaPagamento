using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    public partial class ApiControllerCargoColaboradorHoleriteEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEmpresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbColaborador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalarioBase = table.Column<double>(type: "float", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDemissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dependentes = table.Column<int>(type: "int", nullable: true),
                    Filhos = table.Column<int>(type: "int", nullable: true),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    EmpresaModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbColaborador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbColaborador_TbCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "TbCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbColaborador_TbEmpresa_EmpresaModelId",
                        column: x => x.EmpresaModelId,
                        principalTable: "TbEmpresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbColaborador_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbHolerite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false),
                    MesAno = table.Column<int>(type: "int", nullable: false),
                    SalarioBruto = table.Column<double>(type: "float", nullable: false),
                    DescontoINSS = table.Column<double>(type: "float", nullable: false),
                    DescontoIRRF = table.Column<double>(type: "float", nullable: false),
                    HorasNormais = table.Column<double>(type: "float", nullable: false),
                    SalarioLiquido = table.Column<double>(type: "float", nullable: false),
                    DependentesHolerite = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbHolerite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbHolerite_TbColaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "TbColaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbColaborador_CargoId",
                table: "TbColaborador",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_TbColaborador_EmpresaModelId",
                table: "TbColaborador",
                column: "EmpresaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TbColaborador_UsuarioId",
                table: "TbColaborador",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TbHolerite_ColaboradorId",
                table: "TbHolerite",
                column: "ColaboradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbHolerite");

            migrationBuilder.DropTable(
                name: "TbColaborador");

            migrationBuilder.DropTable(
                name: "TbCargo");

            migrationBuilder.DropTable(
                name: "TbEmpresa");
        }
    }
}
