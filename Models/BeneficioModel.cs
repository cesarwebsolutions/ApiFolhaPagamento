using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFolhaPagamento.Models
{
    [Table("TbBeneficio")]
    public class BeneficioModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }
}
