using AutoBogus;
using FluentAssertions;
using Moq;
using Softplan.CalculaJuros.ApplicationCore.Command;
using Softplan.CalculaJuros.ApplicationCore.Gateways;
using Softplan.CalculaJuros.ApplicationCore.UseCases;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.CalculaJuros.Tests.ApplicationCore.UseCases
{
    public class ExecuteCalculaJurosTest
    {
        private readonly ExecuteCalculaJuros _useCase;
        private readonly Mock<ITaxaJurosGateway> _gateway;

        public ExecuteCalculaJurosTest()
        {
            _gateway = new Mock<ITaxaJurosGateway>();
            _useCase = new ExecuteCalculaJuros(_gateway.Object);
        }

        [Fact]
        public async Task GetTaxaJuros_ShouldReturnSuccess_WhenRequestIsValid()
        {
            var response = 0.01m;
            var command = new CalculaJurosCommand { ValorInicial = 100, Meses = 5 };
            var expected = "105,10";

            _gateway
                .Setup(f => f.Get())
                .ReturnsAsync(response);

            var result = await _useCase.Execute(command);

            result.Should().Be(expected);

            _gateway.Verify(f => f.Get(), Times.Once);
        }
    }
}
