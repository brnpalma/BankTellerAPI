using BankTellerAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankTellerAPI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ContaController> _logger;

        public ContaController(ILogger<ContaController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<Transferencia> Get()
        //{

        //}
    }
}
