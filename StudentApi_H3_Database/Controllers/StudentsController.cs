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
    public class StudentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StudentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            List<Student> StudentList = new List<Student>();
            
            StudentList = await _context.Students.
                Include(t => t.Team).
                Include(s => s.StudentCourse).
                ThenInclude(c => c.Course).
                ToListAsync();

            List<StudentDTO> StudentDTOList = new List<StudentDTO>();

            StudentDTOList = StudentList.Adapt<StudentDTO[]>().ToList();

            return Ok(StudentDTOList);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
          if (_context.Students == null)
          {
              return NotFound();
          }
            var student = await _context.Students.
                Include(t => t.Team).
                Include(s => s.StudentCourse).
                ThenInclude(c => c.Course).
                FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            StudentDTO StudentDTOObject = student.Adapt<StudentDTO>();  

            return Ok(StudentDTOObject);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, UpdateStudentDTO studentDTO)
        {
            if (id != studentDTO.StudentId)
            {
                return BadRequest();
            }

            var student = studentDTO.Adapt<Student>();   

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (_context.Students == null)
          {
              return Problem("Entity set 'DatabaseContext.Students'  is null.");
          }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
