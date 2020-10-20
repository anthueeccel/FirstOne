using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Add([FromBody] UsuarioViewModel usuarioViewModel)
        {
            await _appService.AddAsync(usuarioViewModel);

            return CustomResponse();
        }
    }
}
