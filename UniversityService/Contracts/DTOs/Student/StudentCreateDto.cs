using System.ComponentModel.DataAnnotations;

namespace UniversityService.Contracts.DTOs.Student
{
    public class StudentCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
    }
}
