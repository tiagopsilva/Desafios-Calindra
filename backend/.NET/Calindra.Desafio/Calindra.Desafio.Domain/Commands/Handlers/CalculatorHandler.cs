using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Results;
using Calindra.Desafio.Domain.Services;
using System.Threading.Tasks;

namespace Calindra.Desafio.Domain.Commands.Handlers
{
    public class CalculatorHandler : ICommandHandler<Addresses>
    {
        private readonly DistanceCalculatorService _service;

        public CalculatorHandler(DistanceCalculatorService service)
        {
            _service = service;
        }

        public async Task<MethodResult> Execute(Addresses command)
        {
            return await _service.CalculateDistancesFromAddressess(command);
        }
    }
}
