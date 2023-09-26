using Microsoft.AspNetCore.Authorization;

namespace ApiFolhaPagamento.AuthorizationTeste
{
    public class Permissao : IAuthorizationRequirement
    {
        private int _permissao { get; set; }
        public Permissao(int permissao)
        {
            _permissao = permissao;
        }

    }
}
