using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto1.Models;

public partial class ProductoContext : DbContext
{
    public ProductoContext()
    {
    }

    public ProductoContext(DbContextOptions<ProductoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MEDAPRCSGFSP672\\SQLEXPRESS;Initial Catalog=Producto;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Documento).HasName("PK__Clientes__AF73706CE3D5A353");

            entity.Property(e => e.Apellido)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compras__3214EC0726793674");

            entity.Property(e => e.FechaCompra)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Compra");
            entity.Property(e => e.IdCliente).HasColumnName("Id_cliente");
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Compras__Id_clie__4D94879B");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Compras__Id_prod__4E88ABD4");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC078ACAC9BF");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
