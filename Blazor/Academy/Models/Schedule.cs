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
        public int group {  get; set; }

        [Column("discipline", TypeName = "SMALLINT")]
        public int discipline { get; set; }

        [Column("teacher", TypeName = "SMALLINT")]
        public int teacher {  get; set; }

        [Column("date", TypeName ="DATE")]
        public DateOnly? date {  get; set; }

        [Column("time", TypeName = "TIME(0)")]
        public TimeSpan? time { get; set; }

        [Column("spent")]
        public bool spent { get; set; }

    }
}
