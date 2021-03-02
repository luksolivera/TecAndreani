using API.Presenters;
using Application.UseCase.GeolocalizarOperation;
using Application.UseCase.V1.GeocodificarOperation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.V1
{
    [ApiController]
    public class GeolocalizarController : ControllerBase
    {
        private readonly IApiRestPresenter _presenter;
        private readonly IMediator _mediator;

        public GeolocalizarController(IApiRestPresenter presenter, IMediator mediator)
        {
            _presenter = presenter;
            _mediator = mediator;
        }
        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("geolocalizar")]
        [HttpPost()]
        public async Task<IActionResult> Geolocalizar([FromBody] GeolocalizarRequest request)
        {
            return _presenter.CustomStatusCodeResult(await _mediator.Send(request), System.Net.HttpStatusCode.Accepted);
        }

        [Route("geocodificar")]
        [HttpGet()]
        public async Task<IActionResult> Geocodificar([FromQuery] string id)
        {
            return _presenter.GetActionResult(await _mediator.Send(new GeocodificarRequest { Id = id}));
        }
    }
}