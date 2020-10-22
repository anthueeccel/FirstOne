using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class UsuarioAppService : AppService, IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;

        public UsuarioAppService(IUsuarioRepository repository,
                                IMapper mapper,
                                IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(UsuarioViewModel usuario)
        {
            var command = new AddUsuarioCommand(usuario.Email, usuario.Senha, usuario.PessoaId);

            if (!command.IsValid())
            {
                await RaiseCommandValidationErrors(command);
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }

        public IEnumerable<UsuarioViewModel> GetAll()
        {
            return _mapper.Map<List<UsuarioViewModel>>(_repository.GetAll());
        }
    }
}
