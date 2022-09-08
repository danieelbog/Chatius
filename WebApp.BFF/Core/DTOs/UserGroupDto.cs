using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Core.DTOs
{
    public class UserGroupDto
    {
        public string ApplicationUserId { get; set; }
        public UserDto ApplicationUser { get; set; }
        public string GroupId { get; set; }
        public GroupDto Group { get; set; }


        internal UserGroupDto()
        {
        }
        public UserGroupDto(string applicationUserId, UserDto applicationUser, string groupId, GroupDto group)
        {
            ApplicationUserId = applicationUserId;
            ApplicationUser = applicationUser;
            GroupId = groupId;
            Group = group;
        }
    }
}
