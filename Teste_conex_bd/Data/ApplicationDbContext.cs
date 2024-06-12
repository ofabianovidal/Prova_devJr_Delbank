using Microsoft.EntityFrameworkCore;
using Teste_conex_bd.Models; 
namespace Teste_conex_bd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Dvd> Dvds { get; set; }
        public DbSet<Diretor> Diretores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dvd>()
                .HasOne(d => d.Diretor)
                .WithMany(p => p.Dvds)
                .HasForeignKey(d => d.DiretorId);

            // Configurar campos de data como não nulos
            modelBuilder.Entity<Dvd>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Diretor>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }

}
