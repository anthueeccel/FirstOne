using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Messaging;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations.Usuario
{
    public class AddUsuarioCommandValidation : AbstractValidator<AddUsuarioCommand>
    {
        public AddUsuarioCommandValidation()
        {
            RuleFor(p => p.Email)
                .EmailAddress().WithMessage(string.Format(ValidationMessages.RequiredField, "Email"));
            RuleFor(p => p.Senha)
                .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Senha"));
        }
    }
}
