using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml.Linq;
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

                var connection = await _dbContext.Connections.FindAsync(keyValues: new object[] {Context.ConnectionId, userId});
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
    }
}
