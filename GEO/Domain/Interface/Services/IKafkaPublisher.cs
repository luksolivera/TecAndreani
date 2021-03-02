using Domain.Entitiies;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IKafkaPublisher
    {
        Task publishGeolocalization(Geolocalizacion geolocalizacion);
    }
}
