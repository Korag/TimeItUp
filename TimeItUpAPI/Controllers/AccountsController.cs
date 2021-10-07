using Microsoft.AspNetCore.Authorization;
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

namespace TimeItUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<BasicIdentityUser> _userManager;
        private readonly SignInManager<BasicIdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountsController(UserManager<BasicIdentityUser> userManager, 
                                  SignInManager<BasicIdentityUser> signInManager,
                                  IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _config = config;
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
