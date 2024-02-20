using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityService.Data;
using UniversityService.Models;

namespace UniversityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentCoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourses?pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses(int pageNumber = 1, int pageSize = 10)
        {
            var studentCourses = _context.StudentCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!studentCourses.Any())
            {
                return NotFound("This page does not exist");
            }
            return await studentCourses.AsNoTracking().ToListAsync();
        }

        // GET: api/StudentCourses/1/courses/1
        [HttpGet("{studentId}/courses/{courseId}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(int studentId, int courseId)
        {

            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);

            if (studentCourse == null)
            {
                return NotFound($"The {nameof(studentCourse)} with {nameof(studentId)}:" +
                                $"{studentId} and {nameof(courseId)}:{courseId} does not exist.");
            }

            return studentCourse;
        }

        // POST: api/StudentCourses
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> PostStudentCourse(StudentCourse studentCourse)
        {
            if (ControllerHelper.StudentCourseExists(_context, studentCourse))
            {
                return BadRequest($"A course with {nameof(studentCourse.CourseId)}:{studentCourse.CourseId} " +
                                  $"and {nameof(studentCourse.StudentId)}:{studentCourse.StudentId} already exists.");
            }

            if (!ControllerHelper.ValidateCourse(_context, studentCourse.CourseId) || !ControllerHelper.ValidateStudent(_context,studentCourse.StudentId))
            {
                return BadRequest($"The given {nameof(studentCourse.CourseId)} or {nameof(studentCourse.StudentId)} does not exist");
            }

            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            return studentCourse;
        }

        // DELETE: api/StudentCourses/1/courses/1
        [HttpDelete("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> DeleteStudentCourse(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound($"The wanted to delete {nameof(studentCourse)} with {nameof(studentId)}:" +
                                $"{studentId} and {nameof(courseId)}:{courseId} does not exist.");
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
