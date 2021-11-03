using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softplan.TaxaJuros.ApplicationCore.UseCases;
using Softplan.TaxaJuros.ApplicationCore.UseCases.Interfaces;

namespace Softplan.TaxaJuros.ApplicationCore.Configuration
{
    public static class CoreTaxaJurosConfiguration
    {
        public static void ConfigureCoreTaxaJuros(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDependencies(configuration);
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGetTaxaJuros, GetTaxaJuros>();
        }
    }
}
