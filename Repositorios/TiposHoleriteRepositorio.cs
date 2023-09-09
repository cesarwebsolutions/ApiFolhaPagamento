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
    public class TiposHoleriteRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public TiposHoleriteRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public void Adicionar(TiposHolerite obj)
        {
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }
        public void Atualizar(TiposHolerite obj)
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
        public async Task<List<TiposHolerite>> BuscarTodosTiposHolerite()
        {
            return await _dbContext.TiposHolerite.ToListAsync();
        }
        public TiposHolerite BuscarPorId(int id)
        {
            return _dbContext.TiposHolerite.Find(id);
        }


    }
}