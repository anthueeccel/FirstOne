using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Messaging;
using FluentValidation;
using System;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class UpdatePessoaCommandValidation : AbstractValidator<UpdatePessoaCommand>
    {
        public UpdatePessoaCommandValidation()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage(string.Format(ValidationMessages.RequiredField, "Id"));
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Nome"));
        }
    }
}
