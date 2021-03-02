using Domain.Errors;
using Domain.Interface.Queries;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCase.V1.GeocodificarOperation
{
    public class GeocodificarHandler : IRequestHandler<GeocodificarRequest, ApiResponse<GeocodificarResponse>>
    {
        private readonly IGenericsQuery _genericsQuery;

        public GeocodificarHandler(IGenericsQuery genericsQuery)
        {
            _genericsQuery = genericsQuery;
        }

        public async Task<ApiResponse<GeocodificarResponse>> Handle(GeocodificarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Any Validation
                if (string.IsNullOrWhiteSpace(request.Id))
                {
                    request.AddNotification("Id", $"Id invalido");
                    return new ApiResponse<GeocodificarResponse>(request.Notifications, HttpStatusCode.BadRequest);
                }
                var geocoficated = await _genericsQuery.GetGeocodificar(request.Id);
                if(geocoficated == null)
                {
                    request.AddNotification("Geolocalizacion", $"no existe la Geolocalizacion con Id {request.Id}");
                    return new ApiResponse<GeocodificarResponse>(request.Notifications, HttpStatusCode.NotFound);
                }

                return new ApiResponse<GeocodificarResponse>(new GeocodificarResponse 
                {
                    Id = geocoficated.Id,
                    Estado = geocoficated.Estado,
                    Latitud = geocoficated.Latitud,
                    Longuitud = geocoficated.Longuitud
                });
            }
            catch (Exception ex)
            {
                request.AddNotification("Exception", $"Se ha lanzado una exception: {ex.Message}");
                return new ApiResponse<GeocodificarResponse>(request.Notifications, HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
