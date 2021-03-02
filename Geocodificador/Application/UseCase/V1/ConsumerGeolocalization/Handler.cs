using Application.Services;
using Domain.Errors;
using Domain.Interface.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCase.V1.ConsumerGeolocalization
{
    public class Handler : IRequestHandler<Request, ApiResponse<bool>>
    {
        private readonly IKafkaPublisher _publisher;
        private readonly IOpenStreetMapClient _client;

        public Handler(IKafkaPublisher publisher, IOpenStreetMapClient client)
        {
            _publisher = publisher;
            _client = client;
        }

        public async Task<ApiResponse<bool>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var q = BuildQuery(request);
                var response = await _client.GetGelocation(q);

                if (response.IsSuccessStatusCode)
                {
                    await _publisher.PublishGeocodification(request.Id.ToString(), response.Content.Lat, response.Content.Lon);
                }

                return new ApiResponse<bool>(true);

            }
            catch (Exception ex)
            {
                request.AddNotification("Exception", $"Se ha lanzado una exception: {ex.Message}");
                return new ApiResponse<bool>(request.Notifications, HttpStatusCode.UnprocessableEntity);
            }


        }
        private string BuildQuery(Request request)
        {
            var q = new StringBuilder();
            q.Append($"{request.Calle},");
            q.Append($"{request.Numero},");
            q.Append($"{request.Codigo_Postal},");
            q.Append($"{request.Cuidad},");
            q.Append($"{request.Provincia},");
            q.Append($"{request.Pais},");

            return q.ToString();
        }
    }
}
