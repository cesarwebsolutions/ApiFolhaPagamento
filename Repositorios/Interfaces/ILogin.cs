using ApiFolhaPagamento.Models;
using Microsoft.OpenApi.Any;

namespace ApiFolhaPagamento.Repositorios.Interfaces
{
    public interface ILogin
    {
        Task<bool> Login(string email, string senha);
    }
}
