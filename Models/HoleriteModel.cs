using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFolhaPagamento.Models
{
    [Table("TbHolerite")]
    public class HoleriteModel
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Colaborador))]
        public int ColaboradorId { get; set; }

        // Propriedade virtual para permitir lazy loading dos dados do colaborador
        public virtual ColaboradorModel? Colaborador { get; set; }

        // Propriedade para exibir o nome do colaborador na view
        [NotMapped]
        public string? NomeColaborador { get; set; }
        [Display(Name = "Mês/Ano")]

        public int MesAno { get; set; }
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]

        [Display(Name = "Salário Bruto")]
        public double SalarioBruto { get; set; }
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Desconto INSS")]

        public double DescontoINSS { get; set; }
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Desconto IRRF")]

        public double DescontoIRRF { get; set; }
        [Display(Name = "Horas Trabalhadas")]
        public double HorasNormais { get; set; }
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        [Display(Name = "Salário Líquido")]

        public double SalarioLiquido { get; set; }
        [Display(Name = "Dependentes")]

        public int DependentesHolerite { get; set; }
        public int? Tipo { get; set; }




        public HoleriteModel()
        {

        }

        public HoleriteModel(int id, ColaboradorModel colaborador, int mesAno, double salarioBruto, double descontoInss, double descontoIrrf, double horasNormais, double salarioLiquido, int tipo)
        {
            Id = id;
            Colaborador = colaborador;
            MesAno = mesAno;
            SalarioBruto = salarioBruto;
            DescontoINSS = descontoInss;
            DescontoIRRF = descontoIrrf;
            HorasNormais = horasNormais;
            SalarioLiquido = salarioLiquido;
            Tipo = tipo;

            NomeColaborador = colaborador?.Nome + colaborador?.Sobrenome;
        }


    }
}