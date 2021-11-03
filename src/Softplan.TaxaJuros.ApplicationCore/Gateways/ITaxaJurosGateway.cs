using System.Threading.Tasks;

namespace Softplan.TaxaJuros.ApplicationCore.Gateways
{
    public interface ITaxaJurosGateway
    {
        Task<decimal> Get();
    }
}
