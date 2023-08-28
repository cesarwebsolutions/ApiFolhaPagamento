using System.ComponentModel;

namespace ApiFolhaPagamento.Enums
{
    public enum StatusTarefas
    {
        [Description("A fazer")]
        Afazer = 1,
        [Description("Em andamento")]
        EmAndamenti = 2,
        [Description("Concluido")]
        Concluido = 3
    }
}
