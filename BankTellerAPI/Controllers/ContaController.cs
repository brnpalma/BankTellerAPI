using BankTellerAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using BankTeller.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BankTeller.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController(ILogger<ContaController> logger, IBankService bankService) : ControllerBase
    {
        private readonly ILogger<ContaController> _logger = logger;
        private readonly IBankService _bankService = bankService;

        [HttpPost(Name = "CadastrarNovasContas")]
        public IActionResult CadastrarNovasContas()
        {
            return Ok();
        }

        [HttpGet(Name = "ConsultarContasPorNomeCliente")]
        public async Task<ActionResult<IEnumerable<Conta>>> ConsultarContasPorNomeCliente([FromQuery][Required] string nomeCliente)
        {
            var listaContas = await _bankService.ConsultarContasPorNomeCliente(nomeCliente);
            return Ok(listaContas);
        }

        [HttpGet(Name = "ConsultarContasPorDocumento")]
        public ActionResult<IEnumerable<Conta>> ConsultarContasPorDocumentoCliente([FromQuery][Required] string documentoCliente)
        {
            var conta = _bankService.ConsultarContasPorDocumento(documentoCliente);
            return Ok(conta);
        }

        [HttpPut(Name = "InativarContas")]
        public IActionResult InativarContas()
        {
            return Ok();

        }

        [HttpPut(Name = "TransferirValoresEntreContas")]
        public IActionResult TransferirValoresEntreContas()
        {
            return Ok();
        }
    }
}
