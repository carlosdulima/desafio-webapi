using Softplan.CalculaJuros.ApplicationCore.Gateways;
using Softplan.CalculaJuros.Infra.Gateways.Softplan;
using System.Threading.Tasks;

namespace Softplan.CalculaJuros.Infra.Gateways
{
    public class TaxaJurosGateway : ITaxaJurosGateway
    {
        private readonly ITaxaJurosHttp _taxaJurosHttp;

        public TaxaJurosGateway(ITaxaJurosHttp taxaJurosHttp)
        {
            _taxaJurosHttp = taxaJurosHttp;
        }

        public Task<decimal> Get()
        {
            return _taxaJurosHttp.Get();
        }
    }
}
