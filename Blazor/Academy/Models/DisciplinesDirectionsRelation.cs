using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Models
{
    [Table("DisciplinesDirectionsRelation")]
    [PrimaryKey(nameof(direction), nameof(discipline))]
    public class DisciplinesDirectionsRelation
    {
        [Column(TypeName = "TINYINT")]
        [ForeignKey(nameof(Direction))]
        public int direction { get; set; }

        [Column(TypeName = "SMALLINT")]
        [ForeignKey(nameof(Discipline))]
        public int discipline { get; set; }

        public Direction Direction { get; set; }
        public Discipline Discipline { get; set; }
    }
}
