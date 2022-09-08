using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Core.DTOs
{
    public class GroupDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<UserDto> Members { get; set; }

        internal GroupDto()
        {
            Members = new List<UserDto>();
        }

        public GroupDto(string id, string name)
        {
            Id = id;
            Name = name;
            Members = new List<UserDto>();
        }

        public GroupDto(string id, string name, List<UserDto> members)
        {
            Id = id;
            Name = name;
            Members = members;
        }
    }
}
