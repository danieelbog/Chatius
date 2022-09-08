using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.BFF.Core.DTOs;
using WebApp.BFF.Database;

namespace WebApp.BFF.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private ChatiusContext _dbContext;
        public MessageController(ChatiusContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/getMessages")]
        public async Task<IActionResult> GetMessages([FromQuery] string groupId)
        {
            var messages = await _dbContext.Messages
                .Include(m => m.Author)
                .Include(m => m.Group)
                .Where(m => m.GroupId == groupId)
                .ToListAsync();

            var messagesDto = new List<MessageDto>();
            if (!messages.Any())
                return Ok(messagesDto);

            foreach (var message in messages)
            {
                var authorDto = new UserDto(message.Author.UserName, message.Author.Email);
                var messageDto = new MessageDto(message.Id, authorDto, groupId);
                messagesDto.Add(messageDto);
            }

            return Ok(messagesDto);
        }
    }
}
