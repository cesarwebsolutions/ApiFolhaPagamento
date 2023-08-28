using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiFolhaPagamento.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;
        public UsuarioRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex) 
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuario(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário não encontrado!");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuario(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário não encontrado!");
            }

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return usuarioPorId;
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> BuscarUsuario(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
        }
    }
}
