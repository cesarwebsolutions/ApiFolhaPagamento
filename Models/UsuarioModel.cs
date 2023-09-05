using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiFolhaPagamento.Models
{
    [Table("TbUsuario")]
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        [ForeignKey(nameof(Permissoes))]
        public int PermissaoId { get; set; }


        [JsonIgnore]
        [NotMapped]
        public Permissoes? Permissoes { get; set; }
    }
}
