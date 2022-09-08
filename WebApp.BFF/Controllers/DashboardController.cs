using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.BFF.Core.DTOs;
using WebApp.BFF.Core.Models;
using WebApp.BFF.Database;

namespace WebApp.BFF.Controllers
{
    public class DashboardController : Controller
    {
        private ChatiusContext _dbContext;
        public DashboardController(ChatiusContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet("/getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var applicationUsers = await _dbContext.Users
                    .Include(u => u.Messages)
                    .ToListAsync();

                var usersDto = new List<UserDto>();
                if (!applicationUsers.Any())
                    return Ok(usersDto);

                foreach (var applicationUser in applicationUsers)
                {
                    var userDto = new UserDto(applicationUser.UserName, applicationUser.Email);
                    usersDto.Add(userDto);
                }

                return Ok(usersDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
