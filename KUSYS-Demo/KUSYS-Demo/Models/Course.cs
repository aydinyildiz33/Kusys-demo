using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public IEnumerable<StudentCourse> studentCourses { get; set; }
    }
}
