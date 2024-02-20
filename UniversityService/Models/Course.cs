using System.ComponentModel.DataAnnotations;

namespace UniversityService.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LecturerId { get; set; }
    }
}
