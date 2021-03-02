using Domain.Errors;
using Flunt.Notifications;
using MediatR;

namespace Application.UseCase.V1.ConsumerGeocodificar
{
    public class ConsumerGeocodificarRequest : Notifiable, IRequest<ApiResponse<bool>>
    {
        public string Id { get; set; }
        public string Latitud { get; set; }
        public string Longuitud { get; set; }
    }
}
