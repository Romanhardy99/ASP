using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

public partial class Direction
{
    [Key]
    [Column("direction_id")]
    [Display(Name = "ID")]
    public byte DirectionId { get; set; }

    [Column("direction_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Required(ErrorMessage = "Укажите название направления")]
    [Display(Name = "Направление")]
    public string DirectionName { get; set; } = null!;

    [InverseProperty("DirectionNavigation")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [ForeignKey("Direction")]
    [InverseProperty("Directions")]
    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}
