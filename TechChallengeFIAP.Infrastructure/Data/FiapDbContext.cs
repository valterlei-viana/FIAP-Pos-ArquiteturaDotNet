using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechChallengeFIAP.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TechChallengeFIAP.Infrastructure.Data
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite(b => b.MigrationsAssembly("TechChallengeFIAP.API"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contato>().HasKey(m => m.Id);
            builder.Entity<Telefone>().HasKey(m => m.Id);

            base.OnModelCreating(builder);
        }
    }
}
