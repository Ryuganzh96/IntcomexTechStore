using Microsoft.EntityFrameworkCore;
using IntcomexAPI.Models;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Subcategoria> Subcategorias { get; set; }
    public DbSet<Atributo> Atributos { get; set; }
    public DbSet<Parametro> Parametros { get; set; }
    public DbSet<Accesskey> AccessKeys { get; set; }
    public DbSet<ArticulosSubcategoria> ArticulosSubcategorias { get; set; }
    public DbSet<ArticulosCategoria> ArticulosCategorias { get; set; }
}
