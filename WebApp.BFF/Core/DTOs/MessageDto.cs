using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Core.DTOs
{
    public class MessageDto
    {
        public string Id { get; set; }
        public UserDto Author { get; set; }
        public string GroupId { get; set; }
        public string? Text { get; set; }
        public DateTime CreationDate { get; set; }

        internal MessageDto()
        {

        }
        public MessageDto(string id, UserDto author, string groupId, DateTime creationDate, string? text = null)
        {
            Id = id;
            Author = author;
            GroupId = groupId;
            CreationDate = creationDate;
            Text = text;
        }
    }


    public class MessageGroupByDateDto
    {
        public DateTime CreationDate { get; set; }
        public List<MessageDto> Messages { get; set; }

        internal MessageGroupByDateDto()
        {
            Messages = new List<MessageDto>();
        }

        public MessageGroupByDateDto(DateTime creationDate, List<MessageDto> messages)
        {
            CreationDate = creationDate;
            Messages = messages;
        }
    }
}
