using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public UsuarioRepository()
        {
            _mongoDbContext = new MongoDbContext();
        }

        public void Add(Usuario usuario)
        {
            _mongoDbContext.Usuarios.InsertOne(usuario);
        }
    }
}
