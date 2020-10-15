using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _repository;
        private readonly IMediatorHandler _mediatorHandler;

        public PessoaAppService(IPessoaRepository repository,
                                IMapper mapper,
                                IMediatorHandler mediatorHandler)
        {
            _repository = repository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task AddAsync(PessoaViewModel pessoaViewModel)
        {
            var command = new AddPessoaCommand(pessoaViewModel.Nome);

            if (!command.IsValid())
            {
                foreach (var error in command.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishDomainNotification(new DomainNotification(error.ErrorMessage));
                }
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }

        public IEnumerable<PessoaViewModel> GetAll()
        {
            return _mapper.Map<List<PessoaViewModel>>(_repository.GetAll());
        }

        public async Task UpdateAsync(PessoaViewModel pessoa)
        {
            var command = new UpdatePessoaCommand(pessoa.Id, pessoa.Nome);

            if (!command.IsValid())
            {
                foreach (var error in command.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishDomainNotification(new DomainNotification(error.ErrorMessage));
                }
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }
    }
}
