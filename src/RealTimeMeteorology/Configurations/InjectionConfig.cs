using Microsoft.Extensions.DependencyInjection;
using RealTimeMeteorology.Interfaces;
using RealTimeMeteorology.Service;
using RealTimeMeteorology.Timer;

namespace RealTimeMeteorology.Configurations
{
    public static class InjectionConfig
    {
        public static IServiceCollection AddInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ISensorsService, SensorsService>();
            services.AddSingleton<TimerManager>();
            return services;
        }
    }
}
