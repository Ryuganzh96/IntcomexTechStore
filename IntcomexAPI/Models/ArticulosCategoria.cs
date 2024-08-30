using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("articulos_categoria")]
[Index("IdArticulo", Name = "fk_articulo")]
[Index("IdCategoria", Name = "fk_categoria")]
public partial class ArticulosCategoria
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("Id_articulo", TypeName = "int(11)")]
    public int IdArticulo { get; set; }

    [Column("Id_categoria", TypeName = "int(11)")]
    public int IdCategoria { get; set; }

    [ForeignKey("IdArticulo")]
    [InverseProperty("ArticulosCategoria")]
    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    [ForeignKey("IdCategoria")]
    [InverseProperty("ArticulosCategoria")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
