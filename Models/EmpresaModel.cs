using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiFolhaPagamento.Models
{
    [Table("TbEmpresa")]
    public class EmpresaModel
    {
        public int Id { get; set; }

        [Display(Name = "CNPJ")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "{0} deve ter {1} caracteres")]
        public string Cnpj { get; set; }


        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }


        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [JsonIgnore]
        public string? Logradouro { get; set; }
        [JsonIgnore]
        public string? Bairro { get; set; }
        [JsonIgnore]
        public string? Numero { get; set; }
        [JsonIgnore]
        public string? Cidade { get; set; }
        [JsonIgnore]
        public string? Estado { get; set; }


        [Display(Name = "CEP")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "{0} deve ter {1} caracteres")]
        public string? CEP { get; set; }

        [JsonIgnore]
        public List<ColaboradorModel>? Colaboradores { get; set; }


        public EmpresaModel()
        {

        }

        public EmpresaModel(int id, string cnpj, string razaoSocial, string nomeFantasia)
        {
            Id = id;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
        }
    }
}
