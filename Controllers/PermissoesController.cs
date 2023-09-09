using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiFolhaPagamento.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissoessController : ControllerBase
    {
        private readonly PermissoesRepositorio _permissoesRepositorio;


        public PermissoessController(PermissoesRepositorio permissoesRepositorio)
        {
            _permissoesRepositorio = permissoesRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Permissoes>>> BuscarTodosOsPermissoess()
        {
            List<Permissoes> permissoess = await _permissoesRepositorio.BuscarTodosPermissoess();
            return Ok(permissoess);
        }

        [HttpGet("{id}")]
        public ActionResult<Permissoes> Get(int id)
        {
            var permissoes = _permissoesRepositorio.BuscarPorId(id);
            if (permissoes == null)
            {
                return NotFound();
            }
            return Ok(permissoes);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Permissoes permissoes)
        {
            try
            {
                _permissoesRepositorio.Adicionar(permissoes);
                return CreatedAtAction(nameof(Get), new { id = permissoes.Id }, permissoes);
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
        public IActionResult Put(int id, [FromBody] Permissoes permissoesDTO)
        {
            try
            {
                var existingPermissoes = _permissoesRepositorio.BuscarPorId(id);

                if (existingPermissoes == null)
                {
                    return NotFound();
                }

                // Atualize os dados do permissoes com os valores do DTO
                existingPermissoes.Permissao = permissoesDTO.Permissao;

                _permissoesRepositorio.Atualizar(existingPermissoes);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(permissoesDTO);
        }


    }
}
