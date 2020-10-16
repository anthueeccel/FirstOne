using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;
using System;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class RemovePessoaCommandValidation : AbstractValidator<RemovePessoaCommand>
    {
        public RemovePessoaCommandValidation()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("Favor informar o Id.");
        }
    }
}
