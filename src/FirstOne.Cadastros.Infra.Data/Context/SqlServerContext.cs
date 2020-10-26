using FirstOne.Cadastros.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstOne.Cadastros.Infra.Data.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioClaim> UsuarioClaim { get; set; }
    }
}
