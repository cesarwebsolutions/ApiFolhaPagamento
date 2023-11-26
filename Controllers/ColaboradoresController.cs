using System.Collections.Generic;
using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFolhaPagamento.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly ColaboradorRepositorio _colaboradorRepositorio;
        private readonly CargoRepositorio _cargoRepositorio;
        private readonly EmpresaRepositorio _empresaRepositorio;
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public ColaboradoresController(SistemaFolhaPagamentoDBContex dbContext, ColaboradorRepositorio colaboradorRepositorio, CargoRepositorio cargoRepositorio, EmpresaRepositorio empresaRepositorio)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
            _cargoRepositorio = cargoRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ColaboradorModel>> Get()
        {
            var colaboradores = _colaboradorRepositorio.BuscarTodosColaboradores();
            return Ok(colaboradores);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ColaboradorModel> Get(int id)
        {
            var colaborador = _colaboradorRepositorio.BuscarPorId(id);
            if (colaborador == null)
            {
                return NotFound();
            }
            return Ok(colaborador);
        }

        [HttpPost]
        [Authorize(Policy = "Adm")]
        public IActionResult Post([FromBody] ColaboradorModel colaborador)
        {
            try
            {
                var existingColaborador = _colaboradorRepositorio.BuscarPorCPF(colaborador.CPF);

                if (existingColaborador != null)
                {
                    return BadRequest((new { message = "Já existe um Colaborador com mesmo CPF" }));
                }
                var existingemail = _colaboradorRepositorio.BuscarPorEmail(colaborador.CPF);

                if (existingemail != null)
                {
                    return BadRequest((new { message = "Já existe um Colaborador com mesmo Email" }));
                }

                _colaboradorRepositorio.Adicionar(colaborador);

                return CreatedAtAction(nameof(Get), new { id = colaborador.Id }, colaborador);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Adm")]
        public IActionResult Put(int id, [FromBody] ColaboradorModel colaborador)
        {
            try
            {
                var existingColaborador = _colaboradorRepositorio.BuscarPorId(id);

                if (existingColaborador == null)
                {
                    return NotFound();
                }

                existingColaborador.CPF = colaborador.CPF;
                existingColaborador.Nome = colaborador.Nome;
                existingColaborador.Sobrenome = colaborador.Sobrenome;
                existingColaborador.SalarioBase = colaborador.SalarioBase;
                existingColaborador.DataNascimento = colaborador.DataNascimento;
                existingColaborador.DataAdmissao = colaborador.DataAdmissao;
                existingColaborador.Dependentes = colaborador.Dependentes;
                existingColaborador.Filhos = colaborador.Filhos;
                existingColaborador.CargoId = colaborador.CargoId;
                existingColaborador.EmpresaId = colaborador.EmpresaId;
                existingColaborador.CEP = colaborador.CEP;
                existingColaborador.Logradouro = colaborador.Logradouro;
                existingColaborador.Numero = colaborador.Numero;
                existingColaborador.Bairro = colaborador.Bairro;
                existingColaborador.Cidade = colaborador.Cidade;
                existingColaborador.Estado = colaborador.Estado;
                existingColaborador.Email = colaborador.Email;






                _colaboradorRepositorio.Atualizar(existingColaborador);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(colaborador);
        }

        [HttpPut("demitir/{id}")]
        [Authorize(Policy = "Adm")]
        public IActionResult DemitirColaborador(int id)
        {
            try
            {
                var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.Id == id);

                if (colaborador == null)
                {
                    return NotFound((new { message = "Colaborador não encontrado" }));
                }

                if (!colaborador.Ativo)
                {
                    return BadRequest((new { message = "Este colaborador já está inativo" }));
                }

                colaborador.DataDemissao = DateTime.Now;
                colaborador.Ativo = false;

                _dbContext.SaveChanges();

                return Ok((new { message = "Colaborador demitido com sucesso" }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ativos")]
        [Authorize]
        public ActionResult<List<ColaboradorModel>> GetColaboradoresAtivos()
        {
            try
            {
                var colaboradoresAtivos = _colaboradorRepositorio.BuscarColaboradoresAtivos();
                return Ok(colaboradoresAtivos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("inativos")]
        [Authorize]
        public ActionResult<List<ColaboradorModel>> GetColaboradoreInativos()
        {
            try
            {
                var colaboradoresAtivos = _colaboradorRepositorio.BuscarColaboradoresInativos();
                return Ok(colaboradoresAtivos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reativar/{id}")]
        [Authorize(Policy = "Adm")]
        public IActionResult ReativarColaborador(int id)
        {
            try
            {
                var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.Id == id);

                if (colaborador == null)
                {
                    return NotFound((new { message = "Colaborador não encontrado" }));
                }

                if (colaborador.Ativo)
                {
                    return BadRequest((new { message = "Este colaborador já está ativo" }));
                }

                colaborador.Ativo = true;
                colaborador.DataDemissao = null;
                colaborador.DataAdmissao = DateTime.Now;


                _dbContext.SaveChanges();

                return Ok((new { message = "Colaborador reativado com sucesso" }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
