using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<BasicIdentityUser> _userManager;

        private readonly IUserRepository _userRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly IMapper _mapper;

        public UsersController(UserManager<BasicIdentityUser> userManager, 
                               IUserRepository userRepo, 
                               IGeneralRepository generalRepo, 
                               IMapper mapper)
        {
            _userManager = userManager;

            _userRepo = userRepo;
            _generalRepo = generalRepo;

            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<UserDto>>> GetUsers()
        {
            var usersModel = await _userRepo.GetAllUsersAsync();
            var usersDto = _mapper.Map<ICollection<UserDto>>(usersModel);

            return usersDto.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var user = await _userRepo.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        // GET: api/Users/test@contoso.com
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

            return userDto;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(string id, UpdateUserDto user)
        {
            if (id != user.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepo.GetUserById(id);
            existingUser = _mapper.Map<UpdateUserDto, User>(user, existingUser);

            await _generalRepo.ChangeEntryStateToModified(existingUser);

            try
            {
                await _generalRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userRepo.CheckIfUserExist(id))
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
    }
}
