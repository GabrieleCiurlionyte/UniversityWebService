﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityService.Contracts.DTOs.Student;
using UniversityService.Data;
using UniversityService.Models;

namespace UniversityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students?pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(int pageNumber = 1, int pageSize = 10)
        {
            var students = _context.Students.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!students.Any())
            {
                return NotFound("This page does not exist");
            }
            return await students.AsNoTracking().ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> PutStudent(int id, StudentEditDto studentDto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student is null)
            {
                return BadRequest();
            }

            student.Name = studentDto.Name ?? student.Name;
            student.Surname = studentDto.Surname ?? student.Surname;
            student.Address = studentDto.Address ?? student.Address;

            _context.Update(student);
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControllerHelper.StudentExists(_context, id))
                {
                    return NotFound();
                }
                throw;
            }
            return student;
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentCreateDto studentDto)
        {
            var student = new Student
            {
                Id = 0,
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                Address = studentDto.Address
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
