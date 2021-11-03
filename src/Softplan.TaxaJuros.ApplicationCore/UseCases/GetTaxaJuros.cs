using Softplan.TaxaJuros.ApplicationCore.Gateways;
using Softplan.TaxaJuros.ApplicationCore.UseCases.Interfaces;
using System.Threading.Tasks;

namespace Softplan.TaxaJuros.ApplicationCore.UseCases
{
    public class GetTaxaJuros : IGetTaxaJuros
    {
        private readonly ITaxaJurosGateway _taxaJurosGateway;

        public GetTaxaJuros(ITaxaJurosGateway taxaJurosGateway)
        {
            _taxaJurosGateway = taxaJurosGateway;
        }

        public Task<decimal> Get()
        {
            return _taxaJurosGateway.Get();
        }
    }
}
