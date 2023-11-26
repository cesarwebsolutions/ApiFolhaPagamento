using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiFolhaPagamento.Models
{
    [Table("TbCargo")]
    public class CargoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [JsonIgnore]
        public ICollection<ColaboradorModel>? Colaboradores { get; set; }

        
        public CargoModel()
        {
            Colaboradores = new List<ColaboradorModel>();
        }

        public CargoModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Colaboradores = new List<ColaboradorModel>();
        }
    }
}
