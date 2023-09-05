using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiFolhaPagamento.Models
{
    [Table("TbCargoColaborador")]
    public class CargoColaboradorModel
    {
        public int Id { get; set; }
        public string Funcao { get; set; }

        [ForeignKey(nameof(ColaboradorModel))]
        public int ColaboradorId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ColaboradorModel? ColaboradorModel { get; set; }

        [ForeignKey(nameof(CargoModel))]
        public int CargoId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public CargoModel? CargoModel { get; set; }



    }
}
