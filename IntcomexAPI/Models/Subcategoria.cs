using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("subcategorias")]
[Index("IdCategoria", Name = "IdCategoria")]
public partial class Subcategoria
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int IdSubcategoria { get; set; }

    [StringLength(100)]
    public string NombreSubcategoria { get; set; } = null!;

    [Column(TypeName = "int(11)")]
    public int IdCategoria { get; set; }

    [InverseProperty("IdSubcategoriaNavigation")]
    public virtual ICollection<ArticulosSubcategoria> ArticulosSubcategoria { get; set; } = new List<ArticulosSubcategoria>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Subcategoria")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
