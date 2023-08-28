using ApiFolhaPagamento.Enums;

namespace ApiFolhaPagamento.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public StatusTarefas Status { get; set; }

    }
}
