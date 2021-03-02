using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Extensions.Http;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Net;
using Refit;
using Polly;

namespace Bootstrap.Providers
{
    public static class RegisterClient
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            var uri = configuration.GetSection("OpenStreetMap:Uri").Value;

            services.AddRefitClient<IOpenStreetMapClient>()
               .ConfigureHttpClient(c => 
               {
                   c.BaseAddress = new Uri(uri);
                   c.DefaultRequestHeaders.Add("User-Agent", "Refit");
               });


            return services;
        }
    }
}
