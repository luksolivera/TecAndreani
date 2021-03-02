using Application.Services;
using Domain.Interface.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap.Providers
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IKafkaPublisher, KafkaPublisher>();

            return services;
        }
    }
}
