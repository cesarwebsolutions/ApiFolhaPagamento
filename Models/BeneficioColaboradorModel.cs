using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiFolhaPagamento.Models
{
    [Table("TbBeneficioColaborador")]
    public class BeneficioColaboradorModel
    {
        public int Id { get; set; }
        [ForeignKey(nameof(BeneficioModel))]
        public int BeneficioId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public BeneficioModel? BeneficioModel { get; set; }

        [ForeignKey(nameof(ColaboradorModel))]
        public int ColaboradorId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ColaboradorModel? ColaboradorModel { get; set; }

    }
}
