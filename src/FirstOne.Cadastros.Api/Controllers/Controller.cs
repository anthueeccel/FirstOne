using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class Controller : ControllerBase
    {
        private readonly DomainNotificationHandler _domainNotificationHandler;

        protected Controller(INotificationHandler<DomainNotification> notificationHandler)
        {
            _domainNotificationHandler = (DomainNotificationHandler)notificationHandler; ;
        }

        protected IActionResult CustomResponse()
        {
            if (_domainNotificationHandler.HasNotification())
            {
                return UnprocessableEntity(new
                {
                    errors = _domainNotificationHandler.GetNotifications()
                });
            }

            return Ok();
        }
    }
}
