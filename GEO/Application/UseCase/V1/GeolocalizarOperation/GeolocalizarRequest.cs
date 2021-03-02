using Domain.Errors;
using Flunt.Notifications;
using MediatR;

namespace Application.UseCase.GeolocalizarOperation
{
    public class GeolocalizarRequest : Notifiable, IRequest<ApiResponse<GeolocalizarResponse>>
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Cuidad { get; set; }
        public string Codigo_Postal{ get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
    }
}
