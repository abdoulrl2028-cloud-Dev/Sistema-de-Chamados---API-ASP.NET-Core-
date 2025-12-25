using Microsoft.EntityFrameworkCore;
using SistemaChamados.Api.Domain.Entities;

namespace SistemaChamados.Api.Infrastructure.Data
{
    public class SistemaChamadosDbContext : DbContext
    {
        public SistemaChamadosDbContext(DbContextOptions<SistemaChamadosDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chamado>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Solicitante)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Responsavel)
                    .HasMaxLength(150);

                entity.Property(e => e.Observacoes)
                    .HasMaxLength(2000);

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.Property(e => e.Prioridade)
                    .IsRequired();

                entity.Property(e => e.DataCriacao)
                    .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
