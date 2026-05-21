using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammeManagementSystemApi.Data;
using ProgrammeManagementSystemApi.Models;

namespace ProgrammeManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Registrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            return await _context.Registrations
                .Include(r => r.Student)
                .Include(r => r.Module)
                .ToListAsync();
        }

        // GET: api/Registrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {
            var registration = await _context.Registrations
                .Include(r => r.Student)
                .Include(r => r.Module)
                .FirstOrDefaultAsync(r => r.RegistrationID == id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // POST: api/Registrations
        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
        {
            // Check for duplicate registration
            if (await _context.Registrations.AnyAsync(r => r.StudentID == registration.StudentID && r.ModuleID == registration.ModuleID))
            {
                return BadRequest("Student is already registered for this module.");
            }

            registration.DateRegistered = DateTime.Now;
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = registration.RegistrationID }, registration);
        }

        // DELETE: api/Registrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationID == id);
        }
    }
}
