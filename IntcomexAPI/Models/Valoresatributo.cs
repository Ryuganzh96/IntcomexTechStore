using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("valoresatributos")]
[Index("IdArticulo", Name = "IdArticulo")]
[Index("IdAtributo", Name = "IdAtributo")]
public partial class Valoresatributo
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int IdValorAtributo { get; set; }

    [Column(TypeName = "int(11)")]
    public int IdArticulo { get; set; }

    [Column(TypeName = "int(11)")]
    public int IdAtributo { get; set; }

    [StringLength(255)]
    public string Valor { get; set; } = null!;

    [ForeignKey("IdArticulo")]
    [InverseProperty("Valoresatributos")]
    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    [ForeignKey("IdAtributo")]
    [InverseProperty("Valoresatributos")]
    public virtual Atributo IdAtributoNavigation { get; set; } = null!;
}
