namespace WebApp.BFF.Core.Models
{
    public class UserGroup
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }

    }
}
