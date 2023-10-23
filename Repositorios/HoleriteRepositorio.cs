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
                .Select(holerite => new HoleriteModel
                {
                    Id = holerite.Id,
                    ColaboradorId = holerite.ColaboradorId,
                    Colaborador = new ColaboradorModel
                    {
                        Nome = holerite.Colaborador.Nome,
                        Sobrenome = holerite.Colaborador.Sobrenome,
                        CPF = holerite.Colaborador.CPF
                    },
                    Mes = holerite.Mes,
                    Ano = holerite.Ano,
                    SalarioBruto = holerite.SalarioBruto,
                    DescontoINSS = holerite.DescontoINSS,
                    DescontoIRRF = holerite.DescontoIRRF,
                    HorasNormais = holerite.HorasNormais,
                    HorasExtras = holerite.HorasExtras,
                    SalarioLiquido = holerite.SalarioLiquido,
                    DependentesHolerite = holerite.DependentesHolerite,
                    Tipo = holerite.Tipo
                })
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