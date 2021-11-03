using AutoBogus;
using FluentAssertions;
using Moq;
using Softplan.CalculaJuros.Infra.Gateways;
using Softplan.CalculaJuros.Infra.Gateways.Softplan;
using Softplan.Commons.Resilience.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.CalculaJuros.Tests.Infra.Gateways
{
    public class TaxaJurosGatewayTest
    {
        private readonly TaxaJurosGateway _gateway;
        private readonly Mock<ITaxaJurosHttp> _http;

        public TaxaJurosGatewayTest()
        {
            _http = new Mock<ITaxaJurosHttp>();
            _gateway = new TaxaJurosGateway(_http.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnSuccess_WhenGatewayReturnsSuccess()
        {
            var response = AutoFaker.Generate<decimal>();

            _http
                .Setup(f => f.Get())
                .ReturnsAsync(response);

            var result = await _gateway.Get();

            result.Should().Be(response);

            _http.Verify(f => f.Get(), Times.Once);
        }

    }
}
