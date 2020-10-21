using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class Pessoa : EntidadeBase
    {
        public string Nome { get; private set; }


        public Pessoa(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        protected Pessoa() { }
    }
}
