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

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<ToolCrib> ToolCribs { get; set; }

    public virtual DbSet<Ubicacione> Ubicaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=10.195.10.166,1433;Database=TGRMINVENT;User Id=manu;Password=2022.Tgram2;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC27180AE6A1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Area).HasMaxLength(200);
            entity.Property(e => e.Categoria).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

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

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC27595AEC86");

            entity.ToTable("Registro");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.Material).WithMany(p => p.Registros)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Material");

            entity.HasOne(d => d.ResponsableEntregaNavigation).WithMany(p => p.RegistroResponsableEntregaNavigations)
                .HasForeignKey(d => d.ResponsableEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Entrega");

            entity.HasOne(d => d.ResponsableRecibaNavigation).WithMany(p => p.RegistroResponsableRecibaNavigations)
                .HasForeignKey(d => d.ResponsableReciba)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Reciba");
        });

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

        modelBuilder.Entity<ToolCrib>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToolCrib__3214EC2700DCEC7B");

            entity.ToTable("ToolCrib");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Hora).HasDefaultValueSql("(CONVERT([time],getdate()))");
        });

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
