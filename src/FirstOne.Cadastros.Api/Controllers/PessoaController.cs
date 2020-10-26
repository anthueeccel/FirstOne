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
        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _appService.GetAll();
        }
           
        [AllowAnonymous]
        [HttpPost]
        //[ClaimsAuthorization("Pessoa", "Add")]
        public async Task<IActionResult> Adicionar([FromBody] PessoaViewModel pessoaViewModel)
        {
            await _appService.AddAsync(pessoaViewModel);

            return CustomResponse();
        }

        [HttpPut]
        [ClaimsAuthorization("Pessoa", "Update")]
        public async Task<IActionResult> Atualizar([FromBody] PessoaViewModel pessoa)
        {
            await _appService.UpdateAsync(pessoa);

            return CustomResponse();
        }

        [HttpDelete("{id}")]
        [ClaimsAuthorization("Pessoa", "Remove")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _appService.RemoveAsync(id);

            return CustomResponse();
        }

        [HttpGet("{id}")]
        public PessoaViewModel ObterPorId(Guid id)
        {
            return _appService.GetById(id);
        }
    }
}