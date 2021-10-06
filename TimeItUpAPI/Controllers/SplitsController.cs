using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SplitsController : ControllerBase
    {
        private readonly EFDbContext _context;

        public SplitsController(EFDbContext context)
        {
            _context = context;
        }

        // GET: api/Splits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Split>>> GetSplits()
        {
            return await _context.Splits.ToListAsync();
        }

        // GET: api/Splits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Split>> GetSplit(string id)
        {
            var split = await _context.Splits.FindAsync(id);

            if (split == null)
            {
                return NotFound();
            }

            return split;
        }

        // PUT: api/Splits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSplit(string id, Split split)
        {
            if (id != split.Id)
            {
                return BadRequest();
            }

            _context.Entry(split).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SplitExists(id))
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

        // POST: api/Splits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Split>> PostSplit(Split split)
        {
            _context.Splits.Add(split);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SplitExists(split.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSplit", new { id = split.Id }, split);
        }

        // DELETE: api/Splits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSplit(string id)
        {
            var split = await _context.Splits.FindAsync(id);
            if (split == null)
            {
                return NotFound();
            }

            _context.Splits.Remove(split);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SplitExists(string id)
        {
            return _context.Splits.Any(e => e.Id == id);
        }
    }
}
