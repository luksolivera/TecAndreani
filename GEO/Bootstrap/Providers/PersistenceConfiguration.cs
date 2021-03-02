using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using System.Data;

namespace Bootstrap.Providers
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("SqlServer:ConnectionString").Value;

            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Persistance")));

            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });

            
            return services;
        }

        public static IApplicationBuilder ConfigurePersistence(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext> ();
                context.Database.Migrate();
            }
            return app;
        }
    }
}
