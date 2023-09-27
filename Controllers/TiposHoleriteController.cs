using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiFolhaPagamento.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposHoleriteController : ControllerBase
    {
        private readonly TiposHoleriteRepositorio _tipoRepositorio;


        public TiposHoleriteController(TiposHoleriteRepositorio tipoRepositorio)
        {
            _tipoRepositorio = tipoRepositorio;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<TiposHolerite>>> BuscarTodosOsTipos()
        {
            List<TiposHolerite> tipos = await _tipoRepositorio.BuscarTodosTiposHolerite();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<TiposHolerite> Get(int id)
        {
            var tipo = _tipoRepositorio.BuscarPorId(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return Ok(tipo);
        }

        [HttpPost]
        [Authorize(Policy = "Adm")]
        public IActionResult Post([FromBody] TiposHolerite tipo)
        {
            try
            {
                _tipoRepositorio.Adicionar(tipo);
                return CreatedAtAction(nameof(Get), new { id = tipo.Id }, tipo);
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
        public IActionResult Put(int id, [FromBody] TiposHolerite tipoDTO)
        {
            try
            {
                var existingTipo = _tipoRepositorio.BuscarPorId(id);

                if (existingTipo == null)
                {
                    return NotFound();
                }

                // Atualize os dados do tipo com os valores do DTO
                existingTipo.TipoHolerite = tipoDTO.TipoHolerite;

                _tipoRepositorio.Atualizar(existingTipo);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(tipoDTO);
        }


    }
}
