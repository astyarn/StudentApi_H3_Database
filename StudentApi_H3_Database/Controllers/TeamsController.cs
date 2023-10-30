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
    public class TeamsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TeamsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            
            List<Team> TeamList = new List<Team>();

            TeamList = await _context.Teams.
                Include(s => s.Student).
                ToListAsync();

            List<TeamDTO> TeamDTOList = new List<TeamDTO>();

            TeamDTOList = TeamList.Adapt<TeamDTO[]>().ToList();
            
            return Ok(TeamDTOList);
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            var team = await _context.Teams.
                Include(s => s.Student).
                FirstOrDefaultAsync(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            TeamDTO TeamDTOObjekt = team.Adapt<TeamDTO>();  

            return Ok(TeamDTOObjekt);
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, UpdateTeamDTO teamDTO)
        {
            if (id != teamDTO.TeamId)
            {
                return BadRequest();
            }

            var team = teamDTO.Adapt<Team>();

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
          if (_context.Teams == null)
          {
              return Problem("Entity set 'DatabaseContext.Teams'  is null.");
          }
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.TeamId }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return (_context.Teams?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }
    }
}
