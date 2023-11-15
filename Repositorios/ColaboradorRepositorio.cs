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
                 .Select(colaborador => new ColaboradorModel
                 {
                     Id = colaborador.Id,
                     CPF = colaborador.CPF,
                     Nome = colaborador.Nome,
                     Sobrenome = colaborador.Sobrenome,
                     Email = colaborador.Email,
                     SalarioBase = colaborador.SalarioBase,
                     DataNascimento = colaborador.DataNascimento,
                     DataAdmissao = colaborador.DataAdmissao,
                     Dependentes = colaborador.Dependentes,
                     Filhos = colaborador.Filhos,
                     CargoId = colaborador.CargoId,
                     EmpresaId = colaborador.EmpresaId,
                     CEP = colaborador.CEP,
                     Logradouro = colaborador.Logradouro,
                     Numero = colaborador.Numero,
                     Bairro = colaborador.Bairro,
                     Cidade = colaborador.Cidade,
                     Estado = colaborador.Estado,

                 })
                 .ToList();
        }
        public List<ColaboradorModel> BuscarColaboradoresInativos()
        {
            return _dbContext.Colaboradores
                .Where(c => c.Ativo == false)
                .Select(colaborador => new ColaboradorModel
                {
                    Id = colaborador.Id,
                    CPF = colaborador.CPF,
                    Nome = colaborador.Nome,
                    Sobrenome = colaborador.Sobrenome,
                    SalarioBase = colaborador.SalarioBase,
                    DataNascimento = colaborador.DataNascimento,
                    DataAdmissao = colaborador.DataAdmissao,
                    DataDemissao = colaborador.DataDemissao,
                    Dependentes = colaborador.Dependentes,
                    Filhos = colaborador.Filhos,
                    CargoId = colaborador.CargoId,
                    EmpresaId = colaborador.EmpresaId, 
                    CEP = colaborador.CEP,
                    Logradouro = colaborador.Logradouro,
                    Numero = colaborador.Numero,
                    Bairro = colaborador.Bairro,
                    Cidade = colaborador.Cidade,
                    Estado = colaborador.Estado,
                    Ativo = colaborador.Ativo,
                    Email = colaborador.Email

                })
                .ToList();
        }


        public ColaboradorModel BuscarPorCPF(string cpf)
        {

            var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.CPF == cpf);

            return colaborador;
        }

        public ColaboradorModel BuscarPorEmail(string email)
        {

            var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.Email == email);

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
