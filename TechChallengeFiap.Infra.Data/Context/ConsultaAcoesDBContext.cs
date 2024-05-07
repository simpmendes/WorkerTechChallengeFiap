using TechChallengeFiap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TechChallengeFiap.Infra.Data.Context
{
    public partial class ConsultaAcoesDBContext : DbContext
    {
     
        public virtual DbSet<ConsultaAcoes>? ConsultaAcoes { get; set; }
        public virtual DbSet<Usuario>? Usuarios { get; set; }
        public virtual DbSet<PedidoAcao>? PedidoAcoes { get; set; }
        public virtual DbSet<Acao>? Acoes { get; set; }

        public ConsultaAcoesDBContext():base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TechChallengeDB");

        public ConsultaAcoesDBContext(DbContextOptions<ConsultaAcoesDBContext> options): base(options)
        {
            
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade ConsultaAcoes
            modelBuilder.Entity<ConsultaAcoes>(entity =>
            {
                entity.Property(e => e.Symbol)
                      .IsRequired();

                entity.Property(e => e.DataConsulta)
                      .IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.ConsultasAcoes)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade); // Exemplo de deleção em cascata
            });

            // Configuração da entidade Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.NomeUsuario)
                      .HasColumnType("VARCHAR(50)")
                      .IsRequired();

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome).HasColumnType("VARCHAR(100)");

                entity.Property(e => e.Senha)
                      .HasColumnType("VARCHAR(50)")
                      .IsRequired();

                entity.Property(e => e.Permissao)
                      .IsRequired();

                entity.HasMany(u => u.ConsultasAcoes)
                      .WithOne(ca => ca.Usuario)
                      .HasForeignKey(ca => ca.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade); // Exemplo de deleção em cascata
            });
            modelBuilder.Entity<PedidoAcao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Id_Acao).IsRequired();

                entity.Property(e => e.Id_Usuario)
                      .IsRequired();
                entity.Property(e => e.dtPedido)
                      .IsRequired();
                entity.Property(e => e.qtPedido)
                      .IsRequired();

                entity.HasOne(e => e.Acao)
                      .WithMany(u => u.PedidosAcao)
                      .HasForeignKey(e => e.Id_Acao)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.usuario)
                     .WithMany(u => u.PedidosAcoes)
                     .HasForeignKey(e => e.Id_Usuario)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Acao>(entity =>
            {
                entity.Property(e => e.Id)
                      .IsRequired();

                entity.HasKey(e => new { e.Id });


            });

        }

    }
}
