using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("parametros")]
public partial class Parametro
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [StringLength(255)]
    public string? Valor { get; set; }
}
