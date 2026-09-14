using System;
using System.Collections.Generic;

namespace MyAcademy.Models;

public partial class Holiday
{
    public byte HolidayId { get; set; }

    public string? HolidayName { get; set; }

    public byte? Day { get; set; }

    public byte Duration { get; set; }

    public byte? Month { get; set; }

    public virtual ICollection<DaysOff> DaysOffs { get; set; } = new List<DaysOff>();
}
