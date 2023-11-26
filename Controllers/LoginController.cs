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
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] LoginModel login)
        {
            var validaLogin = await _loginRepositorio.Login(login.Email, login.Senha);
            if (validaLogin == "erro")
            {
                return Unauthorized(new { message = "usuario invalido" });
                
            }
            return Ok(new { token = validaLogin});
        }
    }
}