﻿using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace ApiFolhaPagamento.Services
{
    public class CargoRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public CargoRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext = sistemaFolhaPagamentoDBContex;
        }
        public void Adicionar(CargoModel obj)
        {
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }
        public void Atualizar(CargoModel obj)
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
        public async Task<List<CargoModel>> BuscarTodosCargos()
        {
            return await _dbContext.Cargos.ToListAsync();
        }
        public CargoModel BuscarPorId(int id)
        {
            return _dbContext.Cargos.Find(id);
        }


    }
}