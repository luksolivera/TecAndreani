using Bootstrap.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddMalditoApiVersioning();
            services.AddMediatr();
            services.ConfigurePersistenceServices(configuration);
            services.AddQuerys();
            services.AddCommands();
            services.AddServices();
            services.AddSilverbackConfigure();
            return services;
        }

    }
}
