using Domain.Errors;
using Flunt.Notifications;
using MediatR;

namespace Application.UseCase.V1.GeocodificarOperation
{
    public class GeocodificarRequest : Notifiable, IRequest<ApiResponse<GeocodificarResponse>>
    {
        public string Id { get; set; }
    }
}
