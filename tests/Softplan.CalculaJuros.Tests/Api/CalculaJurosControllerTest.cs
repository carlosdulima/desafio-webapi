using AutoBogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Softplan.CalculaJuros.Api;
using Softplan.CalculaJuros.ApplicationCore.Command;
using Softplan.CalculaJuros.ApplicationCore.UseCases.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.CalculaJuros.Tests.Api
{
    public class CalculaJurosControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly Mock<IExecuteCalculaJuros> _useCase;
        private readonly string _baseUrl;
        private readonly Uri _endpointGetTaxaJuros;

        public CalculaJurosControllerTest(WebApplicationFactory<Startup> factory)
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
            _endpointGetTaxaJuros = new Uri(_baseUrl + "/calculajuros");
            _useCase = new Mock<IExecuteCalculaJuros>();
        }

        [Fact]
        public async Task GetTaxaJuros_ShouldReturnSuccess_WhenRequestIsValid()
        {
            var response = AutoFaker.Generate<string>();

            _useCase.Setup(f => f.Execute(It.IsAny<CalculaJurosCommand>())).ReturnsAsync(response);

            var request = new HttpRequestMessage { RequestUri = _endpointGetTaxaJuros, Method = HttpMethod.Get };

            var result = await _client.SendAsync(request).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseAsString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            responseAsString.Should().Be(response);

            _useCase.Verify(f => f.Execute(It.IsAny<CalculaJurosCommand>()), Times.Once());
        }

        [Fact]
        public async Task GetTaxaJuros_ShouldReturnFailure__WhenRequestIsFail()
        {
            _useCase.Setup(f => f.Execute(It.IsAny<CalculaJurosCommand>())).ThrowsAsync(new Exception());

            var request = new HttpRequestMessage { RequestUri = _endpointGetTaxaJuros, Method = HttpMethod.Get };

            var result = await _client.SendAsync(request).ConfigureAwait(false);

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            _useCase.Verify(f => f.Execute(It.IsAny<CalculaJurosCommand>()), Times.Once());
        }
    }
}
