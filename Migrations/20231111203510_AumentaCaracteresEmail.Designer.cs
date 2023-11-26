﻿// <auto-generated />
using System;
using ApiFolhaPagamento.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiFolhaPagamento.Migrations
{
    [DbContext(typeof(SistemaFolhaPagamentoDBContex))]
    [Migration("20231111203510_AumentaCaracteresEmail")]
    partial class AumentaCaracteresEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiFolhaPagamento.Models.BeneficioColaboradorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BeneficioId")
                        .HasColumnType("int");

                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TbBeneficioColaborador");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.BeneficioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("TbBeneficio");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.CargoColaboradorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TbCargoColaborador");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.CargoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TbCargo");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.ColaboradorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDemissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Dependentes")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<int?>("EmpresaModelId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Filhos")
                        .HasColumnType("int");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SalarioBase")
                        .HasColumnType("float");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EmpresaModelId");

                    b.ToTable("TbColaborador");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.EmpresaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("TbEmpresa");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.HoleriteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<int?>("DependentesHolerite")
                        .HasColumnType("int");

                    b.Property<double?>("DescontoINSS")
                        .HasColumnType("float");

                    b.Property<double?>("DescontoIRRF")
                        .HasColumnType("float");

                    b.Property<double?>("HorasExtras")
                        .HasColumnType("float");

                    b.Property<double?>("HorasNormais")
                        .HasColumnType("float");

                    b.Property<int>("Mes")
                        .HasColumnType("int");

                    b.Property<double?>("SalarioBruto")
                        .HasColumnType("float");

                    b.Property<double?>("SalarioLiquido")
                        .HasColumnType("float");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId");

                    b.ToTable("TbHolerite");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.Permissoes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Permissao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TbPermissoes");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.TiposHolerite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TipoHolerite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TbTipoHolerite");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PermissaoId")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TbUsuario");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.ColaboradorModel", b =>
                {
                    b.HasOne("ApiFolhaPagamento.Models.CargoModel", "Cargo")
                        .WithMany("Colaboradores")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiFolhaPagamento.Models.EmpresaModel", null)
                        .WithMany("Colaboradores")
                        .HasForeignKey("EmpresaModelId");

                    b.Navigation("Cargo");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.HoleriteModel", b =>
                {
                    b.HasOne("ApiFolhaPagamento.Models.ColaboradorModel", "Colaborador")
                        .WithMany()
                        .HasForeignKey("ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaborador");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.CargoModel", b =>
                {
                    b.Navigation("Colaboradores");
                });

            modelBuilder.Entity("ApiFolhaPagamento.Models.EmpresaModel", b =>
                {
                    b.Navigation("Colaboradores");
                });
#pragma warning restore 612, 618
        }
    }
}
