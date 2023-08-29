using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Repositorios;
using ApiFolhaPagamento.Repositorios.Interfaces;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFolhaPagamento.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginRepositorio;

        public LoginController(ILogin loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UsuarioModel usuario)
        {
            var validaLogin = await _loginRepositorio.Login(usuario.Email, usuario.Senha);
            if (validaLogin)
            {
                var token = TokenService.GenerateToken(usuario);
                return token;
            }
        //return View();
        

            return NotFound(new {message = "usuario invalido"});
        }
    }
}
