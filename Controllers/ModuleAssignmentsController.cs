using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammeManagementSystemApi.Data;
using ProgrammeManagementSystemApi.Models;

namespace ProgrammeManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleAssignmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModuleAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ModuleAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleAssignment>>> GetModuleAssignments()
        {
            return await _context.ModuleAssignments
                .Include(ma => ma.Lecturer)
                .Include(ma => ma.Module)
                .ToListAsync();
        }

        // GET: api/ModuleAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleAssignment>> GetModuleAssignment(int id)
        {
            var moduleAssignment = await _context.ModuleAssignments
                .Include(ma => ma.Lecturer)
                .Include(ma => ma.Module)
                .FirstOrDefaultAsync(ma => ma.AssignmentID == id);

            if (moduleAssignment == null)
            {
                return NotFound();
            }

            return moduleAssignment;
        }

        // POST: api/ModuleAssignments
        [HttpPost]
        public async Task<ActionResult<ModuleAssignment>> PostModuleAssignment(ModuleAssignment moduleAssignment)
        {
            // Check for duplicate assignment
            if (await _context.ModuleAssignments.AnyAsync(ma => ma.LecturerID == moduleAssignment.LecturerID && ma.ModuleID == moduleAssignment.ModuleID))
            {
                return BadRequest("Lecturer is already assigned to this module.");
            }

            _context.ModuleAssignments.Add(moduleAssignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModuleAssignment", new { id = moduleAssignment.AssignmentID }, moduleAssignment);
        }

        // DELETE: api/ModuleAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModuleAssignment(int id)
        {
            var moduleAssignment = await _context.ModuleAssignments.FindAsync(id);
            if (moduleAssignment == null)
            {
                return NotFound();
            }

            _context.ModuleAssignments.Remove(moduleAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleAssignmentExists(int id)
        {
            return _context.ModuleAssignments.Any(e => e.AssignmentID == id);
        }
    }
}
