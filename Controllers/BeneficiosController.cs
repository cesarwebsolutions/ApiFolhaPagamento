using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiFolhaPagamento.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiosController : ControllerBase
    {
        private readonly BeneficioRepositorio _beneficioRepositorio;


        public BeneficiosController(BeneficioRepositorio beneficioRepositorio)
        {
            _beneficioRepositorio = beneficioRepositorio;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<BeneficioModel>>> BuscarTodosOsBeneficios()
        {
            List<BeneficioModel> beneficios = await _beneficioRepositorio.BuscarTodosBeneficios();
            return Ok(beneficios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<BeneficioModel> Get(int id)
        {
            var beneficio = _beneficioRepositorio.BuscarPorId(id);
            if (beneficio == null)
            {
                return NotFound();
            }
            return Ok(beneficio);
        }

        [HttpPost]
        [Authorize(Policy = "Adm")]
        public IActionResult Post([FromBody] BeneficioModel beneficio)
        {
            try
            {
                _beneficioRepositorio.Adicionar(beneficio);
                return CreatedAtAction(nameof(Get), new { id = beneficio.Id }, beneficio);
            }
            catch (DllNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "Adm")]
        public IActionResult Put(int id, [FromBody] BeneficioModel beneficioDTO)
        {
            try
            {
                var existingBeneficio = _beneficioRepositorio.BuscarPorId(id);

                if (existingBeneficio == null)
                {
                    return NotFound();
                }

                // Atualize os dados do beneficio com os valores do DTO
                existingBeneficio.Descricao = beneficioDTO.Descricao;

                _beneficioRepositorio.Atualizar(existingBeneficio);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(beneficioDTO);
        }


    }
}
