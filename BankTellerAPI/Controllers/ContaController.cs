using BankTeller.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using BankTeller.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BankTeller.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController(IBankService bankService) : ControllerBase
    {
        private readonly IBankService _bankService = bankService;

        [HttpPost("Cadastrar")]
        [EndpointDescription("Cria uma nova conta bancária.")]
        public async Task<ActionResult<ContaResponse>> Cadastrar([FromBody] ContaRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _bankService.CadastrarConta(model);

            if (!resultado.Sucesso)
            {
                return BadRequest(new { Retorno = resultado.Erro });
            }

            return Ok(new ContaResponse
            {
                Retorno = Constantes.ApiCongrats,
                Nome = resultado.Value.Nome,
                Documento = resultado.Value.Documento,
                DataAbertura = resultado.Value.DataAbertura.ToString("dd/MM/yyyy HH:mm"),
                SaldoAtual = resultado.Value.SaldoAtual
            });
        }

        [HttpGet("ConsultarPorDocumento")]
        [EndpointDescription("Busca contas por número de documento do cliente.")]
        public async Task<ActionResult<IEnumerable<Conta>>> ConsultarPorDocumento([FromQuery][Required] string documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _bankService.ConsultarContasPorDocumento(documento);

            if (!resultado.Sucesso)
            {
                return BadRequest(new { Retorno = resultado.Erro });
            }

            return Ok(resultado.Value);
        }

        [HttpGet("ConsultarPorNome")]
        [EndpointDescription("Permite consultar contas usando o nome do cliente (parcial ou completo)")]
        public async Task<ActionResult<IEnumerable<Conta>>> ConsultarPorNome([FromQuery][Required(ErrorMessage = "O nome do cliente é obrigatório.")] string nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _bankService.ConsultarContasPorNome(nome);

            if (!resultado.Sucesso)
            {
                return BadRequest(new { Retorno = resultado.Erro });
            }

            return Ok(resultado.Value);
        }

        [HttpPatch("Inativar")]
        [EndpointDescription("Permite a inativação de uma conta bancária com base no número do documento do titular.")]
        public async Task<ActionResult<ContaResponse>> Inativar([FromBody] InativaRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _bankService.InativarConta(model);

            if (!resultado.Sucesso)
            {
                return BadRequest(new { Retorno = resultado.Erro });
            }

            return Ok(new { Retorno = resultado.Mensagem });
        }

        [HttpPatch("Transferir")]
        [EndpointDescription("Permite transferir valores entre contas bancárias.")]
        public async Task<ActionResult<ContaResponse>> Transferir([FromBody] TransferenciaRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _bankService.Transferir(model);

            if (!resultado.Sucesso)
            {
                return BadRequest(new { Retorno = resultado.Erro });
            }

            return Ok(new { Retorno = resultado.Mensagem });

            // TODO: Registrar o historico de transferencias
            // TODO: FAzer os testes de unidade
            // TODO: Conferir documentação
            // TODO: Revisar tudo
        }
    }
}
