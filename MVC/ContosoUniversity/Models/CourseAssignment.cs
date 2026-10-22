using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    [PrimaryKey(nameof(InstructorID), nameof(CourseID))]
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
