using System.Text.Json.Serialization;

namespace WebApp.BFF.Core.Models
{
    public class Connection
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsConnected { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
