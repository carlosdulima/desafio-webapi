using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;
using Softplan.CalculaJuros.ApplicationCore.Gateways;
using Softplan.CalculaJuros.Infra.Gateways;
using Softplan.CalculaJuros.Infra.Gateways.Softplan;
using Softplan.Commons.Resilience.Builder;
using Softplan.Commons.Resilience.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Softplan.CalculaJuros.Infra.Configuration
{
    public static class InfraCalculaJurosConfiguration
    {
        public static void ConfigureInfraCalculaJuros(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDependencies(configuration);
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITaxaJurosGateway, TaxaJurosGateway>();

            services.ConfigurePollyAndRefit<ITaxaJurosHttp>(configuration, ApiCatalogo.TAXA_JUROS);
        }

        public static void ConfigurePollyAndRefit<T>(
            this IServiceCollection services, IConfiguration configuration, string serviceName) 
            where T : class
        {
            var proxies = new List<ApiSettings>();
            configuration.GetSection(nameof(ApiSettings)).Bind(proxies);

            var proxie = proxies.FirstOrDefault(f => f.Name == serviceName);

            AddRefit<T>(services, configuration, proxie);

            services.AddSingleton(new PolicyBuilder()
                .UseExecutorAsync()
                .WithRetry(proxie.PoliciesSettings.Retry)
                .WithWaitRetry(proxie.PoliciesSettings.Retry, i => TimeSpan.FromSeconds(i)).Build());
        }

        private static void AddRefit<T>(IServiceCollection services, IConfiguration configuration, ApiSettings proxie)
            where T : class
        {      
            services.TryAddScoped<IHttpPolicyAsyncBuilder, HttpPolicyAsyncBuilder>();

            var serializerSettings = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var settings = new RefitSettings(new SystemTextJsonContentSerializer(serializerSettings));

            services.AddRefitClient<T>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(proxie.Url));
        }
    }
}
