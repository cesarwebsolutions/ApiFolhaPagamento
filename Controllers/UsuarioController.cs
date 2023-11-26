using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Repositorios;
using ApiFolhaPagamento.Repositorios.Interfaces;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFolhaPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Adm")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
        {
            UsuarioModel usuarios = await _usuarioRepositorio.BuscarUsuario(id);
            if (usuarios == null)
            {
                return NotFound((new { message = "Usuário não encontrado" }));
            }
            return Ok(usuarios);
        }

        [HttpPost]
        [Authorize(Policy = "Adm")]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
        {
            try
            {
                var existingemail = _usuarioRepositorio.BuscarPorEmail(usuarioModel.Email);

                if (existingemail != null)
                {
                    return BadRequest((new { message = "Já existe um Usuário com mesmo Email" }));
                }
                UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);

                return Ok(usuario);
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
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, id);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Adm")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            await _usuarioRepositorio.Apagar(id);

            return Ok((new { message = "Usuário deletado com sucesso" }));
        }
    }
}