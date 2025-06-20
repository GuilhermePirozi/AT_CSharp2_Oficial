using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;

namespace AT_CSharp2_Oficial.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PaisDestino> Paises { get; set; }
        public DbSet<CidadeDestino> Cidades { get; set; }
        public DbSet<PacoteTuristico> Pacotes { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CidadeDestino>()
                .HasOne(c => c.PaisDestino)
                .WithMany(p => p.Cidades)
                .HasForeignKey(c => c.PaisDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Destino>()
                .HasOne(d => d.CidadeDestino)
                .WithMany()
                .HasForeignKey(d => d.CidadeDestinoId);

            modelBuilder.Entity<Destino>()
                .HasOne(d => d.PacoteTuristico)
                .WithMany(p => p.Destinos)
                .HasForeignKey(d => d.PacoteTuristicoId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.PacoteTuristico)
                .WithMany(p => p.Reservas)
                .HasForeignKey(r => r.PacoteTuristicoId);

            modelBuilder.Entity<Reserva>()
                .HasIndex(r => new { r.ClienteId, r.PacoteTuristicoId, r.DataReserva })
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
