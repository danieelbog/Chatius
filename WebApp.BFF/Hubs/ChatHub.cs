using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.BFF.Core.DTOs;
using WebApp.BFF.Core.Models;
using WebApp.BFF.Database;

namespace WebApp.BFF.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private ChatiusContext _dbContext;
        public ChatHub(ChatiusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var name = Context?.User?.Identity?.Name;
                var user = await _dbContext.ApplcationUsers
                    .Include(u => u.Connections)
                    .SingleOrDefaultAsync(u => u.UserName == name);

                if (user == null)
                    throw new Exception("There is no such user! How did you get here anyways??? :)");

                var connection = new Connection()
                {
                    Id = Context.ConnectionId,
                    ApplicationUserId = user.Id,
                    ApplicationUser = user,
                    IsConnected = true
                };

                user.Connections.Add(connection);
                _dbContext.SaveChanges();

                await Clients.All.SendAsync("userConnected", user.UserName);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    throw new Exception("There is no such userId! How did you get here anyways??? :)");

                var connection = await _dbContext.Connections.FindAsync(keyValues: new object[] { Context.ConnectionId, userId });
                if (connection == null)
                    throw new Exception("There is no such connection! How did you get here anyways??? :)");

                connection.IsConnected = false;
                _dbContext.SaveChanges();
                await Clients.All.SendAsync("userDisconnected", Context.User.Identity.Name);
            }
            catch (Exception ex)
            {
                return;
            }

        }

        public async Task AddToGroup(string userName)
        {
            try
            {
                if (userName == Context.User.Identity.Name || string.IsNullOrEmpty(userName))
                    return;

                var groupName = $"{userName} + {Context.User.Identity.Name}";
                var reverseGroupName = $"{Context.User.Identity.Name} + {userName}";
                var existingGroup = await _dbContext.Groups
                    .Include(g => g.ApplicationUsers)
                    .Where(g => g.Name == groupName)
                    .FirstOrDefaultAsync();
                if (existingGroup == null)
                    existingGroup = await _dbContext.Groups
                        .Include(g => g.ApplicationUsers)
                        .Where(g => g.Name == reverseGroupName)
                        .FirstOrDefaultAsync();

                var groupDto = new GroupDto();
                if (existingGroup != null)
                {
                    groupDto.Id = existingGroup.Id;
                    groupDto.Name = existingGroup.Name;

                    foreach (var applicationUser in existingGroup.ApplicationUsers)
                    {
                        var applicationUserDto = new UserDto(applicationUser.UserName, applicationUser.Email);
                        groupDto.Members.Add(applicationUserDto);
                    }

                    await Clients.Users(existingGroup.ApplicationUsers.Select(ug => ug.Id).ToList()).SendAsync("addedToGroup", groupDto);
                    return;
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                if (user == null)
                    throw new Exception($"The user with {userName} was not found");

                var currentUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUserId == null)
                    throw new Exception("There is no such userId! How did you get here anyways??? :)");

                var currentUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == currentUserId);

                if (currentUser == null)
                    throw new Exception("There is no such user! How did you get here anyways??? :)");

                var group = new Group()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = groupName
                };

                user.Groups.Add(group);
                currentUser.Groups.Add(group);
                await _dbContext.SaveChangesAsync();               

                var userDto = new UserDto(user.UserName, user.Email);
                var currentUserDto = new UserDto(currentUser.UserName, currentUser.Email);

                groupDto.Id = group.Id;
                groupDto.Name = group.Name;
                groupDto.Members.Add(userDto);
                groupDto.Members.Add(currentUserDto);

                await Clients.Users(new List<string>() { currentUser.Id, user.Id }).SendAsync("addedToGroup", groupDto);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task UserRegistered(Exception? exception)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task SendMessageTo(MessageDto incomingMessageDto)
        {
            if (incomingMessageDto == null || string.IsNullOrEmpty(incomingMessageDto.GroupId) || string.IsNullOrEmpty(incomingMessageDto.Text))
                return;

            try
            {
                var group = await _dbContext.Groups
                    .Include(g => g.Messages)
                    .Include(g => g.ApplicationUsers)
                    .FirstOrDefaultAsync(g => g.Id == incomingMessageDto.GroupId);

                if (group == null)
                    return;

                var currentUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUserId == null)
                    throw new Exception("There is no such userId! How did you get here anyways??? :)");

                var currentUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == currentUserId);

                if (currentUser == null)
                    throw new Exception("There is no such user! How did you get here anyways??? :)");

                var message = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = currentUser,
                    AuthorId = currentUserId,
                    Group = group,
                    GroupId = incomingMessageDto.GroupId,
                    Text = incomingMessageDto.Text,
                    CreationDate = DateTime.Now
                };

                group.Messages.Add(message);
                currentUser.Messages.Add(message);
                await _dbContext.SaveChangesAsync();

                var userDto = new UserDto(currentUser.UserName, currentUser.Email);
                var messageDto = new MessageDto(message.Id, userDto, incomingMessageDto.GroupId, message.CreationDate, incomingMessageDto.Text);

                var ids = group.ApplicationUsers.Select(au => au.Id);
                await Clients.Users(ids).SendAsync("recievedMessage", messageDto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
