using System;
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
    public class StudentCoursesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StudentCoursesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
          if (_context.StudentCourses == null)
          {
              return NotFound();
          }
            return await _context.StudentCourses.ToListAsync();
        }

        // GET: api/StudentCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(int id)
        {
          if (_context.StudentCourses == null)
          {
              return NotFound();
          }
            var studentCourse = await _context.StudentCourses.FindAsync(id);

            if (studentCourse == null)
            {
                return NotFound();
            }

            return studentCourse;
        }

        // GET: api/StudentCourses/5
        [HttpGet("/api/StudenCourse/Grade{id}")]
        public async Task<ActionResult<StudentCourseDTOMinusCourse>> GetStudentCourseGrade(int id)
        {
            if (id == -3 || id == 0 || id == 2 || id == 4 || id == 7 || id == 10 || id == 12)
            {
                List<StudentCourse> StudentList = await _context.StudentCourses.
                    Where(sc => sc.Character == id).
                    Include(s => s.Student).
                    ThenInclude(t => t.Team).
                    ToListAsync();

                if (StudentList.Count == 0)
                {
                    return NotFound("No student found with the specific grade.");
                }

                List<StudentCourseDTOMinusCourse> StudentDTOList = StudentList.Adapt<StudentCourseDTOMinusCourse[]>().ToList();

                return Ok(StudentDTOList);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/StudentCourses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCourse(int id, StudentCourse studentCourse)
        {
            if (id != studentCourse.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(studentCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCourseExists(id))
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

        // POST: api/StudentCourses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> PostStudentCourse(StudentCourse studentCourse)
        {
          if (_context.StudentCourses == null)
          {
              return Problem("Entity set 'DatabaseContext.StudentCourses'  is null.");
          }
            _context.StudentCourses.Add(studentCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentCourseExists(studentCourse.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentCourse", new { id = studentCourse.StudentId }, studentCourse);
        }

        // DELETE: api/StudentCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCourse(int id)
        {
            if (_context.StudentCourses == null)
            {
                return NotFound();
            }
            var studentCourse = await _context.StudentCourses.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCourseExists(int id)
        {
            return (_context.StudentCourses?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
