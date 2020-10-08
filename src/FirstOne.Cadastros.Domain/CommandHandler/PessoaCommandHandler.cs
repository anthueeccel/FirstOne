using FirstOne.Cadastros.Domain.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class PessoaCommandHandler : IRequestHandler<AddPessoaCommand, bool>
    {
        public Task<bool> Handle(AddPessoaCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
