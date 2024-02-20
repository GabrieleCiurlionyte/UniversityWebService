using UniversityService.Data;
using UniversityService.Models;

namespace UniversityService.Controllers
{
    public static class ControllerHelper
    {
        public static bool CourseExists(AppDbContext context, int id)
        {
            return context.Courses.Any(e => e.CourseId == id);
        }

        public static bool CourseExist(AppDbContext context, int? courseId)
        {
            return context.Courses.Any(e => e.CourseId == courseId);
        }

        public static bool ValidateStudent(AppDbContext context, int? studentId)
        {
            return studentId is null || StudentExist(context, studentId);
        }

        public static bool ValidateCourse(AppDbContext context, int? courseId)
        {
            return courseId is null || CourseExist(context, courseId);
        }

        public static bool StudentExist(AppDbContext context, int? studentId)
        {
            return context.Students.Any(e => e.Id == studentId);
        }
        public static bool StudentExists(AppDbContext context,int id)
        {
            return context.Students.Any(e => e.Id == id);
        }

        public static bool StudentCourseExists(AppDbContext context, StudentCourse studentCourse)
        {
            return context.StudentCourses.Any(e => e.CourseId == studentCourse.CourseId && e.StudentId == studentCourse.StudentId);
        }
    }
}
