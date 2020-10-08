using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost]
        public IActionResult Add([FromBody] PessoaViewModel pessoa)
        {
            var result = _appService.Add(pessoa);

            if (result.IsValid)
                return Ok();

            return UnprocessableEntity(new
            {
                errors = result.Errors.Select(e => e.ErrorMessage)
            });
        }
    }
}