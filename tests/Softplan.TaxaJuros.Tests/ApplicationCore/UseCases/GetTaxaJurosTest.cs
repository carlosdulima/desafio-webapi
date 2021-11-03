using AutoBogus;
using FluentAssertions;
using Moq;
using Softplan.TaxaJuros.ApplicationCore.Gateways;
using Softplan.TaxaJuros.ApplicationCore.UseCases;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.TaxaJuros.Tests.ApplicationCore.UseCases
{
    public class GetTaxaJurosTest
    {
        private readonly GetTaxaJuros _useCase;
        private readonly Mock<ITaxaJurosGateway> _gateway;

        public GetTaxaJurosTest()
        {
            _gateway = new Mock<ITaxaJurosGateway>();
            _useCase = new GetTaxaJuros(_gateway.Object);
        }

        [Fact]
        public async Task GetTaxaJuros_ShouldReturnSuccess_WhenRequestIsValid()
        {
            var response = AutoFaker.Generate<decimal>();

            _gateway
                .Setup(f => f.Get())
                .ReturnsAsync(response);

            var result = await _useCase.Get();

            result.Should().Be(response);

            _gateway.Verify(f => f.Get(), Times.Once);
        }
    }
}
