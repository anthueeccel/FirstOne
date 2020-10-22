using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class PessoaAppService : AppService, IPessoaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _repository;

        public PessoaAppService(IPessoaRepository repository,
                                IMapper mapper,
                                IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(PessoaViewModel pessoaViewModel)
        {
            var command = new AddPessoaCommand(pessoaViewModel.Nome);

            if (!command.IsValid())
            {
                await RaiseCommandValidationErrors(command);
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }

        public async Task UpdateAsync(PessoaViewModel pessoa)
        {
            var command = new UpdatePessoaCommand(pessoa.Id, pessoa.Nome);

            if (!command.IsValid())
            {
                await RaiseCommandValidationErrors(command);
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }

        public async Task RemoveAsync(Guid id)
        {
            var command = new RemovePessoaCommand(id);

            if (!command.IsValid())
            {
                await RaiseCommandValidationErrors(command);
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }

        public IEnumerable<PessoaViewModel> GetAll()
        {
            return _mapper.Map<List<PessoaViewModel>>(_repository.GetAll());
        }

        public PessoaViewModel GetById(Guid id)
        {
            return _mapper.Map<PessoaViewModel>(_repository.GetById(id));
        }      
    }
}
