using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
using FirstOne.Cadastros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Infra.Data.Context
{
    public class SqlServerContext : DbContext, IUnitOfWork
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioClaim> UsuarioClaim { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasData(
                new Pessoa(Guid.Parse("cf56b7e5-390f-44a4-b44b-9517f7e619ba"), "Tester")
                );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario(Guid.Parse("fc127929-ef16-4287-96ce-c8e2c8a051c2"), "tester@tester.com", "12345", Guid.Parse("cf56b7e5-390f-44a4-b44b-9517f7e619ba"))
                );

            modelBuilder.Entity<UsuarioClaim>().HasData(
                new UsuarioClaim(Guid.Parse("6226d62f-a747-4ba2-a137-1a4e9c44a613"), Guid.Parse("fc127929-ef16-4287-96ce-c8e2c8a051c2"), EntidadeEnum.Pessoa, "Add, Update, Delete"),
                new UsuarioClaim(Guid.Parse("7a1a80af-f3db-4787-b94e-9afd7cd368fe"), Guid.Parse("fc127929-ef16-4287-96ce-c8e2c8a051c2"), EntidadeEnum.Usuario, "Add, Update, Delete, Claims")
                );
        }
    }
}
