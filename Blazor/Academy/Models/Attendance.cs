using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models
{
    [Table("Attendance")]
    [PrimaryKey(nameof(student), nameof(lesson))]
    public class Attendance
    {
        [Column("student")]
        [ForeignKey(nameof(Student))]
        public int student { get; set; }

        [Column("lesson", TypeName = "BIGINT")]
        [ForeignKey(nameof(Lesson))]
        public long lesson { get; set; }

        [Column("present")]
        public bool present { get; set; }

        public Student Student { get; set; }
        public Schedule Lesson { get; set; }
    }
}
