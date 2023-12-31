﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi_H3_Database.DTO;
using StudentApi_H3_Database.Models;

namespace StudentApi_H3_Database.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CoursesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            
            List<Course> CourseList = new List<Course>(); 
            CourseList = await _context.Courses.
                Include(s => s.StudentCourse).
                ThenInclude(s => s.Student).
                ThenInclude(t => t.Team).
                ToListAsync();  
            
            List<CourseDTO> CourseDTOList = new List<CourseDTO>();

            CourseDTOList = CourseList.Adapt<CourseDTO[]>().ToList();

            return Ok(CourseDTOList);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            
            var course = await _context.Courses.
                Include(s => s.StudentCourse).
                ThenInclude(s => s.Student).
                ThenInclude(t => t.Team).
                FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            CourseDTO CourseDTOObjekt = course.Adapt<CourseDTO>();    

            return Ok(CourseDTOObjekt);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, UpdateCourseDTO courseDTO)
        {
            if (id != courseDTO.CourseId)
            {
                return BadRequest();
            }

            var course = courseDTO.Adapt<Course>();

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaveCourseDTO>> PostCourse(SaveCourseDTO courseDTO)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'DatabaseContext.Courses'  is null.");
          }

            Course course = courseDTO.Adapt<Course>();  

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
