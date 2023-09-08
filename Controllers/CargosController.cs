using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
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
        public async Task<ActionResult<List<CargoModel>>> BuscarTodosOsCargos()
        {
            List<CargoModel> cargos = await _cargoRepositorio.BuscarTodosCargos();
            return Ok(cargos);
        }

        [HttpGet("{id}")]
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
        public IActionResult Post([FromBody] CargoModel cargo)
        {
            try
            {
                var existingCargo = _cargoRepositorio.BuscarPorNome(cargo.Nome);

                if (existingCargo != null)
                {
                    return BadRequest("Já existe um cargo cadastrado com o mesmo Nome.");
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
        public IActionResult Put(int id, [FromBody] CargoModel cargoModel)
        {
            try
            {
                var existingCargo = _cargoRepositorio.BuscarPorId(id);

                if (existingCargo == null)
                {
                    return NotFound();
                }

                // Atualize os dados do cargo com os valores do DTO
                existingCargo.Nome = cargoModel.Nome;

                _cargoRepositorio.Atualizar(existingCargo);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(cargoModel);
        }


    }
}
