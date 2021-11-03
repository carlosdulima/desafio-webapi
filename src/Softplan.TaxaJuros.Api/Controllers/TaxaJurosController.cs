using Microsoft.AspNetCore.Mvc;
using Softplan.TaxaJuros.ApplicationCore.UseCases.Interfaces;
using System.Threading.Tasks;

namespace Softplan.TaxaJuros.Api.Controllers
{
    [ApiController]
    [Route("taxaJuros")]
    public class TaxaJurosController : ControllerBase
    {
        private readonly IGetTaxaJuros _getTaxaJuros;

        public TaxaJurosController(IGetTaxaJuros getTaxaJuros)
        {
            _getTaxaJuros = getTaxaJuros;
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> Get()
        {
            return Ok(await _getTaxaJuros.Get());
        }
    }
}
