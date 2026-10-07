using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Models
{
    [Table("TeachersDisciplinesRelation")]
    [PrimaryKey(nameof(teacher), nameof(discipline))]
    public class TeachersDisciplinesRelation
    {
        //[Key]
        [Column(TypeName ="SMALLINT")]
        [ForeignKey(nameof(Teacher))]
        public int teacher {  get; set; }

        //[Key]
        [Column(TypeName ="SMALLINT")]
        [ForeignKey(nameof(Discipline))]
        public int discipline { get; set; }

        public Teacher Teacher { get; set; }
        public Discipline Discipline { get; set; }
    }
}
