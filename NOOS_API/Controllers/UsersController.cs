using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NOOS_API.Contracts;
using NOOS_API.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NOOS_API.Controllers
{
    /// <summary>
    /// endpoint for user login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly IConfiguration _config;

        // dependency injection and initializations
        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILoggerService logger, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
        }



        /// <summary>
        /// User  login endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                var email = userDTO.Email;   // chose to use 'email' instead of 'username'
                var password = userDTO.Password;
                _logger.LogInfo($"{location}: Login attemt from a user {email}");
                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

                if (result != null)  // this was reverted from if(result != null)  OR if(result.Succeeded)
                {
                    _logger.LogInfo($"{location}: {email} Successfully authenticated");
                    var user = await _userManager.FindByEmailAsync(email);
                    var tokenString = await GenerateJSONWebToken(user);  // passing the user object to gnereate a JSON token
                    return Ok(new { token = tokenString});
                }
                _logger.LogWarn($"{location}: {email} Not authenticated");
                return Unauthorized(userDTO);

            }
            catch (Exception e)
            {

                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
          
        }


        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // hashing
            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            // building the token 

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"], claims, null, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials); // expires after 5 mins of issuing
            
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        
        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return ($"{controller} -{action}");
        }

        // errormessage function
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator");
        }


    }
}
