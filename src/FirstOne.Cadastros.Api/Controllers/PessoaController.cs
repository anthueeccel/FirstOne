using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaAppService _appService;
        private readonly DomainNotificationHandler _domainNotificationHandler;

        public PessoaController(IPessoaAppService appService, INotificationHandler<DomainNotification> notificationHandler)
        {
            _appService = appService;
            _domainNotificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        [HttpGet]
        public IEnumerable<PessoaViewModel> GetAll()
        {
            return _appService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PessoaViewModel pessoaViewModel)
        {
            await _appService.AddAsync(pessoaViewModel);

            if (_domainNotificationHandler.HasNotification())
            {
                return UnprocessableEntity(new
                {
                    errors = _domainNotificationHandler.GetNotifications()
                });
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PessoaViewModel pessoa)
        {
            await _appService.UpdateAsync(pessoa);

            if (_domainNotificationHandler.HasNotification())
            {
                return UnprocessableEntity(new
                {
                    errors = _domainNotificationHandler.GetNotifications()
                });
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _appService.RemoveAsync(id);

            if (_domainNotificationHandler.HasNotification())
            {
                return UnprocessableEntity(new
                {
                    errors = _domainNotificationHandler.GetNotifications()
                });
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public PessoaViewModel GetById(Guid id)
        {
            return _appService.GetById(id);
        }
    }
}