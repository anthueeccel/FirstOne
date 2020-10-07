using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class Pessoa
    {
        public Guid Id { get; }
        public string Nome { get; }

        public Pessoa(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
