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
    public class BeneficioRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public BeneficioRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public void Adicionar(BeneficioModel obj)
        {
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }
        public void Atualizar(BeneficioModel obj)
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
        public async Task<List<BeneficioModel>> BuscarTodosBeneficios()
        {
            return await _dbContext.Beneficios.ToListAsync();
        }
        public BeneficioModel BuscarPorId(int id)
        {
            return _dbContext.Beneficios.Find(id);
        }


    }
}