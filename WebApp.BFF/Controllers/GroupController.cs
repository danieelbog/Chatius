using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.BFF.Core.DTOs;
using WebApp.BFF.Database;

namespace WebApp.BFF.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        private ChatiusContext _dbContext;
        public GroupController(ChatiusContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("/getGroups")]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var groups = await _dbContext.Users
                    .Include(u => u.Groups)
                    .Where(g => g.Id == userId)
                    .SelectMany(u => u.Groups)
                    .Include(g => g.ApplicationUsers)
                    .Distinct()
                    .ToListAsync();

                var groupsDto = new List<GroupDto>();
                if (!groups.Any())
                    return Ok(groupsDto);

                foreach (var group in groups)
                {
                    var members = new List<UserDto>();
                    foreach (var user in group.ApplicationUsers)
                    {
                        var member = new UserDto(user.UserName, user.Email);
                        members.Add(member);
                    }

                    var groupDto = new GroupDto(group.Id, group.Name, members);
                    groupsDto.Add(groupDto);
                }

                return Ok(groupsDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("/getGroup")]
        public async Task<IActionResult> GetGroup([FromQuery] string groupId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var group = await _dbContext.Groups
                    .FirstOrDefaultAsync(g => g.Id == groupId);

                var groupDto = new GroupDto();
                if (group == null)
                    return Ok(groupDto);

                groupDto.Id = group.Id;
                groupDto.Name = group.Name;

                return Ok(groupDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
