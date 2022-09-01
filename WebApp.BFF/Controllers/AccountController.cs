using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.BFF.Core.DTOs;
using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("/getAuthUser")]
        public async Task<IActionResult> GetAuthUser()
        {
            var applicationUser = await _userManager.GetUserAsync(User);            
            return Ok(applicationUser);
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest();

            await _signInManager.SignInAsync(applicationUser, false);
            return Ok(applicationUser);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var applicationUser = await _userManager.FindByNameAsync(loginDto.Username);
            if (applicationUser == null)
                return BadRequest();

            var result =  await _signInManager.PasswordSignInAsync(applicationUser, loginDto.Password, false, false);
            if (!result.Succeeded)
                return BadRequest();
            return Ok(applicationUser);
        }

        [HttpPost]
        [Route("/logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
