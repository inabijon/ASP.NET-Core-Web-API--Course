using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories.IRepository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> UserManager;
        public readonly ITokenRepository TokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            UserManager = userManager;
            TokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto register)
        {
            var identityUser = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };

            var identityResult = await UserManager.CreateAsync(identityUser, register.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this user
                if (register.Roles != null && register.Roles.Any())
                {

                    identityResult = await UserManager.AddToRolesAsync(identityUser, register.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login. ");
                    }
                }

            }

            return BadRequest("Something went wrong!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {
            var user = await UserManager.FindByEmailAsync(login.Username);

            if (user != null)
            {
                var userPasswordResult = await UserManager.CheckPasswordAsync(user, login.Password);

                if (userPasswordResult)
                {
                    // Get Roles for this
                    var roles = await UserManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //Create token and return
                        var jwtToken = TokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password incorrect!");
        }
    }
}
