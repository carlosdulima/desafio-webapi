using Softplan.TaxaJuros.ApplicationCore.Gateways;
using System.Threading.Tasks;

namespace Softplan.TaxaJuros.Infra.Gateways
{
    public class TaxaJurosGateway : ITaxaJurosGateway
    {
        public Task<decimal> Get()
        {
            //aqui pode pegar os dados de um repositorio, uma api, settings da aplicação, de qualquer lugar
            //a função deste gateway é abstrair para poder ser chaveado para qualquer lugar sem necessidade
            //de alterar as camadas de cima, mantendo a flexibilidade de mudar a classe inclusive por Toogles
            return Task.FromResult(0.01m);
        }
    }
}
