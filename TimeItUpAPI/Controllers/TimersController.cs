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
    public class TimersController : ControllerBase
    {
        private readonly EFDbContext _context;

        public TimersController(EFDbContext context)
        {
            _context = context;
        }

        //GET: GetAllTimers
        //GET: GetTimerById
        //GET: GetAllTimersByUserId

        //GET: TimerSplits
        //GET: TimerPauses
        //GET: GetTimerActivePauses
        //GET: GetTimerPastPauses

        //GET: GetAllActiveTimers
        //GET: GetAllPastTimers
        //GET: GetAllActiveTimersByUserId
        //GET: GetAllPastTimersByUserId

        //POST: AddNewTimer

        //PUT: EditTimer
        //PUT: FinishTimer

        //DELETE: RemoveTimer

        // GET: api/Timers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timer>>> GetTimers()
        {
            return await _context.Timers.ToListAsync();
        }

        // GET: api/Timers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Timer>> GetTimer(string id)
        {
            var timer = await _context.Timers.FindAsync(id);

            if (timer == null)
            {
                return NotFound();
            }

            return timer;
        }

        // PUT: api/Timers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimer(int id, Timer timer)
        {
            if (id != timer.Id)
            {
                return BadRequest();
            }

            _context.Entry(timer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimerExists(id))
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

        // POST: api/Timers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Timer>> PostTimer(Timer timer)
        {
            _context.Timers.Add(timer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TimerExists(timer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTimer", new { id = timer.Id }, timer);
        }

        // DELETE: api/Timers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimer(string id)
        {
            var timer = await _context.Timers.FindAsync(id);
            if (timer == null)
            {
                return NotFound();
            }

            _context.Timers.Remove(timer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimerExists(int id)
        {
            return _context.Timers.Any(e => e.Id == id);
        }
    }
}
