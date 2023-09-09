using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;

namespace ApiFolhaPagamento.Services
{
    public class HoleriteRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;
        private readonly ColaboradorRepositorio _colaboradorRepositorio;

        public HoleriteRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex, ColaboradorRepositorio colaboradorRepositorio)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
            _colaboradorRepositorio = colaboradorRepositorio;
        }


        public List<HoleriteModel> BuscarTodosHolerites()
        {
            return _dbContext.Holerites
                .Include(h => h.Colaborador) 
                .ToList();
        }


        public HoleriteModel BuscarHoleritePorId(int id)
        {
            return _dbContext.Holerites.Find(id);
        }

        public void AdicionarHolerite(HoleriteModel holerite)
        {
            _dbContext.Holerites.Add(holerite);
            _dbContext.SaveChanges(); 
        }

        public void AtualizarHolerite(HoleriteModel holerite)
        {
            _dbContext.Update(holerite);
            _dbContext.SaveChanges();
        }

        public void DeletarHolerite(int id)
        {
            var holerite = _dbContext.Holerites.Find(id);
            if (holerite == null)
            {
                throw new DbUpdateConcurrencyException("Holerite não encontrado");
            }

            _dbContext.Holerites.Remove(holerite);
            _dbContext.SaveChanges();
        }
    }
}