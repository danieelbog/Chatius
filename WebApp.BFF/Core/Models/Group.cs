namespace WebApp.BFF.Core.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public virtual List<ApplicationUser> ApplicationUsers { get; set; }


        public Group()
        {
            Messages = new List<Message>();
            ApplicationUsers = new List<ApplicationUser>();
        }
    }
}
