namespace WebApp.BFF.Core.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public List<UserGroup> UserGroups { get; set; }


        public Group()
        {
            Messages = new List<Message>();
            UserGroups = new List<UserGroup>();
        }
    }
}
