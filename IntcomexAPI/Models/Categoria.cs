using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("categorias")]
public partial class Categoria
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int IdCategoria { get; set; }

    [StringLength(100)]
    public string NombreCategoria { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<ArticulosCategoria> ArticulosCategoria { get; set; } = new List<ArticulosCategoria>();

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Subcategoria> Subcategoria { get; set; } = new List<Subcategoria>();
}
