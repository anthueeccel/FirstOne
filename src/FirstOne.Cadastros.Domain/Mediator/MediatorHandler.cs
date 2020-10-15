using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Messaging;
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

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

        public async Task PublishDomainNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
