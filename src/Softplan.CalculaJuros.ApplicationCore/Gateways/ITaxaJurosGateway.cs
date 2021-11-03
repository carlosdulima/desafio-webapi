using System.Threading.Tasks;

namespace Softplan.CalculaJuros.ApplicationCore.Gateways
{
    public interface ITaxaJurosGateway
    {
        Task<decimal> Get();
    }
}
