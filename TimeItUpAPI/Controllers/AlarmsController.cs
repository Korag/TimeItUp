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
    [ApiController]
    public class AlarmsController : ControllerBase
    {
        private readonly IAlarmRepository _alarmRepo;
        private readonly ITimerRepository _timerRepo;
        private readonly IUserRepository _userRepo;

        private readonly IGeneralRepository _generalRepo;

        private readonly IMapper _mapper;

        public AlarmsController(IAlarmRepository alarmRepo,
                                ITimerRepository timerRepo,
                                IUserRepository userRepo,
                                IGeneralRepository generalRepo,
                                IMapper mapper)
        {
            _alarmRepo = alarmRepo;
            _timerRepo = timerRepo;
            _userRepo = userRepo;
            _generalRepo = generalRepo;

            _mapper = mapper;
        }

        // GET: api/Alarms
        [HttpGet]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetAllAlarms()
        {
            var alarms = await _alarmRepo.GetAllAlarmsAsync();
            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(alarms).ToList();

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AlarmDto>> GetAlarmById(int id)
        {
            var alarm = await _alarmRepo.GetAlarmByIdAsync(id);

            if (alarm == null)
            {
                return NotFound();
            }

            var alarmDto = _mapper.Map<AlarmDto>(alarm);

            return Ok(alarmDto);
        }

        // GET: api/Alarms/Multiple
        [HttpGet("Multiple")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetAlarmsByIds([FromBody] ICollection<int> idSet)
        {
            var alarms = await _alarmRepo.GetAlarmsByIdsAsync(idSet);
            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(alarms);

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/User/{userId}
        [HttpGet("User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetUserAlarms(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userAlarms = new List<Alarm>();
            user.Timers?.ToList().ForEach(z => z.Alarms.ToList().ForEach(c => userAlarms.Add(c)));

            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(userAlarms).ToList();

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/Active/User/{userId}
        [HttpGet("Active/User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetOnlyActiveUserAlarms(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userAlarms = new List<Alarm>();
            user.Timers?.ToList().ForEach(z => z.Alarms
                        .Where(z => z.ActivationTime > DateTime.UtcNow)
                        .ToList().ForEach(c => userAlarms.Add(c)));

            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(userAlarms).ToList();

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/Past/User/{userId}
        [HttpGet("Past/User/{userId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetOnlyPastUserAlarms(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userAlarms = new List<Alarm>();
            user.Timers?.ToList().ForEach(z => z.Alarms.Where(z => z.ActivationTime < DateTime.UtcNow).ToList().ForEach(c => userAlarms.Add(c)));

            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(userAlarms).ToList();

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/Timer/{timerId}
        [HttpGet("Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetTimerAlarms(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerAlarms = new List<Alarm>();
            timerAlarms = timer.Alarms?.ToList();

            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(timerAlarms).ToList();

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/Active/Timer/{timerId}
        [HttpGet("Active/Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetTimerActiveAlarms(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerAlarms = new List<Alarm>();
            timerAlarms = timer.Alarms?.Where(z => z.ActivationTime > DateTime.UtcNow).ToList();

            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(timerAlarms).ToList();

            return Ok(alarmsDto);
        }

        // GET: api/Alarms/Past/Timer/{timerId}
        [HttpGet("Past/Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<AlarmDto>>> GetTimerPastAlarms(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerAlarms = new List<Alarm>();
            timerAlarms = timer.Alarms?.Where(z => z.ActivationTime < DateTime.UtcNow).ToList();

            var alarmsDto = _mapper.Map<ICollection<AlarmDto>>(timerAlarms).ToList();

            return Ok(alarmsDto);
        }

        // PUT: api/Alarms/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAlarm(int alarmId, UpdateAlarmDto alarm)
        {
            if (alarmId != alarm.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAlarm = await _alarmRepo.GetAlarmByIdAsync(alarmId);

            if (existingAlarm == null)
            {
                return NotFound();
            }

            existingAlarm = _mapper.Map<UpdateAlarmDto, Alarm>(alarm, existingAlarm);

            await _generalRepo.ChangeEntryStateToModified(existingAlarm);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Alarms/Postpone/5
        [HttpPut("Postpone/{id}")]
        [Authorize]
        public async Task<IActionResult> PostponeAlarm(int alarmId, DateTime newAlarmActivationTime)
        {
            if (newAlarmActivationTime < DateTime.UtcNow)
            {
                return BadRequest();
            }

            var existingAlarm = await _alarmRepo.GetAlarmByIdAsync(alarmId);

            if (existingAlarm == null)
            {
                return NotFound();
            }

            existingAlarm.ActivationTime = newAlarmActivationTime;

            await _generalRepo.ChangeEntryStateToModified(existingAlarm);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Alarms
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AlarmDto>> CreateAlarm(CreateAlarmDto alarm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAlarm = _mapper.Map<Alarm>(alarm);
            await _alarmRepo.AddAlarmAsync(createdAlarm);

            try
            {
                await _generalRepo.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_alarmRepo.CheckIfAlarmExist(createdAlarm.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var alarmDto = _mapper.Map<AlarmDto>(createdAlarm);

            return CreatedAtAction("GetAlarmById", new { id = alarmDto.Id }, alarmDto);
        }

        // DELETE: api/Alarms/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveAlarm(int id)
        {
            var alarm = await _alarmRepo.GetAlarmByIdAsync(id);

            if (alarm == null)
            {
                return NotFound();
            }

            _alarmRepo.RemoveAlarm(alarm);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
