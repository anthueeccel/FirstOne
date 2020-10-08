using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

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

        public ValidationResult Add(PessoaViewModel pessoaViewModel)
        {
            var command = new AddPessoaCommand(pessoaViewModel.Nome);
            if (!command.IsValid())
                return command.ValidationResult;


            var pessoa = new Pessoa(Guid.NewGuid(), command.Nome);
            _repository.Add(pessoa);

            _mediatorHandler.SendCommand(command);

            return command.ValidationResult;
        }

        public IEnumerable<PessoaViewModel> GetAll()
        {
            return _mapper.Map<List<PessoaViewModel>>(_repository.GetAll());
        }
    }
}
