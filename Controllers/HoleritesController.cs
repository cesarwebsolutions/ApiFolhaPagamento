using System;
using System.IO;
using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ApiFolhaPagamento.Services;

namespace ApiFolhaPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleritesController : ControllerBase
    {
        private readonly HoleriteRepositorio _holeriteRepositorio;
        private readonly ColaboradorRepositorio _colaboradorRepositorio;
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public HoleritesController(HoleriteRepositorio holeriteRepositorio, ColaboradorRepositorio colaboradorRepositorio, SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex)
        {
            _holeriteRepositorio = holeriteRepositorio;
            _colaboradorRepositorio = colaboradorRepositorio;
            _dbContext = sistemaFolhaPagamentoDBContex;
        }

        [HttpPost]
        public IActionResult Post([FromBody] HoleriteModel holerite)
        {
            try
            {
                var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.Id == holerite.ColaboradorId);

                if (colaborador == null)
                {
                    return NotFound("Colaborador não encontrado");
                }

                // Calcula o desconto INSS
                holerite.DescontoINSS = Math.Round(CalculaDescontoINSS(colaborador.SalarioBase), 2);

                // Calcula o desconto IRRF
                holerite.DescontoIRRF = Math.Round(CalculaDescontoIRRF(colaborador.SalarioBase, holerite.DescontoIRRF ?? 0.0), 2);

                // Soma os descontos
                double descontos = holerite.DescontoINSS.Value + holerite.DescontoIRRF.Value;

                // Calcula o salário líquido
                holerite.SalarioBruto = colaborador.SalarioBase;
                holerite.SalarioLiquido = Math.Round(colaborador.SalarioBase - descontos, 2);
                holerite.DependentesHolerite = (int)colaborador.Dependentes;

                _holeriteRepositorio.AdicionarHolerite(holerite);

                return CreatedAtAction(nameof(BuscarPorId), new { id = holerite.Id }, holerite);
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


        [HttpGet]
        public ActionResult<List<HoleriteModel>> BuscarTodosOsHolerites()
        {
            var holerites = _holeriteRepositorio.BuscarTodosHolerites();
            return Ok(holerites);
        }

        [HttpGet("{id}")]
        public ActionResult<HoleriteModel> BuscarPorId(int id)
        {
            var colaborador = _holeriteRepositorio.BuscarHoleritePorId(id);
            if (colaborador == null)
            {
                return NotFound("Holerite Não Encontrado");
            }
            return Ok(colaborador);
        }

        [HttpDelete("{id}")]
        public ActionResult<HoleriteModel> Apagar(int id)
        {
             _holeriteRepositorio.DeletarHolerite(id);

            return Ok("Holerite Deletado com Sucesso!");
        }
        //Regra de calculo baseada na fonte https://www.contabilizei.com.br/contabilidade-online/tabela-inss/
        private double CalculaDescontoINSS(double salarioBase)
        {
            double descontoINSS = 0.0;

            if (salarioBase <= 1320.00)
            {
                descontoINSS = salarioBase * 0.075; // 7.5%
            }
            else if (salarioBase <= 2571.29)
            {
                descontoINSS = salarioBase * 0.09; // 9%
            }
            else if (salarioBase <= 3856.94)
            {
                descontoINSS = salarioBase * 0.12; // 12%
            }
            else if (salarioBase <= 7507.49)
            {
                descontoINSS = salarioBase * 0.14; // 14%
            }
            else
            {
                descontoINSS = 7507.49 * 0.14; // Valor máximo para a alíquota de 14%
            }

            return descontoINSS;
        }


        //regra de cálculo baseada na fonte https://www.pontotel.com.br/calcular-irrf/#:~:text=como%20a%20seguir!-,Como%20funciona%20o%20c%C3%A1lculo%20do%20IRRF%20no%20sal%C3%A1rio%3F,realizar%20o%20c%C3%A1lculo%20do%20IRRF.
        private double CalculaDescontoIRRF(double salarioBase, double descontoINSS)
        {
            double descontoIRRF = 0.0;
            double salarioBaseAjustado = salarioBase - descontoINSS;

            if (salarioBaseAjustado <= 1903.98)
            {
                descontoIRRF = 0;
            }
            else if (salarioBaseAjustado <= 2826.65)
            {
                descontoIRRF = (salarioBaseAjustado * 0.075) - 142.80;
            }
            else if (salarioBaseAjustado <= 3751.05)
            {
                descontoIRRF = (salarioBaseAjustado * 0.15) - 354.80;
            }
            else if (salarioBaseAjustado <= 4664.68)
            {
                descontoIRRF = (salarioBaseAjustado * 0.225) - 636.13;
            }
            else
            {
                descontoIRRF = (salarioBaseAjustado * 0.275) - 869.36;
            }

            return descontoIRRF;
        }
    }
}
