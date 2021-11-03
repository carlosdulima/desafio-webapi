using Microsoft.AspNetCore.Mvc;
using Softplan.CalculaJuros.Api.Requests;
using Softplan.CalculaJuros.ApplicationCore.Command;
using Softplan.CalculaJuros.ApplicationCore.UseCases.Interfaces;
using System.Threading.Tasks;

namespace Softplan.CalculaJuros.Api.Controllers
{
    [ApiController]
    [Route("calculajuros")]
    public class CalculaJurosController : ControllerBase
    {
        private readonly IExecuteCalculaJuros _executeCalculaJuros;

        public CalculaJurosController(IExecuteCalculaJuros executeCalculaJuros)
        {
            _executeCalculaJuros = executeCalculaJuros;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery] CalculaJurosRequest request)
        {
            //poderia usar um mapper como o automapper
            var command = new CalculaJurosCommand
            {
                ValorInicial = request.ValorInicial,
                Meses = request.Meses
            };

            return Ok(await _executeCalculaJuros.Execute(command));
        }
    }
}
