using Microsoft.AspNetCore.Identity;

namespace WebApp.BFF.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Connection> Connections { get; set; }

        public ApplicationUser()
        {
            Connections = new List<Connection>();
        }
    }
}
