using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

[PrimaryKey("Date", "Holiday")]
[Table("DaysOFF")]
public partial class DaysOff
{
    [Key]
    [Column("date")]
    public DateOnly Date { get; set; }

    [Key]
    [Column("holiday")]
    public byte Holiday { get; set; }

    [ForeignKey("Holiday")]
    [InverseProperty("DaysOffs")]
    public virtual Holiday HolidayNavigation { get; set; } = null!;
}
