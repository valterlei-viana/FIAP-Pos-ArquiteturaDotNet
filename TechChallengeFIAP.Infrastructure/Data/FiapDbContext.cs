using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Infrastructure.Data
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contato>().HasKey(m => m.Id);
            builder.Entity<Telefone>().HasKey(m => m.Id);
        }
    }
}
