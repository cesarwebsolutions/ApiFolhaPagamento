    using System;
    using System.Collections.Generic;
using System.Data;
using System.Linq;
    using System.Threading.Tasks;
using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFolhaPagamento.Services
{
    public class ColaboradorRepositorio
    {
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public ColaboradorRepositorio(SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _dbContext  = sistemaFolhaPagamentoDBContex;
        }

        public List<ColaboradorModel> BuscarTodosColaboradores()
        {
            return _dbContext.Colaboradores.ToList();
        }

        public void Adicionar(ColaboradorModel obj)
        {
            obj.Ativo = true;
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }

        public ColaboradorModel BuscarPorId(int id)
        {
            return _dbContext.Colaboradores
                .Include(colaborador => colaborador.Cargo)
                .FirstOrDefault(colaborador => colaborador.Id == id);
        }


        public void Deletar(int id)
        {
            var obj = _dbContext.Colaboradores.Find(id);
            _dbContext.Colaboradores.Remove(obj);
            _dbContext.SaveChanges();
        }


        public void Atualizar(ColaboradorModel obj)
        {
            if (!_dbContext.Colaboradores.Any(x => x.Id == obj.Id))
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
        public List<ColaboradorModel> BuscarColaboradoresAtivos()
        {
            return _dbContext.Colaboradores
                .Where(c => c.Ativo == true)
                .Join(
                    _dbContext.Cargos,
                    colaborador => colaborador.CargoId,
                    cargo => cargo.Id,
                    (colaborador, cargo) => new { Colaborador = colaborador, Cargo = cargo }
                )
                .Join(
                    _dbContext.Empresas,
                    colaboradorCargo => colaboradorCargo.Colaborador.EmpresaId,
                    empresa => empresa.Id,
                    (colaboradorCargo, empresa) => new ColaboradorModel
                    {
                        Id = colaboradorCargo.Colaborador.Id,
                        Nome = colaboradorCargo.Colaborador.Nome,
                        Sobrenome = colaboradorCargo.Colaborador.Sobrenome,
                        SalarioBase = colaboradorCargo.Colaborador.SalarioBase,
                        Dependentes = colaboradorCargo.Colaborador.Dependentes,
                        Filhos = colaboradorCargo.Colaborador.Filhos,
                        DataNascimento = colaboradorCargo.Colaborador.DataNascimento,
                        DataAdmissao = colaboradorCargo.Colaborador.DataAdmissao,
                        Cargo = colaboradorCargo.Cargo,
                        Empresa = empresa 
                    }
                )
                .ToList();
        }

        public ColaboradorModel BuscarPorCPF(string cpf)
        {
        
            var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.CPF == cpf);

            return colaborador;
        }

        public void Demitir(int id)
        {
            var colaborador = _dbContext.Colaboradores.Find(id);

            if (colaborador != null)
            {
                colaborador.Ativo = false;
                colaborador.DataDemissao = DateTime.Now;

                _dbContext.SaveChanges();
            }
        }
        public bool HasCompanyEmpresa(int colaboradorId)
        {
            return _dbContext.Colaboradores.Any(c => c.Id == colaboradorId && c.EmpresaId != null);
        }
        public bool HasCompanyCargo(int colaboradorId)
        {
            return _dbContext.Colaboradores.Any(c => c.Id == colaboradorId && c.CargoId != null);
        }

    }
}
