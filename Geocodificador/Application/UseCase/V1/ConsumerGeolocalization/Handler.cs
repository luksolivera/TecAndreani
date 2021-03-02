using Application.Services;
using Domain.Errors;
using Domain.Interface.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCase.V1.ConsumerGeolocalization
{
    public class Handler : IRequestHandler<Request, Domain.Errors.ApiResponse<bool>>
    {
        private readonly IKafkaPublisher _publisher;
        private readonly IOpenStreetMapClient _client;

        public Handler(IKafkaPublisher publisher, IOpenStreetMapClient client)
        {
            _publisher = publisher;
            _client = client;
        }

        public async Task<Domain.Errors.ApiResponse<bool>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var q = BuildQuery(request);
                var response = await _client.GetGelocation(q);

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.FirstOrDefault();
                    if(content == null)
                        await _publisher.PublishGeocodification(request.Id.ToString(), "","");
                    else
                        await _publisher.PublishGeocodification(request.Id.ToString(), content.Lat, content.Lon);
                }

                return new Domain.Errors.ApiResponse<bool>(true);

            }
            catch (Exception ex)
            {
                request.AddNotification("Exception", $"Se ha lanzado una exception: {ex.Message}");
                return new Domain.Errors.ApiResponse<bool>(request.Notifications, HttpStatusCode.UnprocessableEntity);
            }


        }
        private string BuildQuery(Request request)
        {
            var q = new StringBuilder();
            q.Append($"{request.Calle.Replace(' ', '+')},");
            q.Append($"{request.Numero.Replace(' ', '+')},");
            q.Append($"{request.Codigo_Postal.Replace(' ', '+')},");
            q.Append($"{request.Cuidad.Replace(' ', '+')},");
            q.Append($"{request.Provincia.Replace(' ', '+')},");
            q.Append($"{request.Pais.Replace(' ', '+')}");

            return q.ToString();
        }
    }
}
