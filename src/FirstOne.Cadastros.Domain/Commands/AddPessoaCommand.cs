using FirstOne.Cadastros.Domain.Validations;
using FluentValidation.Results;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class AddPessoaCommand : Command
    {
        public string Nome { get; }

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
