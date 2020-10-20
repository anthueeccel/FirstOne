using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Messaging;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class AddPessoaCommandValidation : AbstractValidator<AddPessoaCommand>
    {
        public AddPessoaCommandValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Nome"));
        }
    }
}
