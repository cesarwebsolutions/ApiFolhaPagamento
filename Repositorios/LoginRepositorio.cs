using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiFolhaPagamento.Repositorios
{
    public class LoginRepositorio : ILogin
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;
        public LoginRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public async Task<bool> Login(string email, string senha)
        {
            var user = await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email);

            if (user != null)
            {
                var passwordIsValid = VerifyPassword(senha, user.Senha);
                return passwordIsValid;
            }

            return false;
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            return providedPassword == storedHash ? true : false;
            // Implement your password verification logic here.
            // Compare the providedPassword with the storedHash (after applying the same hashing/salting algorithm).
            // Return true if they match, otherwise return false.

            
        }
    }
}
