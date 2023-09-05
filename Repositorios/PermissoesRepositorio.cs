using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace ApiFolhaPagamento.Services
{
    public class PermissoesRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public PermissoesRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public void Adicionar(Permissoes obj)
        {
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }
        public void Atualizar(Permissoes obj)
        {
          
            try
            {
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
        public async Task<List<Permissoes>> BuscarTodosPermissoess()
        {
            return await _dbContext.Permissoes.ToListAsync();
        }
        public Permissoes BuscarPorId(int id)
        {
            return _dbContext.Permissoes.Find(id);
        }


    }
}