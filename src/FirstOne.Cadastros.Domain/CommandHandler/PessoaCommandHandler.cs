using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class PessoaCommandHandler : IRequestHandler<AddPessoaCommand, Unit>,
                                        IRequestHandler<UpdatePessoaCommand, Unit>,
                                        IRequestHandler<RemovePessoaCommand, Unit>
    {
        private readonly IPessoaRepository _repository;

        public PessoaCommandHandler(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddPessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(Guid.NewGuid(), request.Nome);
            _repository.Add(pessoa);

            await _repository.UnitOfWork.Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdatePessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(request.Id, request.Nome);
            _repository.Update(pessoa);

            await _repository.UnitOfWork.Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(RemovePessoaCommand request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);

            await _repository.UnitOfWork.Commit();

            return Unit.Value;
        }
    }
}
