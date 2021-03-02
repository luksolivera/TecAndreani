using Application.UseCase.V1.ConsumerGeocodificar;
using BrokerCommon.Event;
using MediatR;
using Silverback.Messaging.Subscribers;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KafkaConsumer : ISubscriber
    {
        private readonly IMediator _mediator;

        public KafkaConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGeocodificated(GeocodificadoEvent message)
        {
            var data = message.Payload;

            await _mediator.Send(new ConsumerGeocodificarRequest 
            {
                Id = data.Id,
                Latitud = data.Latitud,
                Longuitud = data.Longuitud
            });
        }
    }
}
