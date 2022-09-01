using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.BFF.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task ConnectUser(string username)
        {
            await Clients.All.SendAsync("userConnected", username);
        }

        public async Task DisconnectUser(string username)
        {
            await Clients.All.SendAsync("userDisconnected", username);
        }

        public async Task SendChatMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        public Task ReceiveMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
