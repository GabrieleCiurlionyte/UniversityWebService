﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityService.Contracts.DTOs.Course;
using UniversityService.Data;
using UniversityService.Models;

namespace UniversityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Courses?pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses(int pageNumber = 1, int pageSize = 10)
        {
            var courses = _context.Courses.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!courses.Any())
            {
                return NotFound("This page does not exist");;
            }

            return await courses.AsNoTracking().ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> PutCourse(int id, CourseEditDto courseDto)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return BadRequest();
            }

            course.Name = courseDto.Name ?? course.Name;
            course.Description = courseDto.Description ?? course.Description;
            course.LecturerId = courseDto.LecturerId ?? course.LecturerId;

            _context.Update(course);
            _context.Entry(course).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControllerHelper.CourseExists(_context, id))
                {
                    return NotFound();
                }
                throw;
            }

            return course;
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDto courseDto)
        {
            var course = new Course
            {
                CourseId = 0,
                Description = courseDto.Description,
                Name = courseDto.Name,
                LecturerId = courseDto.LecturerId

            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
