using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public abstract class AppService
    {
        protected readonly IMediatorHandler _mediatorHandler;

        protected AppService(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected async Task RaiseCommandValidationErrors(Command command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishDomainNotification(new DomainNotification(error.ErrorMessage));
            }
        }
    }
}
