using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("articulos")]
public partial class Articulo
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int IdArticulo { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    public string Sku { get; set; } = null!;

    [Column("MPN")]
    [StringLength(50)]
    public string Mpn { get; set; } = null!;

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdArticuloNavigation")]
    public virtual ICollection<ArticulosCategoria> ArticulosCategoria { get; set; } = new List<ArticulosCategoria>();

    [InverseProperty("IdArticuloNavigation")]
    public virtual ICollection<ArticulosSubcategoria> ArticulosSubcategoria { get; set; } = new List<ArticulosSubcategoria>();

    [InverseProperty("IdArticuloNavigation")]
    public virtual ICollection<Valoresatributo> Valoresatributos { get; set; } = new List<Valoresatributo>();
}
