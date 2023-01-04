using KUSYS_Demo.DataModel;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly DataContext _context;

        public StudentCourseController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new StudentCourseModel();
            model.Courses = _context
                .Courses
                .Include(i => i.studentCourses)
                .ThenInclude(i => i.Student)
                .ToList();

            model.Students = _context
                .Students
                .Include(i => i.studentCourses)
                .ThenInclude(i => i.Course)
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _context.Students.Find(id);
            ViewBag.Courses = _context.Courses.Include(p => p.studentCourses);

            return View("StudentEditor", student);
        }

        [HttpPost]
        public IActionResult EditStudent(int id, int[] courseid)
        {
            Student student = _context
                .Students
                .Include(s => s.studentCourses)
                .First(i => i.StudentId == id);

            if (student != null)
            {
                student.studentCourses = courseid.Select(cid => new StudentCourse()
                {
                    CourseId = cid,
                    StudentId = id
                }).ToList();

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
