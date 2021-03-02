using Domain.Errors;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.V1.ConsumerGeolocalization
{
    public class Request : Notifiable, IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Cuidad { get; set; }
        public string Codigo_Postal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
    }
}
