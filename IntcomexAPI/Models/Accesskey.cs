using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IntcomexAPI.Models;

[Table("accesskeys")]
public partial class Accesskey
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Llave { get; set; } = null!;

    [Column(TypeName = "enum('si','no')")]
    public string Activa { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaExpiracion { get; set; }
}
