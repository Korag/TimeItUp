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
using TimeItUpServices.Library;

namespace TimeItUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimersController : ControllerBase
    {
        private readonly ITimerRepository _timerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly ITimePeriodTimerCalcFacade _timeCalc;
        private readonly IMapper _mapper;


        public TimersController(ITimerRepository timerRepo,
                                IUserRepository userRepo,
                                IGeneralRepository generalRepo,
                                ITimePeriodTimerCalcFacade timeCalc,
                                IMapper mapper)
        {
            _timerRepo = timerRepo;

            _userRepo = userRepo;
            _generalRepo = generalRepo;

            _timeCalc = timeCalc;
            _mapper = mapper;
        }

        // GET: api/Timers
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetAllTimers()
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

        // GET: api/Timers/Multiple
        [HttpGet("Multiple")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetTimersByIds([FromBody]ICollection<int> idSet)
        {
            var timers = await _timerRepo.GetTimersByIdsAsync(idSet);
            var timersDto = _mapper.Map<ICollection<TimerDto>>(timers);

            return Ok(timersDto);
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

        // GET: api/Timers/User/{id}
        [HttpGet("User/{id}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetUserTimers(string id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Active/User/{id}
        [HttpGet("Active/User/{id}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetActiveUserTimers(string id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.Where(z => !z.Finished).ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Finished/User/{id}
        [HttpGet("Finished/User/{id}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetFinishedUserTimers(string id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userTimers = user.Timers?.Where(z => z.Finished).ToList();
            var timersDto = _mapper.Map<ICollection<TimerDto>>(userTimers).ToList();

            return Ok(timersDto);
        }

        // GET: api/Timers/Paused/User/{id}
        [HttpGet("Paused/User/{id}")]
        [Authorize]
        public async Task<ActionResult<ICollection<TimerDto>>> GetPausedUserTimers(string id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

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
        public async Task<IActionResult> UpdateTimer(int id, UpdateTimerDto timer)
        {
            if (id != timer.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTimer = await _timerRepo.GetTimerByIdAsync(id);

            if (existingTimer == null)
            {
                return NotFound();
            }

            existingTimer = _mapper.Map<UpdateTimerDto, Timer>(timer, existingTimer);

            await _generalRepo.ChangeEntryStateToModified(existingTimer);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Timers/Start/5
        [HttpPut("Start/{id}")]
        [Authorize]
        public async Task<IActionResult> StartTimer(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);

            if (timer == null)
            {
                return NotFound();
            }

            if (timer.StartAt != DateTime.MinValue)
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
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Timer/Finish/5
        [HttpPut("Finish/{id}")]
        [Authorize]
        public async Task<IActionResult> FinishTimer(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);

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
                var lastPause = timer.Pauses?.Where(z => z.EndAt == DateTime.MinValue).FirstOrDefault();

                if (lastPause != null)
                {
                    lastPause.EndAt = DateTime.UtcNow;
                    lastPause = _timeCalc.CalculatePauseTimePeriod(lastPause);
                    await _generalRepo.ChangeEntryStateToModified(lastPause);
                }
            }
            else
            {
                var lastSplit = timer.Splits?.Where(z => z.EndAt == DateTime.MinValue).FirstOrDefault();

                if (lastSplit != null)
                {
                    lastSplit.EndAt = DateTime.UtcNow;
                    lastSplit = _timeCalc.CalculateSplitTimePeriod(lastSplit);
                    await _generalRepo.ChangeEntryStateToModified(lastSplit);
                }
            }

            timer.EndAt = DateTime.UtcNow;
            timer.Finished = true;
            timer = _timeCalc.CalculateTimerTimePeriods(timer);

            await _generalRepo.ChangeEntryStateToModified(timer);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Timer/Reinstate/5
        [HttpPut("Reinstate/{id}")]
        [Authorize]
        public async Task<IActionResult> ReinstateTimer(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);

            if (timer == null)
            {
                return NotFound();
            }

            if (timer.StartAt == DateTime.MinValue || timer.EndAt == DateTime.MinValue)
            {
                return BadRequest();
            }

            timer.EndAt = DateTime.MinValue;
            timer.Paused = false;

            var nextSplit = new Split()
            {
                TimerId = timer.Id,
                StartAt = DateTime.UtcNow
            };
            timer.Splits.Add(nextSplit);

            await _generalRepo.ChangeEntryStateToModified(timer);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Timers/Active/All/CalculatePeriods
        [HttpPut("Active/All/CalculatePeriods")]
        [Authorize]
        public async Task<IActionResult> CalculateAllActiveTimersPeriods()
        {
            var timers = await _timerRepo.GetAllActiveTimersAsync();
            await CalculateTimerPeriods(timers);

            return NoContent();
        }

        // PUT: api/Timers/Multiple/CalculatePeriods
        [HttpPut("Multiple/CalculatePeriods")]
        [Authorize]
        public async Task<IActionResult> CalculateSelectedTimersPeriods(ICollection<int> ids)
        {
            var timers = await _timerRepo.GetTimersByIdsAsync(ids);
            await CalculateTimerPeriods(timers);

            return NoContent();
        }

        [HttpPut("Active/User/CalculatePeriods/{id}")]
        [Authorize]
        public async Task<IActionResult> CalculateUserActiveTimersPeriods(string id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userActiveTimers = user.Timers?.Where(z => !z.Finished && !z.Paused).ToList();
            await CalculateTimerPeriods(userActiveTimers);

            return NoContent();
        }

        // PUT: api/Timers/CalculatePeriods/{id}
        [HttpPut("CalculatePeriods/{id}")]
        [Authorize]
        public async Task<IActionResult> CalculateSelectedTimerPeriods(int id)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(id);
            await CalculateTimerPeriods(new List<Timer> { timer });

            return NoContent();
        }

        // POST: api/Timers
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TimerDto>> CreateTimer(CreateTimerDto timer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTimer = _mapper.Map<Timer>(timer);

            if (!_userRepo.CheckIfUserExist(timer.UserId))
            {
                return NotFound();
            }

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

            var timerDto = _mapper.Map<TimerDto>(createdTimer);

            return CreatedAtAction("GetTimerById", new { id = timerDto.Id }, timerDto);
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

        private async Task CalculateTimerPeriods(ICollection<Timer> timers)
        {
            var timersList = timers.ToList();

            for (int i = 0; i < timersList.Count; i++)
            {
                var splitsList = timersList[i].Splits.ToList();
                var pausesList = timersList[i].Pauses.ToList();

                for (int j = 0; j < splitsList.Count; j++)
                {
                    splitsList[j] = _timeCalc.CalculateSplitTimePeriod(splitsList[j]);
                    await _generalRepo.ChangeEntryStateToModified(splitsList[j]);
                }
                for (int j = 0; j < pausesList.Count; j++)
                {
                    pausesList[j] = _timeCalc.CalculatePauseTimePeriod(pausesList[j]);
                    await _generalRepo.ChangeEntryStateToModified(pausesList[j]);
                }
                await _generalRepo.SaveChangesAsync();

                timersList[i] = _timeCalc.CalculateTimerTimePeriods(timersList[i]);
                await _generalRepo.ChangeEntryStateToModified(timersList[i]);
            }

            await _generalRepo.SaveChangesAsync();
        }
    }
}