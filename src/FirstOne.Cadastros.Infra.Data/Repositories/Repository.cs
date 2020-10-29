using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public abstract class Repository : IRepository
    {
        protected readonly SqlServerContext _context;

        protected Repository(SqlServerContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
