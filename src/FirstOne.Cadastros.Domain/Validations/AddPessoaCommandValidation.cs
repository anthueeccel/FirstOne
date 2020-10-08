using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class AddPessoaCommandValidation : AbstractValidator<AddPessoaCommand>
    {
        public AddPessoaCommandValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Favor informar um Nome.");
        }
    }
}
