﻿using AutoMapper;
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
    public class PausesController : Controller
    {
        private readonly IPauseRepository _pauseRepo;
        private readonly ITimerRepository _timerRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly ITimePeriodTimerCalcFacade _timeCalc;
        private readonly IMapper _mapper;

        public PausesController(IPauseRepository pauseRepo,
                                ITimerRepository timerRepo,
                                IGeneralRepository generalRepo,
                                ITimePeriodTimerCalcFacade timeCalc,
                                IMapper mapper)
        {
            _pauseRepo = pauseRepo;
            _timerRepo = timerRepo;
            _generalRepo = generalRepo;

            _timeCalc = timeCalc;
            _mapper = mapper;
        }

        // GET: api/Pauses
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetPauses()
        {
            var pauses = await _pauseRepo.GetAllPausesAsync();
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(pauses).ToList();

            return Ok(pausesDto);
        }

        // GET: api/Pauses/Active
        [HttpGet("Active")]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetActivePauses()
        {
            var pauses = await _pauseRepo.GetAllActivePausesAsync();
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(pauses).ToList();

            return Ok(pausesDto);
        }

        // GET: api/Pauses/Past
        [HttpGet("Past")]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetPastPauses()
        {
            var pauses = await _pauseRepo.GetAllPastPausesAsync();
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(pauses).ToList();

            return Ok(pausesDto);
        }

        // GET: api/Splits/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SplitDto>> GetPausesById(int id)
        {
            var pause = await _pauseRepo.GetPauseByIdAsync(id);

            if (pause == null)
            {
                return NotFound();
            }

            var pauseDto = _mapper.Map<PauseDto>(pause);

            return Ok(pauseDto);
        }

        // GET: api/Pauses/Multiple
        [HttpGet("Multiple")]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetPausesByIds([FromBody] ICollection<int> idSet)
        {
            var pauses = await _pauseRepo.GetPausesByIdsAsync(idSet);
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(pauses);

            return Ok(pausesDto);
        }

        // GET: api/Pauses/Timer/{timerId}
        [HttpGet("Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetTimerPauses(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerPauses = timer.Pauses?.ToList();
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(timerPauses).ToList();

            return Ok(pausesDto);
        }

        // GET: api/Pauses/Active/Timer/{timerId}
        [HttpGet("Active/Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetTimerActivePauses(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerPauses = timer.Pauses?.Where(z => z.StartAt != DateTime.MinValue && z.EndAt == DateTime.MinValue).ToList();
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(timerPauses).ToList();

            return Ok(pausesDto);
        }

        // GET: api/Pauses/Past/Timer/{timerId}
        [HttpGet("Past/Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<PauseDto>>> GetTimerPastPauses(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerPauses = timer.Pauses?.Where(z => z.StartAt != DateTime.MinValue && z.EndAt != DateTime.MinValue).ToList();
            var pausesDto = _mapper.Map<ICollection<PauseDto>>(timerPauses).ToList();

            return Ok(pausesDto);
        }

        // PUT: api/Pause/Start/5
        [HttpPut("Start/{id}")]
        [Authorize]
        public async Task<IActionResult> StartPause(int pauseId)
        {
            var pause = await _pauseRepo.GetPauseByIdAsync(pauseId);

            if (pause == null)
            {
                return NotFound();
            }

            if (pause.StartAt != DateTime.MinValue)
            {
                return BadRequest();
            }

            pause.StartAt = DateTime.UtcNow;
            //CHECK
            pause.Timer.Paused = true;

            var lastSplit = pause.Timer.Splits?.Where(z => z.EndAt == DateTime.MinValue).FirstOrDefault();

            if (lastSplit != null)
            {
                lastSplit.EndAt = DateTime.UtcNow;
                lastSplit = _timeCalc.CalculateSplitTimePeriod(lastSplit);
                await _generalRepo.ChangeEntryStateToModified(lastSplit);
            }

            await _generalRepo.ChangeEntryStateToModified(pause);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Pause/Finish/5
        [HttpPut("Pause/{id}")]
        [Authorize]
        public async Task<IActionResult> FinishPause(int pauseId)
        {
            var pause = await _pauseRepo.GetPauseByIdAsync(pauseId);

            if (pause == null)
            {
                return NotFound();
            }

            if (pause.StartAt == DateTime.MinValue)
            {
                return BadRequest();
            }

            pause.EndAt = DateTime.UtcNow;
            pause = _timeCalc.CalculatePauseTimePeriod(pause);
            pause.Timer.Paused = false;

            var nextSplit = new Split()
            {
                TimerId = pause.Timer.Id,
                StartAt = DateTime.UtcNow
            };
            pause.Timer.Splits.Add(nextSplit);

            await _generalRepo.ChangeEntryStateToModified(pause);
            //Necessary?
            await _generalRepo.ChangeEntryStateToModified(nextSplit);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Pauses
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PauseDto>> PostSplit(CreatePauseDto pause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPause = _mapper.Map<Pause>(pause);

            if (createdPause.Timer == null)
            {
                return NotFound();
            }

            await _pauseRepo.AddPauseAsync(createdPause);

            try
            {
                await _generalRepo.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_pauseRepo.CheckIfPauseExist(createdPause.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var pauseDto = _mapper.Map<PauseDto>(createdPause);

            return CreatedAtAction("GetPauseById", new { id = pauseDto.Id }, pauseDto);
        }

        // DELETE: api/Pause/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemovePause(int id)
        {
            var pause = await _pauseRepo.GetPauseByIdAsync(id);

            if (pause == null)
            {
                return NotFound();
            }

            _pauseRepo.RemovePause(pause);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
