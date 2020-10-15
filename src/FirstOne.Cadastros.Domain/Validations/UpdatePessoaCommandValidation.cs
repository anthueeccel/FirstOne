using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;
using System;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class UpdatePessoaCommandValidation : AbstractValidator<UpdatePessoaCommand>
    {
        public UpdatePessoaCommandValidation()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("Favor informar o Id.");
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Favor informar o Nome.");
        }
    }
}
