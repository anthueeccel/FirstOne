using FirstOne.Cadastros.Domain.Commands;
using MediatR;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommand<T>(T command) where T : AddPessoaCommand
        {
            await _mediator.Send(command);
        }
    }
}
