using System;
using System.IO;
using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf;
using iTextSharp.text;
using IronPdf;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using ApiFolhaPagamento.Migrations;

namespace ApiFolhaPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleritesController : ControllerBase
    {
        private readonly HoleriteRepositorio _holeriteRepositorio;
        private readonly ColaboradorRepositorio _colaboradorRepositorio;
        private readonly CargoRepositorio _cargoRepositorio;
        private readonly TiposHoleriteRepositorio _tipoHoleriteRepositorio;
        private readonly EmpresaRepositorio _empresaRepositorio;
        private readonly SistemaFolhaPagamentoDBContex _dbContext;

        public HoleritesController(HoleriteRepositorio holeriteRepositorio, ColaboradorRepositorio colaboradorRepositorio, SistemaFolhaPagamentoDBContex sistemaFolhaPagamentoDBContex, CargoRepositorio cargoRepositorio, TiposHoleriteRepositorio tiposHolerite, EmpresaRepositorio empresa)
        {
            _holeriteRepositorio = holeriteRepositorio;
            _colaboradorRepositorio = colaboradorRepositorio;
            _dbContext = sistemaFolhaPagamentoDBContex;
            _cargoRepositorio = cargoRepositorio;
            _tipoHoleriteRepositorio = tiposHolerite;
            _empresaRepositorio = empresa;
        }
        [HttpPost]
        //[Authorize(Policy = "Adm")]
        public IActionResult Post([FromBody] HoleriteModel holerite)
        {
            try
            {
                var colaborador = _dbContext.Colaboradores.FirstOrDefault(c => c.Id == holerite.ColaboradorId);

                if (colaborador == null)
                {
                    return NotFound("Colaborador não encontrado");
                }

                var holeriteExistente = _dbContext.Holerites.FirstOrDefault(h =>
                    h.ColaboradorId == holerite.ColaboradorId &&
                    h.Mes == holerite.Mes &&
                    h.Ano == holerite.Ano &&
                    h.Tipo == holerite.Tipo);

                if (holeriteExistente != null)
                {
                    return BadRequest(new { message = "Já existe um Holerite com mesmo Mês/Ano e Tipo cadastrado" });
                }

                double salarioBase = 0.0;

                // Verifica se o campo HorasExtras foi informado
                if (holerite.HorasExtras.HasValue)
                {
                    // Calcula o Salário Bruto baseado nas horas trabalhadas e nas horas extras
                    double percentualSalarioBase = holerite.HorasNormais.Value / 220.0; // 220 é a carga horária padrão
                    salarioBase = Math.Round(colaborador.SalarioBase * percentualSalarioBase, 2);
                    holerite.ValorHorasNormais = salarioBase;

                    // Define a taxa de adicional para horas extras (50% é comum, mas pode variar)
                    double taxaAdicional = 0.5; // 50% de adicional

                    double valorHoraExtra = salarioBase / 220 * taxaAdicional;
                    holerite.ValorHorasExtras = Math.Round(holerite.HorasExtras.Value * valorHoraExtra, 2);
                    double salarioBruto = salarioBase + (holerite.HorasExtras.Value * valorHoraExtra);

                    holerite.SalarioBruto = Math.Round(salarioBruto, 2);
                }
                else
                {
                    // Calcula o Salário Bruto apenas com as horas normais
                    double percentualSalarioBase = holerite.HorasNormais.Value / 220.0; // 220 é a carga horária padrão
                    salarioBase = Math.Round(colaborador.SalarioBase * percentualSalarioBase, 2);
                    holerite.SalarioBruto = salarioBase;
                    holerite.ValorHorasNormais = salarioBase;
                }

                // Calcula o desconto INSS e obtém o percentual
                double percentualINSS;
                holerite.DescontoINSS = Math.Round(CalculaDescontoINSS(holerite.SalarioBruto.Value, out percentualINSS), 2);
                holerite.PercentualINSS = percentualINSS;

                // Calcula o desconto IRRF
                double percentualIRRF;
                holerite.DescontoIRRF = Math.Round(CalculaDescontoIRRF(holerite.SalarioBruto.Value, holerite.DescontoINSS ?? 0.0, out percentualIRRF), 2);
                holerite.PercentualIRRF = percentualIRRF;

                // Soma os descontos
                double descontos = holerite.DescontoINSS.Value + holerite.DescontoIRRF.Value;

                // Calcula o salário líquido
                holerite.SalarioLiquido = Math.Round(holerite.SalarioBruto.Value - descontos, 2);
                holerite.DependentesHolerite = (int)colaborador.Dependentes;

                // Adiciona o Holerite ao banco de dados
                _holeriteRepositorio.AdicionarHolerite(holerite);

                HoleriteModel teste = _holeriteRepositorio.BuscarHoleritePorId(holerite.Id);

                var renderer = new ChromePdfRenderer();

                renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;

                var pdf = renderer.RenderUrlAsPdf("http://localhost:4200/holerite/" + holerite.Id);
                pdf.SaveAs(holerite.Id.ToString() + ".pdf");

                var emailMessage = new MailMessage();
                emailMessage.Subject = "Holerite";
                emailMessage.From = new MailAddress("rhprojectr@gmail.com");
                emailMessage.To.Add(colaborador.Email);
                emailMessage.IsBodyHtml = true;

                emailMessage.Body = "<h1>Teste</h1>";

                var attachmentPath = holerite.Id.ToString() + ".pdf";

                Attachment attachment = new Attachment(attachmentPath, MediaTypeNames.Application.Pdf);
                emailMessage.Attachments.Add(attachment);

                var client = new SmtpClient("smtp.gmail.com", 587);

                client.Credentials = new NetworkCredential("rhprojectr@gmail.com", "jdsn ctkq kasc reew");
                client.EnableSsl = true;

                try
                {
                    client.Send(emailMessage);
                    Console.WriteLine("banana");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                return CreatedAtAction(nameof(BuscarPorId), new { id = holerite.Id }, holerite);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("teste");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }


       



        [HttpGet]
        [Authorize(Policy = "Adm")]
        public ActionResult<List<HoleriteModel>> BuscarTodosOsHolerites()
        {
            var holerites = _holeriteRepositorio.BuscarTodosHolerites();
            return Ok(holerites);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<HoleriteModel> BuscarPorId(int id)
        {
            var holerite = _holeriteRepositorio.BuscarHoleritePorId(id);
            if (holerite == null)
            {
                return NotFound((new { message = "Holerite não encontrado" }));
            }
            return Ok(holerite);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Adm")]
        public ActionResult<HoleriteModel> Apagar(int id)
        {
            var holerite = _holeriteRepositorio.BuscarHoleritePorId(id);

            if (holerite == null)
            {
                return NotFound((new { message = "Holerite não encontrado" }));
            }
            _holeriteRepositorio.DeletarHolerite(id);

            return Ok((new { message = "Holerite Deletado com sucesso" }));
        }

        [HttpGet("filtro")]
        [Authorize(Policy = "Adm")]
        public ActionResult<List<HoleriteModel>> FiltrarHolerites(int? ano, int? mes)
        {
            // Verifica se os parâmetros de ano e mês foram fornecidos na solicitação
            if (ano == null && mes == null)
            {
                // Se nenhum parâmetro foi fornecido, retorna todos os holerites
                var holerites = _holeriteRepositorio.BuscarTodosHolerites();
                return Ok(holerites);
            }
            else
            {
                // Filtra os holerites com base nos parâmetros fornecidos
                var holeritesFiltrados = _holeriteRepositorio
                    .BuscarTodosHolerites()
                    .Where(h =>
                        (!ano.HasValue || h.Ano == ano.Value) &&
                        (!mes.HasValue || h.Mes == mes.Value))
                    .ToList();

                return Ok(holeritesFiltrados);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //Regra de calculo baseada na fonte https://www.contabilizei.com.br/contabilidade-online/tabela-inss/
        private double CalculaDescontoINSS(double salarioBase, out double percentualINSS)
        {
            double descontoINSS = 0.0;
            percentualINSS = 0.0;

            if (salarioBase <= 1320.00)
            {
                descontoINSS = salarioBase * 0.075; // 7.5%
                percentualINSS = 7.5;
            }
            else if (salarioBase <= 2571.29)
            {
                descontoINSS = salarioBase * 0.09; // 9%
                percentualINSS = 9.0;
            }
            else if (salarioBase <= 3856.94)
            {
                descontoINSS = salarioBase * 0.12; // 12%
                percentualINSS = 12.0;
            }
            else if (salarioBase <= 7507.49)
            {
                descontoINSS = salarioBase * 0.14; // 14%
                percentualINSS = 14.0;
            }
            else
            {
                descontoINSS = 7507.49 * 0.14; // Valor máximo para a alíquota de 14%
                percentualINSS = 14.0;
            }

            return descontoINSS;
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        //regra de cálculo baseada na fonte https://www.pontotel.com.br/calcular-irrf/#:~:text=como%20a%20seguir!-,Como%20funciona%20o%20c%C3%A1lculo%20do%20IRRF%20no%20sal%C3%A1rio%3F,realizar%20o%20c%C3%A1lculo%20do%20IRRF.
        private double CalculaDescontoIRRF(double salarioBase, double descontoINSS, out double percentualIRRF)
        {
            double descontoIRRF = 0.0;
            double salarioBaseAjustado = salarioBase - descontoINSS;

            if (salarioBaseAjustado <= 1903.98)
            {
                descontoIRRF = 0;
                percentualIRRF = 0.0;


            }
            else if (salarioBaseAjustado <= 2826.65)
            {
                descontoIRRF = (salarioBaseAjustado * 0.075) - 142.80;
                percentualIRRF = 7.5;

            }
            else if (salarioBaseAjustado <= 3751.05)
            {
                descontoIRRF = (salarioBaseAjustado * 0.15) - 354.80;
                percentualIRRF = 15.0;

            }
            else if (salarioBaseAjustado <= 4664.68)
            {
                descontoIRRF = (salarioBaseAjustado * 0.225) - 636.13;
                percentualIRRF = 22.5;

            }
            else
            {
                descontoIRRF = (salarioBaseAjustado * 0.275) - 869.36;
                percentualIRRF = 27.0;

            }
            return descontoIRRF;
        }

        [HttpGet("ReenviarHolerite/{id}")]
        //[Authorize]
        public ActionResult<HoleriteModel> ReenviarHolerite(int id)
        {
            var holerite = _holeriteRepositorio.BuscarHoleritePorId(id);
            if (holerite == null)
            {
                return NotFound((new { message = "Holerite não encontrado" }));
            }
            var colaborador = _colaboradorRepositorio.BuscarPorId(holerite.ColaboradorId);

            var emailMessage = new MailMessage();
            emailMessage.Subject = "Holerite";
            emailMessage.From = new MailAddress("rhprojectr@gmail.com");
            emailMessage.To.Add(colaborador.Email);
            emailMessage.IsBodyHtml = true;

            emailMessage.Body = "<h1>Holerite</h1>";

            var attachmentPath = holerite.Id.ToString() + ".pdf";

            Attachment attachment = new Attachment(attachmentPath, MediaTypeNames.Application.Pdf);
            emailMessage.Attachments.Add(attachment);

            var client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new NetworkCredential("rhprojectr@gmail.com", "jdsn ctkq kasc reew");
            client.EnableSsl = true;

            try
            {
                client.Send(emailMessage);
                Console.WriteLine("banana");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return Ok(holerite);
        }

        [HttpGet("Holerites-colaborador/{id}")]
        //[Authorize]
        public ActionResult<HoleriteModel> HoleritesColaborador(int id)
        {
            var holerites = _holeriteRepositorio.BuscarTodosHoleritesColaborador(id);
            if (holerites == null)
            {
                return NotFound((new { message = "Holerites não encontrado" }));
            }
            return Ok(holerites);
        }

        [HttpGet("Holerites-layout/{id}")]
        //[Authorize]
        public ActionResult<HoleriteModel> Holeritelayout(int id)
        {
            var holerites = _holeriteRepositorio.BuscarHoleritePorId(id);
            if (holerites == null)
            {
                return NotFound((new { message = "Holerites não encontrado" }));
            }

            var colaborador = _colaboradorRepositorio.BuscarPorId(holerites.ColaboradorId);
            var cargo = _cargoRepositorio.BuscarPorId(colaborador.CargoId);
            var tipoHolerite = _tipoHoleriteRepositorio.BuscarPorId(holerites.Tipo);
            var empresa = _empresaRepositorio.BuscarPorId(colaborador.EmpresaId);
            return Ok(new { holerite = holerites, colaborador = colaborador, cargo = cargo, tipoHolerite = tipoHolerite, empresa = empresa });
        }

        [HttpGet("Relatorio/{mes}/{ano}")]
        public IActionResult GerarRelatorio(string mes, string ano)
        {

            var renderer = new ChromePdfRenderer();

            renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;

            var pdf = renderer.RenderUrlAsPdf("http://localhost:4200/relatorio/" +  mes + "/" + ano );
            pdf.SaveAs("Relatorio.pdf");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Relatorio.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "Relatorio.pdf");
        }

    }

}
