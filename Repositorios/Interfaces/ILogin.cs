using ApiFolhaPagamento.Models;
using Microsoft.OpenApi.Any;

namespace ApiFolhaPagamento.Repositorios.Interfaces
{
    public interface ILogin
    {
        Task<string> Login(string email, string senha);
    }
}