using Calindra.Desafio.Domain.Results;
using System.Threading.Tasks;

namespace Calindra.Desafio.Domain.Commands.Handlers
{
    public interface ICommandHandler<ICommand>
    {
        Task<MethodResult> Execute(ICommand command);
    }
}
