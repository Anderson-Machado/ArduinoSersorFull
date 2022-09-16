using Microsoft.Extensions.DependencyInjection;
using RealTimeMeteorology.Interfaces;
using RealTimeMeteorology.Model;
using RealTimeMeteorology.Service;
using RealTimeMeteorology.Timer;
using System.Collections.Generic;

namespace RealTimeMeteorology.Configurations
{
    public static class InjectionConfig
    {
        public static IServiceCollection AddInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ISensorsService, SensorsService>();
            services.AddSingleton<TimerManager>();
            services.AddScoped<List<ChartModel>>();
            return services;
        }
    }
}
