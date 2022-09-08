using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Core.DTOs
{
    public class ConnectionDto
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsConnected { get; set; }
        public UserDto ApplicationUser { get; set; }

        internal ConnectionDto()
        {

        }

        public ConnectionDto(string id, string applicationUserId, bool isConnected, UserDto applicationUser)
        {
            Id = id;
            ApplicationUserId = applicationUserId;
            IsConnected = isConnected;
            ApplicationUser = applicationUser;
        }
    }
}
