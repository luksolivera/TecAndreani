using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap.Providers
{
    public static class ApiVersionConfiguration
    {
        public static IServiceCollection AddMalditoApiVersioning(this IServiceCollection services)
        {
            services.AddTransient<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }
    }
}
