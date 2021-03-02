using API.Presenters;
using Application.UseCase.UseCaseExample;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("/v{v:apiVersion}/[controller]")]
    public class DefaultController : ControllerBase
    {
        private readonly IApiRestPresenter _presenter;
        private readonly IMediator _mediator;

        public DefaultController(IApiRestPresenter presenter, IMediator mediator)
        {
            _presenter = presenter;
            _mediator = mediator;
        }
        /// <summary>
        /// Example API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return _presenter.GetActionResult(await _mediator.Send(new UseCaseExampleRequest { Id = id }));
        }
    }
}