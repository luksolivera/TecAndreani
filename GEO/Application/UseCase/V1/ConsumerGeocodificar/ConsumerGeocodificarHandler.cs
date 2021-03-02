using Domain.Entitiies;
using Domain.Enums;
using Domain.Errors;
using Domain.Interface.Command;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCase.V1.ConsumerGeocodificar
{
    public class ConsumerGeocodificarHandler : IRequestHandler<ConsumerGeocodificarRequest, ApiResponse<bool>>
    {
        private readonly IGenericsCommand _command;

        public ConsumerGeocodificarHandler(IGenericsCommand command)
        {
            _command = command;
        }

        public async Task<ApiResponse<bool>> Handle(ConsumerGeocodificarRequest request, CancellationToken cancellationToken)
        {
            try 
            {
                var entity = await _command.Find<Geolocalizacion>(request.Id);

                entity.Longuitud = request.Longuitud;
                entity.Latitud = request.Latitud;
                entity.EstadoId = (int)Enums.Estado.Terminado;

                _command.Update(entity);
                await _command.SaveChangeAsync();

                return new ApiResponse<bool>(true);
            }
            catch (Exception ex)
            {
                request.AddNotification("Exception", $"Se ha lanzado una exception: {ex.Message}");
                return new ApiResponse<bool>(request.Notifications, HttpStatusCode.UnprocessableEntity);
            }

        }
    }
}
