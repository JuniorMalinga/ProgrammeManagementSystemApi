using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammeManagementSystemApi.Data;
using ProgrammeManagementSystemApi.Models;

namespace ProgrammeManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModules()
        {
            return await _context.Modules.OrderBy(m => m.ModuleCode).ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var @module = await _context.Modules
                .Include(m => m.Registrations)
                .ThenInclude(r => r.Student)
                .Include(m => m.ModuleAssignments)
                .ThenInclude(ma => ma.Lecturer)
                .FirstOrDefaultAsync(m => m.ModuleID == id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Modules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.ModuleID)
            {
                return BadRequest();
            }

            if (await _context.Modules.AnyAsync(m => m.ModuleCode == @module.ModuleCode && m.ModuleID != id))
            {
                return BadRequest("Module code already exists.");
            }

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Modules
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            if (await _context.Modules.AnyAsync(m => m.ModuleCode == @module.ModuleCode))
            {
                return BadRequest("Module code already exists.");
            }

            _context.Modules.Add(@module);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = @module.ModuleID }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.ModuleID == id);
        }
    }
}
