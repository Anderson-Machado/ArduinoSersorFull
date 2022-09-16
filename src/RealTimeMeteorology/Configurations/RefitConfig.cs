using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeMeteorology.Facedes;
using Refit;
using System;

namespace RealTimeMeteorology.Configurations
{
    public static class RefitConfig
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services,
       IConfiguration configuration)
        {

            services
                .AddRefitClient<IArduino>()
                .ConfigureHttpClient(c =>
                {
                    var apiUserBaseUrl = configuration.GetSection("BaseConfigApi").GetSection("ApiArduino").Value;
                    c.BaseAddress = new Uri(apiUserBaseUrl);
                });

            return services;
        }
    }
}
