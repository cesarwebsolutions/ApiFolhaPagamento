using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFolhaPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UsuarioModel usuario)
        {
            //var usuario = {"Nome": "teste", "senha": 'fdjakhsdjkhf'}
            //return View();

            var token = TokenService.GenerateToken(usuario);

            return token;
        }
    }
}
