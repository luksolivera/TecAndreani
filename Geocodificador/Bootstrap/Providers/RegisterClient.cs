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
        private const int retryCount = 3;
        public static IServiceCollection AddAccountManagementApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            var uri = configuration.GetValue<string>("OpenStreetMap:Uri");

            services.AddRefitClient<IOpenStreetMapClient>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri(uri));


            return services;
        }
    }
}
