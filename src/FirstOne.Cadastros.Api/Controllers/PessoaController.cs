using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaAppService _appService;

        public PessoaController(IPessoaAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public IEnumerable<PessoaViewModel> GetAll()
        {
            return _appService.GetAll();
        }
    }
}