using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;
using TimeItUpData.Library.Repositories;

namespace TimeItUpAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TimersController : ControllerBase
    {
        private readonly ITimerRepository _timerRepo;
        private readonly ISplitRepository _splitRepo;
        private readonly IPauseRepository _pauseRepo;
        private readonly IUserRepository _userRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly IMapper _mapper;


        public TimersController(ITimerRepository timerRepo,
                                IUserRepository userRepo,
                                ISplitRepository splitRepo,
                                IPauseRepository pauseRepo,
                                IGeneralRepository generalRepo,
                                IMapper mapper)
        {
            _timerRepo = timerRepo;
            _userRepo = userRepo;
            _splitRepo = splitRepo;
            _pauseRepo = pauseRepo;
            _generalRepo = generalRepo;

            _mapper = mapper;
        }

        // GET: api/Timers
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetTimers()
        {
            var timers = await _timerRepo.GetAllTimersAsync();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(timers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Active
        [HttpGet("Active")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetActiveTimers()
        {
            var timers = await _timerRepo.GetAllActiveTimersAsync();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(timers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Finished
        [HttpGet("Finished")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetFinishedTimers()
        {
            var timers = await _timerRepo.GetAllFinishedTimersAsync();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(timers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Paused
        [HttpGet("Paused")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetPausedTimers()
        {
            var timers = await _timerRepo.GetAllPausedTimersAsync();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(timers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TimerDto>> GetTimerById(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);

            if (timer == null)
            {
                return NotFound();
            }

            var timerDto = _mapper.Map<TimerDto>(timer);

            return Ok(timerDto);
        }

        // GET: api/Timers/CompleteInfo/5
        [HttpGet("CompleteInfo/{id}")]
        [Authorize]
        public async Task<ActionResult<CompleteTimerInfoDto>> GetCompleteTimerInfoById(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);

            if (timer == null)
            {
                return NotFound();
            }

            var completeTimerInfo = new CompleteTimerInfoDto()
            {
                Timer = _mapper.Map<TimerDto>(timer),
                Splits = _mapper.Map<ICollection<SplitDto>>(timer.Splits),
                Alarms = _mapper.Map<ICollection<AlarmDto>>(timer.Alarms),
                Pauses = _mapper.Map<ICollection<PauseDto>>(timer.Pauses)
            };

            return Ok(completeTimerInfo);
        }

        // GET: api/Timers/User/{userId}
        [HttpGet("User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetUserTimers(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Active/User/{userId}
        [HttpGet("Active/User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetOnlyActiveUserTimers(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.Where(z => !z.Finished && !z.Paused).ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Finished/User/{userId}
        [HttpGet("Finished/User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetOnlyFinishedUserTimers(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.Where(z => z.Finished).ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Paused/User/{userId}
        [HttpGet("Paused/User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetOnlyPausedUserTimers(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.Where(z => z.Paused).ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // PUT: api/Timer/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTimer(int timerId, UpdateTimerDto timer)
        {
            if (timerId != timer.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTimer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (existingTimer == null)
            {
                return NotFound();
            }

            existingTimer = _mapper.Map<UpdateTimerDto, Timer>(timer, existingTimer);

            await _generalRepo.ChangeEntryStateToModified(existingTimer);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Timer/Start/5
        [HttpPut("Start/{id}")]
        [Authorize]
        public async Task<IActionResult> StartTimer(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            if (timer.StartAt == DateTime.MinValue)
            {
                return BadRequest();
            }

            timer.StartAt = DateTime.UtcNow;
            timer.Paused = false;

            var initialSplit = new Split()
            {
                TimerId = timer.Id,
                StartAt = DateTime.UtcNow
            };
            timer.Splits.Add(initialSplit);

            await _generalRepo.ChangeEntryStateToModified(timer);
            await _generalRepo.ChangeEntryStateToModified(initialSplit);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Timer/Finish/5
        [HttpPut("Finish/{id}")]
        [Authorize]
        public async Task<IActionResult> FinishTimer(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            if (timer.StartAt == DateTime.MinValue || timer.Finished)
            {
                return BadRequest();
            }

            if (timer.Paused)
            {
                var lastPause = timer.Pauses.Where(z => z.EndAt == DateTime.MinValue).FirstOrDefault();
                lastPause.EndAt = DateTime.UtcNow;

                //Calculate Duration
                lastPause.TotalDuration = "INIT DURATION";

                await _generalRepo.ChangeEntryStateToModified(lastPause);
            }
            else
            {
                var lastSplit = timer.Splits.Where(z => z.EndAt == DateTime.MinValue).FirstOrDefault();
                lastSplit.EndAt = DateTime.UtcNow;

                //Calculate Duration
                lastSplit.TotalDuration = "INIT DURATION";

                await _generalRepo.ChangeEntryStateToModified(lastSplit);
            }

            timer.EndAt = DateTime.UtcNow;
            //Calculate TotalDuration of timer from Adding all TotalDuration from Splits
            timer.TotalDuration = "INIT DURATION";

            await _generalRepo.ChangeEntryStateToModified(timer);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Alarms
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Alarm>> PostTimer(CreateTimerDto timer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTimer = _mapper.Map<Timer>(timer);
            //CHECK
            createdTimer.Splits = new List<Split>();

            await _timerRepo.AddTimerAsync(createdTimer);

            try
            {
                await _generalRepo.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_timerRepo.CheckIfTimerExist(createdTimer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTimerById", new { id = createdTimer.Id }, createdTimer);
        }

        // DELETE: api/Timers/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveTimer(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);

            if (timer == null)
            {
                return NotFound();
            }

            _timerRepo.RemoveTimer(timer);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}