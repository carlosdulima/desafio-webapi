using AutoBogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Softplan.TaxaJuros.Api;
using Softplan.TaxaJuros.ApplicationCore.UseCases.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.TaxaJuros.Tests.Api
{
    public class TaxaJurosControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly Mock<IGetTaxaJuros> _useCase;
        private readonly string _baseUrl;
        private readonly Uri _endpointGetTaxaJuros;

        public TaxaJurosControllerTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped(sp => _useCase.Object);
                    });
                }).CreateClient();

            _baseUrl = "http://localhost:5001";
            _endpointGetTaxaJuros = new Uri(_baseUrl + "/taxaJuros");
            _useCase = new Mock<IGetTaxaJuros>();
        }

        [Fact]
        public async Task GetTaxaJuros_ShouldReturnSuccess_WhenRequestIsValid()
        {
            var response = AutoFaker.Generate<decimal>();

            _useCase.Setup(f => f.Get()).ReturnsAsync(response);

            var request = new HttpRequestMessage { RequestUri = _endpointGetTaxaJuros, Method = HttpMethod.Get };

            var result = await _client.SendAsync(request).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
                        
            var responseAsString = JsonSerializer.Deserialize<decimal>(
                await result.Content.ReadAsStringAsync().ConfigureAwait(false)
            );

            responseAsString.Should().Be(response);

            _useCase.Verify(f => f.Get(), Times.Once());
        }

        [Fact]
        public async Task GetTaxaJuros_ShouldReturnFailure__WhenRequestIsFail()
        {
            _useCase.Setup(f => f.Get()).ThrowsAsync(new Exception());

            var request = new HttpRequestMessage { RequestUri = _endpointGetTaxaJuros, Method = HttpMethod.Get };

            var result = await _client.SendAsync(request).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            _useCase.Verify(f => f.Get(), Times.Once());
        }
    }
}
