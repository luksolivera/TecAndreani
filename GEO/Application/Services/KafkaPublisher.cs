using BrokerCommon.Dto;
using BrokerCommon.Event;
using Domain.Entitiies;
using Domain.Interface.Services;
using Silverback.Messaging.Publishing;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KafkaPublisher : IKafkaPublisher
    {
        private readonly IPublisher _publisher;

        public KafkaPublisher(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task publishGeolocalization(Geolocalizacion geolocalizacion)
        {
            await _publisher.PublishAsync(new GeolocalizacionEvent(new GeolocalizacionEventDto 
            {
                Provincia = geolocalizacion.Provincia,
                Calle = geolocalizacion.Calle,
                Codigo_Postal = geolocalizacion.Codigo_Postal,
                Cuidad = geolocalizacion.Cuidad,
                Id = geolocalizacion.Id,
                Numero = geolocalizacion.Numero,
                Pais = geolocalizacion.Pais
            }));
        }
    }
}
