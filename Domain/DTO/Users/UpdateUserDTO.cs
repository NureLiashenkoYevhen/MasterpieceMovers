using Domain.Enums;

namespace Domain.DTO.Users
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public RoleEnum Role { get; set; }
    }
}
