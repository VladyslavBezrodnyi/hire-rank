using AutoMapper;
using HireRank.Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace HireRank.Application
{
    public static class ServiceExtensions
    {
        private static readonly Assembly _assembly = typeof(ServiceExtensions).Assembly;

        public static IServiceCollection AddAppMediatR(this IServiceCollection services)
        {
            var assembly = _assembly;
            services.AddMediatR(assembly);
            services.AddScopedServices();

            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            return services.AddAutoMapper(_assembly);
        }

        private static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            var scopedInterface = typeof(IScopedService);

            var scopedServices = _assembly
                .ExportedTypes
                .Where(type => scopedInterface.IsAssignableFrom(type) && type != scopedInterface);

            foreach (var serviceType in scopedServices)
            {
                var serviceInterface = serviceType.GetInterface($"I{serviceType.Name}");
                if (serviceInterface != null)
                {
                    services.AddScoped(serviceInterface, serviceType);
                }
                else
                {
                    services.AddScoped(serviceType);
                }
            }

            return services;
        }
    }
}
