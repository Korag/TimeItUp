using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;
using TimeItUpData.Library.Repositories;

namespace TimeItUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<BasicIdentityUser> _userManager;
        private readonly SignInManager<BasicIdentityUser> _signInManager;

        private readonly IUserRepository _userRepo;
        private readonly IGeneralRepository _generalRepo;

        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<BasicIdentityUser> userManager, 
                                  SignInManager<BasicIdentityUser> signInManager,
                                  IConfiguration config,
                                  IMapper mapper,
                                  IUserRepository userRepo,
                                  IGeneralRepository generalRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _config = config;
            _mapper = mapper;

            _userRepo = userRepo;
            _generalRepo = generalRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<JwtTokenDto>> Login(UserLoginDto user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    var existingUser = _userManager.Users.FirstOrDefault(z => z.Email == user.Email);
                    var token = await GenerateJwtToken(existingUser.Email);

                    return Ok(token);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _mapper.Map<BasicIdentityUser>(user);
                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    var createdUser = _mapper.Map<User>(user);
                    createdUser = _mapper.Map<BasicIdentityUser, User>(newUser, createdUser);

                    await _userRepo.AddUserAsync(createdUser);
                    await _generalRepo.SaveChangesAsync();

                    return CreatedAtAction("GetUserById", "UsersController", new { id = createdUser.Id }, createdUser);
                }
                else
                {
                    ModelState.AddModelError("Overall", "The user with the specified email address already exists in the system");
                    return Conflict(ModelState);
                }
            }

            ModelState.AddModelError("Overall", "Incorrectly entered new user data");
            return BadRequest(ModelState);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var existingUser = await _userRepo.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _userRepo.RemoveUser(existingUser);

            var userAccount = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(userAccount);

            if (result.Succeeded)
            {
                await _generalRepo.SaveChangesAsync();
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        //TODO: Change User Password
        //TODO: Reset User Password
        //TODO: Change User Email
        //TODO: Confirm Email Address

        private async Task<JwtTokenDto> GenerateJwtToken(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, emailAddress),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Secrets:JwtKey")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(new JwtHeader(credentials), new JwtPayload(claims));

            return new JwtTokenDto { Jwt = new JwtSecurityTokenHandler().WriteToken(token), EmailAddress = user.Email };
        }
    }
}
