using Application.UseCase.V1.ConsumerGeolocalization;
using BrokerCommon.Event;
using MediatR;
using Silverback.Messaging.Subscribers;
using System;
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
        public async Task OnGeolocalization(GeolocalizacionEvent message)
        {
            var data = message.Payload;

            await _mediator.Send(new Request
            {
                Id = data.Id,
                Calle = data.Calle,
                Codigo_Postal = data.Codigo_Postal,
                Cuidad = data.Cuidad,
                Numero = data.Numero,
                Pais = data.Pais,
                Provincia = data.Provincia
            });
            
        }
    }
}
