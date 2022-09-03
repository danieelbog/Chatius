namespace WebApp.BFF.Core.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
    }
}
