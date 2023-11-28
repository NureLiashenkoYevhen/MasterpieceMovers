using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public bool isRead { get; set; }

        public User User { get; set; }
    }
}
