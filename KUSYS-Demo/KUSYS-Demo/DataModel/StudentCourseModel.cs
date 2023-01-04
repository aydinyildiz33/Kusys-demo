using KUSYS_Demo.Models;

namespace KUSYS_Demo.DataModel
{
    public class StudentCourseModel
    {
        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}
