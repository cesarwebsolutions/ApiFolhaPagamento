using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFolhaPagamento.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly ColaboradorRepositorio _colaboradorRepositorio;
        private readonly CargoRepositorio _cargoRepositorio;
        private readonly EmpresaRepositorio _empresaRepositorio;

        public ColaboradoresController(ColaboradorRepositorio colaboradorRepositorio, CargoRepositorio cargoRepositorio, EmpresaRepositorio empresaRepositorio)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
            _cargoRepositorio = cargoRepositorio;
            _empresaRepositorio = empresaRepositorio;
        }

        [HttpGet]
        public ActionResult<List<ColaboradorModel>> Get()
        {
            var colaboradores = _colaboradorRepositorio.BuscarTodosColaboradores();
            return Ok(colaboradores);
        }

        [HttpGet("{id}")]
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
        public IActionResult Post([FromBody] ColaboradorModel colaborador)
        {
            try
            {
                var existingColaborador = _colaboradorRepositorio.BuscarPorCPF(colaborador.CPF);

                if (existingColaborador != null)
                {
                    return BadRequest("Já existe um colaborador cadastrado com o mesmo CPF.");
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
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

                _colaboradorRepositorio.Atualizar(existingColaborador);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(colaborador);
        }



    }
}
