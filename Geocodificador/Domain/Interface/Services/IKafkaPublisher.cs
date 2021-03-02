using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IKafkaPublisher
    {
        Task PublishGeocodification(string id, string latitud, string longuitud );
    }
}
