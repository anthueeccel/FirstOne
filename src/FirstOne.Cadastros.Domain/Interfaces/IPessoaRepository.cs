﻿using FirstOne.Cadastros.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IPessoaRepository : IRepository
    {
        IEnumerable<Pessoa> GetAll();
        void Add(Pessoa pessoa);
        void Update(Pessoa pessoa);
        void Remove(Guid id);
        Pessoa GetById(Guid id);
    }
}
