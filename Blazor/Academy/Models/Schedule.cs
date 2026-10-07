using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Models
{
    public class Schedule
    {

        [Key]
        [Column("lesson_id", TypeName = "BIGINT")]
        public long lesson_id { get; set; }

        [Column("group")]
        [ForeignKey(nameof(Group))]
        public int group {  get; set; }

        [Column("discipline", TypeName = "SMALLINT")]
        [ForeignKey(nameof(Discipline))]
        public int discipline { get; set; }

        [Column("teacher", TypeName = "SMALLINT")]
        [ForeignKey(nameof(Teacher))]
        public int teacher {  get; set; }


        [Column("date", TypeName ="DATE")]
        public DateOnly? date {  get; set; }


        [Column("time", TypeName = "TIME(0)")]
        public TimeSpan? time { get; set; }

        [Column("spent")]
        public bool spent { get; set; }

        public Group Group { get; set; }
        public Discipline Discipline { get; set; }
        public Teacher Teacher { get; set; }

    }
}
