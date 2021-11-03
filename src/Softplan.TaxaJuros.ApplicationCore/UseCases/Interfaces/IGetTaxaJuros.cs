using System.Threading.Tasks;

namespace Softplan.TaxaJuros.ApplicationCore.UseCases.Interfaces
{
    public interface IGetTaxaJuros
    {
        Task<decimal> Get();
    }
}
