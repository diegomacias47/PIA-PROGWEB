using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PROG.Models.dbModels
{
    public partial class P141121Context : DbContext
    {
        public P141121Context()
        {
        }

        public P141121Context(DbContextOptions<P141121Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Arma> Armas { get; set; }
        public virtual DbSet<Carrito> Carritos { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=P141121;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Arma>(entity =>
            {
                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Armas)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Llave_Arma_Estado");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Armas)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Llave_Arma_Tipo");
            });

            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.Property(e => e.IdDetalle).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdArmaNavigation)
                    .WithMany(p => p.Carritos)
                    .HasForeignKey(d => d.IdArma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idArmaLlave");

                entity.HasOne(d => d.IdDetalleNavigation)
                    .WithOne(p => p.Carrito)
                    .HasForeignKey<Carrito>(d => d.IdDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idUsuarioLlave");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.Property(e => e.IdFactura).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithOne(p => p.Factura)
                    .HasForeignKey<Factura>(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioKey");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_Usuarios");

                entity.HasOne(d => d.RolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Rol)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
