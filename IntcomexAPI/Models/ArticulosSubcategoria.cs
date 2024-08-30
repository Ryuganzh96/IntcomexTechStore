using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("articulos_subcategorias")]
[Index("IdArticulo", Name = "articulos_subcategorias_ibfk_1")]
[Index("IdSubcategoria", Name = "articulos_subcategorias_ibfk_2")]
public partial class ArticulosSubcategoria
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("Id_Articulo", TypeName = "int(11)")]
    public int IdArticulo { get; set; }

    [Column("Id_Subcategoria", TypeName = "int(11)")]
    public int IdSubcategoria { get; set; }

    [ForeignKey("IdArticulo")]
    [InverseProperty("ArticulosSubcategoria")]
    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    [ForeignKey("IdSubcategoria")]
    [InverseProperty("ArticulosSubcategoria")]
    public virtual Subcategoria IdSubcategoriaNavigation { get; set; } = null!;
}
