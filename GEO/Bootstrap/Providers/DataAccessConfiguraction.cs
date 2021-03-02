
using Domain.Interface.Command;
using Domain.Interface.Queries;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Queries;
using Persistence.Command;

namespace Bootstrap.Providers
{
    public static class DataAccessConfiguraction
    {
        public static IServiceCollection AddQuerys(this IServiceCollection services)
        {
            services.AddTransient<IGenericsQuery, GenericsQuery>();
            return services;
        }
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddTransient<IGenericsCommand, GenericsCommand>();
            return services;
        }
    }
}
