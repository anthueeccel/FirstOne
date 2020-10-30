using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _appService;

        public UsuarioController(IUsuarioAppService appService,
                                 INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _appService = appService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ClaimsAuthorization("Usuario", "Add")]
        public async Task<IActionResult> Add([FromBody] UsuarioViewModel usuarioViewModel)
        {
            await _appService.AddAsync(usuarioViewModel);

            return CustomResponse();
        }

        [HttpGet]
        [Authorize(Roles = "Zezinho")]
        //[ClaimsAuthorization("Usuario", "GetAll")]
        public IEnumerable<UsuarioViewModel> GetAll()
        {
            return _appService.GetAll();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            var token = _appService.Login(loginViewModel.Email, loginViewModel.Senha);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }

        [HttpPost("claims")]
        [ClaimsAuthorization("Usuario", "Claims")]
        public async Task<IActionResult> AtualizarClaims([FromBody] UsuarioClaimViewModel usuarioPermissaoViewModel)
        {
            await _appService.AtualizarClaims(usuarioPermissaoViewModel);
            return Ok();
        }
    }
}
