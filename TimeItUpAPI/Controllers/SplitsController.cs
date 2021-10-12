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
    public class SplitsController : ControllerBase
    {
        private readonly ISplitRepository _splitRepo;
        private readonly ITimerRepository _timerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly IMapper _mapper;

        public SplitsController(IUserRepository userRepo,
                                ISplitRepository splitRepo,
                                ITimerRepository timerRepo,
                                IGeneralRepository generalRepo,
                                IMapper mapper)
        {
            _splitRepo = splitRepo;
            _timerRepo = timerRepo;

            _userRepo = userRepo;
            _generalRepo = generalRepo;

            _mapper = mapper;
        }

        // GET: api/Splits
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetSplits()
        {
            var splits = await _splitRepo.GetAllSplitsAsync();
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(splits).ToList();

            return Ok(splitsDto);
        }

        // GET: api/Splits/Active
        [HttpGet("Active")]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetActiveSplits()
        {
            var splits = await _splitRepo.GetAllActiveSplitsAsync();
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(splits).ToList();

            return Ok(splitsDto);
        }

        // GET: api/Splits/Past
        [HttpGet("Past")]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetPastTimers()
        {
            var splits = await _splitRepo.GetAllPastSplitsAsync();
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(splits).ToList();

            return Ok(splitsDto);
        }

        // GET: api/Splits/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SplitDto>> GetSplitById(int id)
        {
            var split = await _splitRepo.GetSplitByIdAsync(id);

            if (split == null)
            {
                return NotFound();
            }

            var splitDto = _mapper.Map<SplitDto>(split);

            return Ok(splitDto);
        }

        // GET: api/Splits/Multiple
        [HttpGet("Multiple")]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetSplitsByIds([FromBody] ICollection<int> idSet)
        {
            var splits = await _splitRepo.GetSplitsByIdsAsync(idSet);
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(splits);

            return Ok(splitsDto);
        }

        // GET: api/Splits/Timer/{timerId}
        [HttpGet("Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetTimerSplits(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerSplits = timer.Splits?.ToList();
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(timerSplits).ToList();

            return Ok(splitsDto);
        }

        // GET: api/Splits/Active/Timer/{timerId}
        [HttpGet("Active/Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetTimerActiveSplits(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerSplits = timer.Splits?.Where(z => z.StartAt != DateTime.MinValue && z.EndAt == DateTime.MinValue).ToList();
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(timerSplits).ToList();

            return Ok(splitsDto);
        }

        // GET: api/Splits/Past/Timer/{timerId}
        [HttpGet("Past/Timer/{timerId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<SplitDto>>> GetTimerPastSplits(int timerId)
        {
            var timer = await _timerRepo.GetTimerByIdAsync(timerId);

            if (timer == null)
            {
                return NotFound();
            }

            var timerSplits = timer.Splits?.Where(z => z.StartAt != DateTime.MinValue && z.EndAt != DateTime.MinValue).ToList();
            var splitsDto = _mapper.Map<ICollection<SplitDto>>(timerSplits).ToList();

            return Ok(splitsDto);
        }

        // PUT: api/Split/Start/5
        [HttpPut("Start/{id}")]
        [Authorize]
        public async Task<IActionResult> StartSplit(int splitId)
        {
            var split = await _splitRepo.GetSplitByIdAsync(splitId);

            if (split == null)
            {
                return NotFound();
            }

            if (split.StartAt != DateTime.MinValue)
            {
                return BadRequest();
            }

            split.StartAt = DateTime.UtcNow;

            await _generalRepo.ChangeEntryStateToModified(split);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Split/Finish/5
        [HttpPut("Split/{id}")]
        [Authorize]
        public async Task<IActionResult> FinishSplit(int splitId)
        {
            var split = await _splitRepo.GetSplitByIdAsync(splitId);

            if (split == null)
            {
                return NotFound();
            }

            if (split.StartAt == DateTime.MinValue)
            {
                return BadRequest();
            }

            //Calculate Duration
            split.EndAt = DateTime.UtcNow;
            split.TotalDuration = "INIT DURATION";

            await _generalRepo.ChangeEntryStateToModified(split);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Splits
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Split>> PostSplit(CreateSplitDto split)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSplit = _mapper.Map<Split>(split);

            if (createdSplit.Timer == null)
            {
                return NotFound();
            }

            await _splitRepo.AddSplitAsync(createdSplit);

            try
            {
                await _generalRepo.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_splitRepo.CheckIfSplitExist(createdSplit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSplitById", new { id = createdSplit.Id }, createdSplit);
        }

        // DELETE: api/Split/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveSplit(int id)
        {
            var split = await _splitRepo.GetSplitByIdAsync(id);

            if (split == null)
            {
                return NotFound();
            }

            _splitRepo.RemoveSplit(split);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
