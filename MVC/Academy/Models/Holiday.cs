using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

public partial class Holiday
{
    [Key]
    [Column("holiday_id")]
    public byte HolidayId { get; set; }

    [Column("holiday_name")]
    [StringLength(50)]
    public string? HolidayName { get; set; }

    [Column("day")]
    public byte? Day { get; set; }

    [Column("duration")]
    public byte Duration { get; set; }

    [Column("month")]
    public byte? Month { get; set; }

    [InverseProperty("HolidayNavigation")]
    public virtual ICollection<DaysOff> DaysOffs { get; set; } = new List<DaysOff>();
}
