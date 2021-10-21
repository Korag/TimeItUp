using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo,
                               IGeneralRepository generalRepo,
                               IMapper mapper)
        {
            _userRepo = userRepo;
            _generalRepo = generalRepo;

            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<UserDto>>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            var usersDto = _mapper.Map<ICollection<UserDto>>(users);

            return Ok(usersDto.ToList());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        // GET: api/Users/Multiple
        [HttpGet("Multiple")]
        [Authorize]
        public async Task<ActionResult<ICollection<UserDto>>> GetUsersByIds([FromBody] ICollection<string> idSet)
        {
            var users = await _userRepo.GetUsersByIdsAsync(idSet);
            var usersDto = _mapper.Map<ICollection<TimerDto>>(users);

            return Ok(usersDto);
        }

        // GET: api/Users/Email/test@contoso.com
        [HttpGet("Email/{email}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepo.GetUserByEmailAddress(email);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto user)
        {
            if (id != user.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepo.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser = _mapper.Map<UpdateUserDto, User>(user, existingUser);

            await _generalRepo.ChangeEntryStateToModified(existingUser);
            await _generalRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
