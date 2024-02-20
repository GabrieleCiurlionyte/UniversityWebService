namespace UniversityService.Contracts.DTOs.Course
{
    public class CourseCreateDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string LecturerId { get; set; }
    }
}
