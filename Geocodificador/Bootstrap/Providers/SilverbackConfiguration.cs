using Application.Services;
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
                .AddOutboundConnector())
            .UseModel()
            .AddScopedSubscriber<KafkaConsumer>();
            return services;
        }
        public static IApplicationBuilder SilverbackConfigure(this IApplicationBuilder app, BusConfigurator busConfigurator, IConfiguration configuration)
        {
            var uri = configuration.GetSection("KafkaConfiguration:Uri").Value;
            var maxFailedAttempts = 3;
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
                    }, policy => policy.Chain(
                                    policy.Move(new KafkaProducerEndpoint("retry_5M_topic") { Configuration = new KafkaProducerConfig { BootstrapServers = configuration["Kafka:Uri"] } })
                                      .MaxFailedAttempts(maxFailedAttempts),
                                    policy.Move(new KafkaProducerEndpoint("retry_30M_topic") { Configuration = new KafkaProducerConfig { BootstrapServers = configuration["Kafka:Uri"] } })
                                      .MaxFailedAttempts(maxFailedAttempts),
                                    policy.Move(new KafkaProducerEndpoint("retry_60M_topic") { Configuration = new KafkaProducerConfig { BootstrapServers = configuration["Kafka:Uri"] } })
                                      .MaxFailedAttempts(maxFailedAttempts),
                                    policy.Move(new KafkaProducerEndpoint("failed_topic") { Configuration = new KafkaProducerConfig { BootstrapServers = configuration["Kafka:Uri"] } })
                                      .MaxFailedAttempts(maxFailedAttempts)))
                .AddOutbound<GeocodificadoEvent>(
                    new KafkaProducerEndpoint("geocodification")
                    {
                        Configuration = ConfigurationProducer
                    }));


            return app;
        }
    }
}
