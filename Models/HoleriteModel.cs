using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiFolhaPagamento.Models
{
    [Table("TbHolerite")]
    public class HoleriteModel
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Colaborador))]
        public int ColaboradorId { get; set; }
        //[JsonIgnore]
        public virtual ColaboradorModel? Colaborador { get; set; }


        [Display(Name = "Mês")]
        [DataType(DataType.Date)]
        public int Mes { get; set; }

        [Display(Name = "Ano")]
        [DataType(DataType.Date)]
        public int Ano { get; set; }


        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Salário Bruto")]
        public double? SalarioBruto { get; set; }

        
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Desconto INSS")]
        public double? DescontoINSS { get; set; }

        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Desconto IRRF")]
        public double? DescontoIRRF { get; set; }


        [Display(Name = "Horas Trabalhadas")]
        [Range(1, 220, ErrorMessage = "O campo HorasNormais deve estar entre 1 e 220.")]
        public double? HorasNormais { get; set; }

        [Display(Name = "Horas Extras")]

        public double? HorasExtras { get; set; }

        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Salário Líquido")]
        public double? SalarioLiquido { get; set; }

        [Display(Name = "Dependentes")]
        public int? DependentesHolerite { get; set; }


        public int Tipo { get; set; }

        public double? ValorHorasNormais { get; set; } 
        public double? ValorHorasExtras { get; set; }

        public double? PercentualINSS { get; set; }
        public double? PercentualIRRF { get;set; }




        public HoleriteModel()
        {

        }

        public HoleriteModel(int id, ColaboradorModel colaborador, int mes, int ano, double salarioBruto, double descontoInss, double descontoIrrf, double horasNormais, double salarioLiquido, int tipo, double valorHorasNormais, double valorHorasExtras)
        {
            Id = id;
            Colaborador = colaborador;
            Mes = mes;
            Ano = ano;
            SalarioBruto = salarioBruto;
            DescontoINSS = descontoInss;
            DescontoIRRF = descontoIrrf;
            HorasNormais = horasNormais;
            SalarioLiquido = salarioLiquido;
            Tipo = tipo;
            ValorHorasNormais = valorHorasNormais;
            ValorHorasExtras = valorHorasExtras;
        }


    }
}