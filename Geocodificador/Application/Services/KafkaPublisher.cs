using Domain.Interface.Services;
using Silverback.Messaging.Publishing;
using System.Threading.Tasks;
using BrokerCommon.Event;
using BrokerCommon.Dto;

namespace Application.Services
{
    public class KafkaPublisher : IKafkaPublisher
    {
        private readonly IPublisher _publisher;

        public KafkaPublisher(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task PublishGeocodification(string id, string latitud, string longuitud)
        {
            await _publisher.PublishAsync(new GeocodificadoEvent(new GeocodificadoEventDto 
            {
                Id = id,
                Latitud = latitud,
                Longuitud = longuitud
            }));
        }
    }
}
