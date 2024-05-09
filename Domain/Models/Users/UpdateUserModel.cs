using Core.Enums;

namespace Core.DTO.Users
{
    public class UpdateUserModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public RoleEnum Role { get; set; }
    }
}
