using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiFolhaPagamento.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly CargoRepositorio _cargoRepositorio;


        public CargosController(CargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CargoModel>>> BuscarTodosOsCargos()
        {
            List<CargoModel> cargos = await _cargoRepositorio.BuscarTodosCargos();
            return Ok(cargos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<CargoModel> Get(int id)
        {
            var cargo = _cargoRepositorio.BuscarPorId(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return Ok(cargo);
        }

        [HttpPost]
        [Authorize(Policy = "Adm")]
        public IActionResult Post([FromBody] CargoModel cargo)
        {
            try
            {
                var existingCargo = _cargoRepositorio.BuscarPorNome(cargo.Nome);

                if (existingCargo != null)
                {
                    return BadRequest(new { message = "Já existe um Cargo com mesmo Nome"});
                }


                _cargoRepositorio.Adicionar(cargo);
                return CreatedAtAction(nameof(Get), new { id = cargo.Id }, cargo);
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
        public IActionResult Put(int id, [FromBody] CargoModel cargoModel)
        {
            try
            {
                var existingCargo = _cargoRepositorio.BuscarPorId(id);

                if (existingCargo == null)
                {
                    return NotFound();
                }

                existingCargo.Nome = cargoModel.Nome;

                _cargoRepositorio.Atualizar(existingCargo);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(cargoModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Adm")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_cargoRepositorio.TemColaboradoresVinculados(id))
                {
                    return BadRequest(new { message = "Não foi possível excluir o Cargo, pois há colaboradores vinculados" });
                }

                _cargoRepositorio.ExcluirCargo(id);
                return Ok(new { message = "Cargo excluído com sucesso" });
            }
            catch (ArgumentException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }



    }
}
