using System.Collections.Generic;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiFolhaPagamento.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly EmpresaRepositorio _empresaRepositorio;


        public EmpresasController(EmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<EmpresaModel>> Get()
        {
            var empresas = _empresaRepositorio.BuscarTodos();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<EmpresaModel> Get(int id)
        {
            var empresa = _empresaRepositorio.BuscarPorId(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return Ok(empresa);
        }

        [HttpPost]
        [Authorize(Policy = "Adm")]
        public IActionResult Post([FromBody] EmpresaModel empresa)
        {
            try
            {
                _empresaRepositorio.Adicionar(empresa);
                return CreatedAtAction(nameof(Get), new { id = empresa.Id }, empresa);
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
        [Authorize(Policy = "Adm")]
        public IActionResult Put(int id, [FromBody] EmpresaModel empresa)
        {
            try
            {
                var existingEmpresa = _empresaRepositorio.BuscarPorId(id);

                if (existingEmpresa == null)
                {
                    return NotFound();
                }

                existingEmpresa.Cnpj = empresa.Cnpj;
                existingEmpresa.RazaoSocial = empresa.RazaoSocial;
                existingEmpresa.NomeFantasia = empresa.NomeFantasia;
                existingEmpresa.CEP = empresa.CEP;
                existingEmpresa.Numero = empresa.Numero;





                _empresaRepositorio.Atualizar(existingEmpresa);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            return Ok(empresa);
        }
    }
}
