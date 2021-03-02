using BrokerCommon.Event;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Messaging;
using Silverback.Messaging.Configuration;

namespace Bootstrap.Providers
{
    public static class SilverbackConfiguration
    {
        public static IServiceCollection AddSilverbackConfigure(this IServiceCollection services)
        {
            services
            .AddSilverback()
            .WithConnectionToMessageBroker(options => options
                .AddKafka()
                .AddInboundConnector()
                .AddOutboundConnector());
            return services;
        }
        public static IApplicationBuilder SilverbackConfigure(this IApplicationBuilder app, BusConfigurator busConfigurator, IConfiguration configuration)
        {
            var uri = configuration.GetSection("KafkaConfiguration:Uri").Value;
            var ConfigurationConsumer = new KafkaConsumerConfig
            {
                BootstrapServers = uri,
                GroupId = "Geo"
            };
            var ConfigurationProducer = new KafkaProducerConfig
            {
                BootstrapServers = uri
            };
            busConfigurator.Connect(endpoints => endpoints
                .AddInbound(
                    new KafkaConsumerEndpoint("create-geolocalization")
                    {
                        Configuration = ConfigurationConsumer
                    })
                .AddOutbound<GeocodificadoEvent>(
                    new KafkaProducerEndpoint("geocodification")
                    {
                        Configuration = ConfigurationProducer
                    }));


            return app;
        }
    }
}
