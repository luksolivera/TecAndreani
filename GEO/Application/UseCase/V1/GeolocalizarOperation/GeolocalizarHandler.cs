using Domain.Entitiies;
using Domain.Enums;
using Domain.Errors;
using Domain.Interface.Command;
using Domain.Interface.Services;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.UseCase.GeolocalizarOperation
{
    public class GeolocalizarHandler : IRequestHandler<GeolocalizarRequest, ApiResponse<GeolocalizarResponse>>
    {
        private readonly IGenericsCommand _command;
        private readonly IKafkaPublisher _publisher;

        public GeolocalizarHandler(IGenericsCommand command, IKafkaPublisher publisher)
        {
            _command = command;
            _publisher = publisher;
        }

        public async Task<ApiResponse<GeolocalizarResponse>> Handle(GeolocalizarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Any validations

                var entity = new Geolocalizacion
                {
                    Calle = request.Calle,
                    Codigo_Postal = request.Codigo_Postal,
                    Cuidad = request.Cuidad,
                    EstadoId = (int)Enums.Estado.Procesado,
                    Latitud = "",
                    Longuitud = "",
                    Numero = request.Numero,
                    Pais = request.Pais,
                    Provincia = request.Provincia
                };

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _command.Add(entity);
                    await _command.SaveChangeAsync();
                    await _publisher.publishGeolocalization(entity);

                    scope.Complete();
                }

                return new ApiResponse<GeolocalizarResponse>(new GeolocalizarResponse
                {
                    Id = entity.Id.ToString()
                });

            }
            catch(Exception ex)
            {
                request.AddNotification("Exception", $"Se ha lanzado una exception: {ex.Message}");
                return new ApiResponse<GeolocalizarResponse>(request.Notifications, HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
