using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softplan.TaxaJuros.ApplicationCore.Gateways;
using Softplan.TaxaJuros.Infra.Gateways;

namespace Softplan.TaxaJuros.Infra.Configuration
{
    public static class InfraTaxaJurosConfiguration
    {
        public static void ConfigureInfraTaxaJuros(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDependencies(configuration);
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITaxaJurosGateway, TaxaJurosGateway>();
        }
    }
}
