﻿using System.ComponentModel.DataAnnotations;

namespace UniversityService.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Address { get; set; }
    }
}
