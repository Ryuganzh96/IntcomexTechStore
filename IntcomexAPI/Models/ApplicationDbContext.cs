using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace IntcomexAPI.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accesskey> Accesskeys { get; set; }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<ArticulosCategoria> ArticulosCategoria { get; set; }

    public virtual DbSet<ArticulosSubcategoria> ArticulosSubcategorias { get; set; }

    public virtual DbSet<Atributo> Atributos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<Subcategoria> Subcategorias { get; set; }

    public virtual DbSet<Valoresatributo> Valoresatributos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=intcomex_db;user=intcomex_user;password=int_user_1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Accesskey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Activa).HasDefaultValueSql("'no'");
        });

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("PRIMARY");
        });

        modelBuilder.Entity<ArticulosCategoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.ArticulosCategoria).HasConstraintName("fk_articulo");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.ArticulosCategoria).HasConstraintName("fk_categoria");
        });

        modelBuilder.Entity<ArticulosSubcategoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.ArticulosSubcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("articulos_subcategorias_ibfk_1");

            entity.HasOne(d => d.IdSubcategoriaNavigation).WithMany(p => p.ArticulosSubcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("articulos_subcategorias_ibfk_2");
        });

        modelBuilder.Entity<Atributo>(entity =>
        {
            entity.HasKey(e => e.IdAtributo).HasName("PRIMARY");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");
        });

        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Subcategoria>(entity =>
        {
            entity.HasKey(e => e.IdSubcategoria).HasName("PRIMARY");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Subcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subcategorias_ibfk_1");
        });

        modelBuilder.Entity<Valoresatributo>(entity =>
        {
            entity.HasKey(e => e.IdValorAtributo).HasName("PRIMARY");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.Valoresatributos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("valoresatributos_ibfk_1");

            entity.HasOne(d => d.IdAtributoNavigation).WithMany(p => p.Valoresatributos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("valoresatributos_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
