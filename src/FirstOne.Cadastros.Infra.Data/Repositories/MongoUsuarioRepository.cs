using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using MongoDB.Driver;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public class MongoUsuarioRepository : IUsuarioRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public MongoUsuarioRepository()
        {
            _mongoDbContext = new MongoDbContext();
        }

        public void Add(Usuario usuario)
        {
            _mongoDbContext.Usuarios.InsertOne(usuario);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _mongoDbContext.Usuarios.Find(p => true).ToList();
        }
    }
}
