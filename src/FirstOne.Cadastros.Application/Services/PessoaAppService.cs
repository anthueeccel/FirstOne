using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Interfaces;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _repository;

        public PessoaAppService(IPessoaRepository repository,
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public ValidationResult AddAsync(PessoaViewModel pessoa)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PessoaViewModel> GetAll()
        {
            return _mapper.Map<List<PessoaViewModel>>(_repository.GetAll());
        }
    }
}
