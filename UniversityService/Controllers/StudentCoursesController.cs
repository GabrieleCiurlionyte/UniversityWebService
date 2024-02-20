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

        // GET: api/StudentCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            return await _context.StudentCourses.ToListAsync();
        }

        // GET: api/StudentCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(int studentId, int courseId)
        {
            //todo: TEST
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);

            if (studentCourse == null)
            {
                return NotFound();
            }

            return studentCourse;
        }

        // PUT: api/Courses/5
        //TODO: test
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCourse(int studentId, int courseId, StudentCourse studentCourse)
        {
            //Search for studentId
            //Search for courseId

            //if (id != course.CourseId)
            //{
            //    return BadRequest();
            //}

            _context.Entry(studentCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCourseExist(studentId, courseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //TODO test
        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostStudentCourse(StudentCourse studentCourse)
        {
            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentCourse", new { studentCourse.CourseId, studentCourse.StudentId }, studentCourse);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCourse(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCourseExist(int studentId, int courseId)
        {
            return _context.StudentCourses.Any(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
