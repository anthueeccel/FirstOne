using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Messaging;
using FluentValidation;
using System;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class RemovePessoaCommandValidation : AbstractValidator<RemovePessoaCommand>
    {
        public RemovePessoaCommandValidation()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage(string.Format(ValidationMessages.RequiredField, "Id"));
        }
    }
}
