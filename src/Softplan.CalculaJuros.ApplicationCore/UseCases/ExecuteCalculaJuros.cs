using Softplan.CalculaJuros.ApplicationCore.Command;
using Softplan.CalculaJuros.ApplicationCore.Domains;
using Softplan.CalculaJuros.ApplicationCore.Gateways;
using Softplan.CalculaJuros.ApplicationCore.UseCases.Interfaces;
using System.Threading.Tasks;

namespace Softplan.CalculaJuros.ApplicationCore.UseCases
{
    public class ExecuteCalculaJuros : IExecuteCalculaJuros
    {
        private readonly ITaxaJurosGateway _taxaJurosGateway;

        public ExecuteCalculaJuros(ITaxaJurosGateway taxaJurosGateway)
        {
            _taxaJurosGateway = taxaJurosGateway;
        }

        public async Task<string> Execute(CalculaJurosCommand calculaJurosCommand)
        {
            var resultTaxaJuros = await _taxaJurosGateway.Get();

            var resultCalculoJuros = new CalcularJurosCompostos(
                calculaJurosCommand.ValorInicial, 
                calculaJurosCommand.Meses, 
                resultTaxaJuros);

            return resultCalculoJuros.GetTruncatedValue(2);
        }
    }
}
