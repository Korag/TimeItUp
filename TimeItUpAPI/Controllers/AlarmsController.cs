using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmsController : ControllerBase
    {
        private readonly EFDbContext _context;

        public AlarmsController(EFDbContext context)
        {
            _context = context;
        }

        // GET: api/Alarms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alarm>>> GetAlarms()
        {
            return await _context.Alarms.ToListAsync();
        }

        // GET: api/Alarms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alarm>> GetAlarm(string id)
        {
            var alarm = await _context.Alarms.FindAsync(id);

            if (alarm == null)
            {
                return NotFound();
            }

            return alarm;
        }

        // PUT: api/Alarms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlarm(string id, Alarm alarm)
        {
            if (id != alarm.Id)
            {
                return BadRequest();
            }

            _context.Entry(alarm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlarmExists(id))
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

        // POST: api/Alarms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alarm>> PostAlarm(Alarm alarm)
        {
            _context.Alarms.Add(alarm);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlarmExists(alarm.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlarm", new { id = alarm.Id }, alarm);
        }

        // DELETE: api/Alarms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlarm(string id)
        {
            var alarm = await _context.Alarms.FindAsync(id);
            if (alarm == null)
            {
                return NotFound();
            }

            _context.Alarms.Remove(alarm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlarmExists(string id)
        {
            return _context.Alarms.Any(e => e.Id == id);
        }
    }
}
