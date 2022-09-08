using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace WebApp.BFF.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Connection> Connections { get; set; }
        public List<Message> Messages { get; set; }
        public virtual List<Group> Groups { get; set; }

        public ApplicationUser()
        {
            Connections = new List<Connection>();
            Messages = new List<Message>();
            Groups = new List<Group>();

        }
    }
}
