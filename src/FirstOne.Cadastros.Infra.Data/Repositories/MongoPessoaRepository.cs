using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public class MongoPessoaRepository : IPessoaRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public MongoPessoaRepository()
        {
            _mongoDbContext = new MongoDbContext();
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _mongoDbContext.Pessoas.Find(p => true).ToList();
        }

        public void Add(Pessoa pessoa)
        {
            _mongoDbContext.Pessoas.InsertOne(pessoa);
        }

        public void Update(Pessoa pessoa)
        {
            _mongoDbContext.Pessoas.ReplaceOne(r => r.Id == pessoa.Id, pessoa);
        }

        public void Remove(Guid id)
        {
            _mongoDbContext.Pessoas.DeleteOne(r => r.Id == id);
        }

        public Pessoa GetById(Guid id)
        {
            return _mongoDbContext.Pessoas.Find(g => g.Id == id).FirstOrDefault();
        }
    }
}
