using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models
{
    [Keyless]
    public class ScheduleView
    {
        [Column("Группа")]
        public string GroupName { get; set; } = "";

        [Column("Дисциплина")]
        public string DisciplineName { get; set; } = "";

        [Column("Преподаватель")]
        public string TeacherName { get; set; } = "";
        
        [Column("Дата")]
        public DateOnly? Date { get; set; }
        
        [Column("День недели")]
        public string? DayOfWeek { get; set; }

        [Column("Время")]
        public TimeSpan? Time { get; set; }

        [Column("Статус")]
        public string Status { get; set; } = "";
    }
}
