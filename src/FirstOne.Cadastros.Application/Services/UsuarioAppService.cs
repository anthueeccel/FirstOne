using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Token;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class UsuarioAppService : AppService, IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;
        private readonly TokenSettings _tokenSettings;

        public UsuarioAppService(IUsuarioRepository repository,
                                IMapper mapper,
                                IMediatorHandler mediatorHandler,
                                IOptions<TokenSettings> tokenSettings) : base(mediatorHandler)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenSettings = tokenSettings.Value;
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

        public string Login(string email, string senha)
        {
            var usuario = _repository
                .Search(x => x.Email == email && x.Senha == senha)
                .FirstOrDefault();

            if (usuario == null)
                return null;

            return GenerateToken(usuario);
        }

        private string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var permissions = _repository.GetPermissoes(usuario.Id);

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(permission.EntidadeEnum.ToString(), permission.EndPoint));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                Expires = DateTime.Now.AddHours(_tokenSettings.Expires),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Subject = claimsIdentity
            });

            return tokenHandler.WriteToken(token);
        }

        public void AdicionarPermissao(UsuarioPermissaoViewModel usuarioPermissaoViewModel)
        {
            foreach (var permissao in usuarioPermissaoViewModel.Permissions)
            {
                _repository.AdicionarPermissao(usuarioPermissaoViewModel.UserId, permissao.Entidade, string.Join(",", permissao.EndPoints));

            }
        }

        public UsuarioPermissaoViewModel GetPermissoes(Guid usuarioId)
        {
            var permissoes = _repository.GetPermissoes(usuarioId);

            var usuarioPermissaoViewModel = new List<PermissionsViewModel>();

            foreach (var permissao in permissoes)
            {
                usuarioPermissaoViewModel.Add(new PermissionsViewModel()
                {
                    Entidade = permissao.EntidadeEnum,
                    EndPoints = permissao.EndPoint.Split(",".ToCharArray())
                });
            }
            return new UsuarioPermissaoViewModel() { UserId = usuarioId, Permissions = usuarioPermissaoViewModel };
        }
    }
}
