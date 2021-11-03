using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Softplan.CalculaJuros.Api.Controllers
{
    [ApiController]
    [Route("/showmethecode")]
    public class ShowMeTheCodeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return Ok(await Task.FromResult("https://github.com/carlosdulima/desafio-webapi"));
        }
    }
}
