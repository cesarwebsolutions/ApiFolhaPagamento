using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFolhaPagamento.Models
{
    [Table("TbPermissoes")]
    public class Permissoes
    {
        public int Id { get; set; }
        public string Permissao { get; set; }
    }
}
