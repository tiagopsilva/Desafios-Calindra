using Calindra.Desafio.Domain.Commands.Handlers;
using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Calindra.Desafio.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GeolocationController : ControllerBase
    {
        private readonly ICommandHandler<Addresses> _handler;

        public GeolocationController(ICommandHandler<Addresses> handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Addresses addresses)
        {
            var result = await _handler.Execute(addresses);
            return Process(result);
        }

        protected IActionResult Process(MethodResult result)
        {
            if (result.Success)
                return Ok(result.Data);

            if (result.ErrorCode != null)
                return base.StatusCode(result.ErrorCode.Value, result);

            return StatusCode((int)HttpStatusCode.InternalServerError, result);
        }
    }
}
