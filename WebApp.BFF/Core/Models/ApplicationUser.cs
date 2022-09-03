using Microsoft.AspNetCore.Identity;

namespace WebApp.BFF.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Connection> Connections { get; set; }
        public List<Message> Messages { get; set; }
        public List<UserGroup> UserGroups { get; set; }

        public ApplicationUser()
        {
            Connections = new List<Connection>();
            Messages = new List<Message>();
            UserGroups = new List<UserGroup>();

        }
    }
}
