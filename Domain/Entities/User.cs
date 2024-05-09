using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHashed { get; set; }

        public string PasswordSalt { get; set; }

        public RoleEnum Role { get; set; }

        public List<Transfer> Shipments { get; set; }

        public List<Notification> Notifications { get; set; }
    }
}
