using FirstOne.Cadastros.Domain.Validations;
using FluentValidation.Results;
using MediatR;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class AddPessoaCommand : IRequest<bool>
    {
        public string Nome { get; }
        public ValidationResult ValidationResult { get; private set; }

        public AddPessoaCommand(string nome)
        {
            Nome = nome;
        }

        public bool IsValid()
        {
            ValidationResult = new AddPessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
