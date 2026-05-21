using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammeManagementSystemApi.Data;
using ProgrammeManagementSystemApi.Models;

namespace ProgrammeManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LecturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Lecturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecturer>>> GetLecturers()
        {
            return await _context.Lecturers.ToListAsync();
        }

        // GET: api/Lecturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecturer>> GetLecturer(int id)
        {
            var lecturer = await _context.Lecturers
                .Include(l => l.ModuleAssignments)
                .ThenInclude(ma => ma.Module)
                .FirstOrDefaultAsync(l => l.LecturerID == id);

            if (lecturer == null)
            {
                return NotFound();
            }

            return lecturer;
        }

        // PUT: api/Lecturers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturer(int id, Lecturer lecturer)
        {
            if (id != lecturer.LecturerID)
            {
                return BadRequest();
            }

            if (await _context.Lecturers.AnyAsync(l => l.Email == lecturer.Email && l.LecturerID != id))
            {
                return BadRequest("Email already exists.");
            }

            _context.Entry(lecturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(id))
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

        // POST: api/Lecturers
        [HttpPost]
        public async Task<ActionResult<Lecturer>> PostLecturer(Lecturer lecturer)
        {
            if (await _context.Lecturers.AnyAsync(l => l.Email == lecturer.Email))
            {
                return BadRequest("Email already exists.");
            }

            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturer", new { id = lecturer.LecturerID }, lecturer);
        }

        // DELETE: api/Lecturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.LecturerID == id);
        }
    }
}
