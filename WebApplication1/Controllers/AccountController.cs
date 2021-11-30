using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Share;
using System.Threading.Tasks;
using System;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _userManager.FindByNameAsync(model.Username) is not null
                || await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return BadRequest();
            }

            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Username,
                NormalizedUserName = model.Username.Normalize()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var loging = new LoginModel()
                {
                    Username = model.Username,
                    Password = model.Password
                };
                return await Login(loging);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user is null || await _userManager.CheckPasswordAsync(user, model.Password) is false)
            {
                return Unauthorized();
            }

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpGet("getinfo")]
        public IActionResult GetInfo()
        {
            return Ok("Is private zone");
        }
    }
}
