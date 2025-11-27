
using Microsoft.EntityFrameworkCore;

namespace Creditos.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Asesor> Asesor { get; set; }
        public DbSet<NotificacionAsesor> NotificacionAsesor { get; set; }
        public DbSet<RespuestaCreditoFinanciera> RespuestaCreditoFinanciera { get; set; }
        public DbSet<SolicitudCredito> SolicitudCredito { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RespuestaCreditoFinanciera>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta);
                entity.HasOne(d => d.SolicitudCredito)
                .WithMany(p => p.RespuestasCreditos)
                .HasForeignKey(f => f.IdSolicitud)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Monto)
                .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<NotificacionAsesor>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion);

                entity.HasOne(n => n.Solicitud)
                .WithMany()
                .HasForeignKey(n => n.IdSolicitud)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Asesor)
                .WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdAsesor)
                .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<SolicitudCredito>(entity =>
            {
                entity.HasKey(e => e.IdSolicitud);

                entity.HasOne(s => s.Asesor)
                .WithMany(p => p.Solicitudes)
                .HasForeignKey(s => s.IdAsesor)
                .OnDelete(DeleteBehavior.Restrict);
            });
            
        }
    }
}
