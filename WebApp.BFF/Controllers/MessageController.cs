using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.BFF.Core.DTOs;
using WebApp.BFF.Core.Models;
using WebApp.BFF.Database;
using WebApp.BFF.Core;

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
            try
            {
                var messages = await _dbContext.Messages
                .Include(m => m.Author)
                .Include(m => m.Group)
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.CreationDate)
                .ToListAsync();

                var f = await _dbContext.Messages
                    .Include(m => m.Author)
                    .Include(m => m.Group)
                    .Where(m => m.GroupId == groupId)
                    .GroupBy(m => new
                    {
                        id = 1,
                        day = m.CreationDate.Day
                    })
                    .Select(m => new
                    {
                        Date = m.Key.day,
                        Messages = m.ToList()
                    })
                    .ToListAsync();

                var messagesDto = new List<MessageDto>();
                if (!messages.Any())
                    return Ok(messagesDto);

                foreach (var message in messages)
                {
                    var authorDto = new UserDto(message.Author.UserName, message.Author.Email);
                    var messageDto = new MessageDto(message.Id, authorDto, groupId, message.CreationDate, message.Text);
                    messagesDto.Add(messageDto);
                }

                return Ok(messagesDto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("/getMessagesByDate")]
        public async Task<IActionResult> GetMessagesByDate([FromQuery] string groupId)
        {
            try
            {
                var dates = await _dbContext.Messages
                    .Include(m => m.Author)
                    .Include(m => m.Group)
                    .Where(m => m.GroupId == groupId)
                    .GroupBy(m => new
                    {
                        id = 1,
                        day = m.CreationDate.Date
                    })
                    .Skip(2)
                    .Take(2)
                    .Select(m => new
                    {
                        Date = m.Key.day,
                        Messages = m.ToList()
                    })
                    .ToListAsync();


                var messagesGroupedByDateDto = new List<MessageGroupByDateDto>();
                foreach (var date in dates)
                {
                    var messageGroupedByDateDto = new MessageGroupByDateDto
                    {
                        CreationDate = date.Date,
                        Messages = new List<MessageDto>()
                    };

                    foreach (var message in date.Messages)
                    {
                        var authorDto = new UserDto(message.Author.UserName, message.Author.Email);
                        var messageDto = new MessageDto(message.Id, authorDto, message.GroupId, message.CreationDate);
                        messageGroupedByDateDto.Messages.Add(messageDto);
                    }

                    messagesGroupedByDateDto.Add(messageGroupedByDateDto);
                }

                return Ok(messagesGroupedByDateDto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
