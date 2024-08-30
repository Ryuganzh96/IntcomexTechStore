using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("atributos")]
public partial class Atributo
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int IdAtributo { get; set; }

    [StringLength(100)]
    public string NombreAtributo { get; set; } = null!;

    [InverseProperty("IdAtributoNavigation")]
    public virtual ICollection<Valoresatributo> Valoresatributos { get; set; } = new List<Valoresatributo>();
}
