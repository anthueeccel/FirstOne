using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class UsuarioCommandHandler : IRequestHandler<AddUsuarioCommand, Unit>
    {
        private IUsuarioRepository _repository;

        public UsuarioCommandHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
        {
            var pessoa = new Usuario(Guid.NewGuid(), request.Email, request.Senha, request.PessoaId);
            _repository.Add(pessoa);

            return Unit.Value;
        }
    }
}
