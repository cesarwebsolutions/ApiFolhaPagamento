using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;


namespace ApiFolhaPagamento.Models
{
    [Table("TbColaborador")]
    public class ColaboradorModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "{0} deve ter {1} caracteres")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Sobrenome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Salário Base")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        public double SalarioBase { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data de Admissão")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAdmissao { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataDemissao { get; set; }
        public int? Dependentes { get; set; }
        public int? Filhos { get; set; }

        [JsonIgnore]
        public CargoModel? Cargo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [ForeignKey(nameof(Cargo))]
        public int CargoId { get; set; }


        [JsonIgnore]
        public EmpresaModel? Empresa;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }
       
        public bool Ativo { get; set; } = true;
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Estado { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "CEP")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "{0} deve ter {1} caracteres")]
        public string CEP { get; set; }

        public ColaboradorModel()
        {

        }

        public ColaboradorModel(int id, string nome, string sobrenome, double salarioBase, DateTime dataNascimento, DateTime dataAdmissao, int dependentes, int filhos, CargoModel cargo, EmpresaModel empresa)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            SalarioBase = salarioBase;
            DataNascimento = dataNascimento;
            DataAdmissao = dataAdmissao;
            Dependentes = dependentes;
            Filhos = filhos;
            Cargo = cargo;
            Empresa = empresa;
        }
    }
}
