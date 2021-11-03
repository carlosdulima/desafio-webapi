using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softplan.CalculaJuros.ApplicationCore.UseCases;
using Softplan.CalculaJuros.ApplicationCore.UseCases.Interfaces;

namespace Softplan.CalculaJuros.ApplicationCore.Configuration
{
    public static class CoreCalculaJurosConfiguration
    {
        public static void ConfigureCoreCalculaJuros(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDependencies(configuration);
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IExecuteCalculaJuros, ExecuteCalculaJuros>();
        }
    }
}
