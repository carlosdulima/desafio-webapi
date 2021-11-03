using Softplan.CalculaJuros.ApplicationCore.Command;
using System.Threading.Tasks;

namespace Softplan.CalculaJuros.ApplicationCore.UseCases.Interfaces
{
    public interface IExecuteCalculaJuros
    {
        Task<string> Execute(CalculaJurosCommand calculaJurosCommand);
    }
}
