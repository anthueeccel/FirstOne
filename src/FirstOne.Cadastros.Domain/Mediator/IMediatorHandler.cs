using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Messaging;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Mediator
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : AddPessoaCommand;
        Task PublishDomainNotification<T>(T notification) where T : DomainNotification;
    }
}
