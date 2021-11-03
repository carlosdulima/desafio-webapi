using Refit;
using System.Threading.Tasks;

namespace Softplan.CalculaJuros.Infra.Gateways.Softplan
{
    public interface ITaxaJurosHttp
    {
        [Get("/taxaJuros")]
        Task<decimal> Get();
    }
}
