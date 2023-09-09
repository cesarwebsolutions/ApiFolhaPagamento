using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Repositorios.Interfaces;
using ApiFolhaPagamento.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ApiFolhaPagamento.Repositorios
{
    public class LoginRepositorio : ILogin
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;
        public LoginRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public async Task<string> Login(string email, string senha)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email);

            if (usuario != null)
            {
                if(senha == usuario.Senha)
                {
                    return TokenService.GenerateToken(usuario);
                }
            }

            return "erro";
        }
    }
}