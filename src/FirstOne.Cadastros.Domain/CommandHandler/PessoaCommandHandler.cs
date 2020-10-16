using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class PessoaCommandHandler : IRequestHandler<AddPessoaCommand, bool>,
                                        IRequestHandler<UpdatePessoaCommand, bool>,
                                        IRequestHandler<RemovePessoaCommand, bool>
    {
        private readonly IPessoaRepository _repository;
        private readonly IMediatorHandler _mediatorHandler;

        public PessoaCommandHandler(IPessoaRepository repository, IMediatorHandler mediatorHandler)
        {
            _repository = repository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach(var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishDomainNotification( new DomainNotification(error.ErrorMessage));
                }
                return false;
            }

            var pessoa = new Pessoa(Guid.NewGuid(), request.Nome);
            _repository.Add(pessoa);

            return true;
        }

        public async Task<bool> Handle(UpdatePessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishDomainNotification(new DomainNotification(error.ErrorMessage));
                }
                return false;
            }

            var pessoa = new Pessoa(request.Id, request.Nome);
            _repository.Update(pessoa);

            return true;
        }

        public async Task<bool> Handle(RemovePessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishDomainNotification(new DomainNotification(error.ErrorMessage));
                }
                return false;
            }

            _repository.Remove(request.Id);

            return true;
        }
    }
}
