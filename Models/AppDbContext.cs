using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SIQuim.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Materiale> Materiales { get; set; }

    public DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<ToolCrib> ToolCribs { get; set; }

    public virtual DbSet<Ubicacione> Ubicaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=10.195.10.166,1433;Database=TGRMINVENT;User Id=manu;Password=2022.Tgram2;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Empleado
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC27180AE6A1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Area).HasMaxLength(200);
            entity.Property(e => e.Categoria).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        // Materiales
        modelBuilder.Entity<Materiale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC276457C408");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Kanban).HasMaxLength(100);
            entity.Property(e => e.PartNumber).HasMaxLength(100);
            entity.Property(e => e.Uom)
                .HasMaxLength(50)
                .HasColumnName("UOM");
            entity.Property(e => e.Vendor).HasMaxLength(150);
        });

        // Pedido
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(p => p.Id).HasName("PK__Pedido__3214EC27");

            entity.ToTable("Pedido");

            entity.Property(p => p.Id).HasColumnName("ID");
            entity.Property(p => p.FechaHora)
                  .HasDefaultValueSql("(getdate())")
                  .HasColumnType("datetime");

            // Relación con empleados
            entity.HasOne(p => p.ResponsableEntrega)
                  .WithMany()
                  .HasForeignKey(p => p.ResponsableEntregaId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Pedido_Entrega");

            entity.HasOne(p => p.ResponsableRecibe)
                  .WithMany()
                  .HasForeignKey(p => p.ResponsableRecibeId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Pedido_Recibe");
        });

        // Registro
        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC27595AEC86");

            entity.ToTable("Registro");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            // Relación con Material
            entity.HasOne(d => d.Material).WithMany(p => p.Registros)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Material");

            // Relación con Empleado (Entrega)
            entity.HasOne(d => d.ResponsableEntregaNavigation).WithMany(p => p.RegistroResponsableEntregaNavigations)
                .HasForeignKey(d => d.ResponsableEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Entrega");

            // Relación con Empleado (Reciba)
            entity.HasOne(d => d.ResponsableRecibaNavigation).WithMany(p => p.RegistroResponsableRecibaNavigations)
                .HasForeignKey(d => d.ResponsableReciba)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Reciba");

            // Relación con Pedido
            entity.HasOne(d => d.Pedido)
                .WithMany(p => p.Registros)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Registro_Pedido");
        });

        // Stock
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock__2C83A9E27F5DE0E3");

            entity.ToTable("Stock");

            entity.Property(e => e.StockId).HasColumnName("StockID");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.UbicacionId).HasColumnName("UbicacionID");

            entity.HasOne(d => d.Material).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Materiales");

            entity.HasOne(d => d.Ubicacion).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.UbicacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Ubicaciones");
        });

        // ToolCrib
        modelBuilder.Entity<ToolCrib>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToolCrib__3214EC2700DCEC7B");

            entity.ToTable("ToolCrib");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Hora).HasDefaultValueSql("(CONVERT([time],getdate()))");
        });

        // Ubicaciones
        modelBuilder.Entity<Ubicacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ubicacio__10375DF57DE70632");

            entity.HasIndex(e => e.Codigo, "UQ__Ubicacio__06370DAC038EC742").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
