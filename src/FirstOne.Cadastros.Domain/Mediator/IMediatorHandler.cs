using FirstOne.Cadastros.Domain.Commands;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Mediator
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : AddPessoaCommand;
    }
}
