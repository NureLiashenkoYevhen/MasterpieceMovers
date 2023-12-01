namespace Core.Models.Notification
{
    public class NotificationModel : IModel
    {
        public string Message { get; set; }

        public bool IsRead { get; set; }
    }
}
