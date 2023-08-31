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
    public class EmpresaRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public EmpresaRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }

        public void Adicionar(EmpresaModel empresa)
        {
            _dbContext.Empresas.Add(empresa);
            _dbContext.SaveChanges();
        }

        public List<EmpresaModel> BuscarTodos()
        {
            return _dbContext.Empresas.OrderBy(x => x.NomeFantasia).ToList();
        }
        public EmpresaModel BuscarPorId(int id)
        {
            return _dbContext.Empresas.Find(id);
        }
        public bool HasCompany(int empresaId)
        {
            return _dbContext.Colaboradores.Any(c => c.EmpresaId == empresaId);
        }

        public void Atualizar(EmpresaModel obj)
        {
            if (!_dbContext.Empresas.Any(x => x.Id == obj.Id))
            {
                throw new FileNotFoundException("Id not found");
            }
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

    }
}
