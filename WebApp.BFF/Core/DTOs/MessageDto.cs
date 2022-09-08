using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Core.DTOs
{
    public class MessageDto
    {
        public string Id { get; set; }
        public UserDto Author { get; set; }
        public string GroupId { get; set; }
        public string? Text { get; set; }

        internal MessageDto()
        {

        }
        public MessageDto(string id, UserDto author, string groupId, string? text = null)
        {
            Id = id;
            Author = author;
            GroupId = groupId;
            Text = text;
        }
    }
}
