using Microsoft.EntityFrameworkCore;
using UniversityService.Models;

namespace UniversityService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; init; }
        public DbSet<Course> Courses { get; init; }

        public DbSet<StudentCourse> StudentCourses { get; init;}
    }
}
