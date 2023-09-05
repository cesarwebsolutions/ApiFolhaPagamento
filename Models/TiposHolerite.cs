using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFolhaPagamento.Models
{
    [Table("TbTipoHolerite")]
    public class TiposHolerite
    {
        public int Id { get; set; }
        public string TipoHolerite { get; set; }
    }
}
