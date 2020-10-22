using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Authorize]   
    public class PessoaController : Controller
    {
        private readonly IPessoaAppService _appService;

        public PessoaController(IPessoaAppService appService,
                                INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _appService = appService;
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

            return CustomResponse();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PessoaViewModel pessoa)
        {
            await _appService.UpdateAsync(pessoa);

            return CustomResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _appService.RemoveAsync(id);

            return CustomResponse();
        }

        [HttpGet("{id}")]
        public PessoaViewModel GetById(Guid id)
        {
            return _appService.GetById(id);
        }
    }
}