using Core.Models.Notification;
using FluentResults;

namespace BLL.Notifications
{
    public interface INotificationService
    {
        Task<NotificationModel> GetNotificationByIdAsync(int notificationId);

        Task<List<NotificationSuccessModel>> GetAllNotificationsAsync();

        Task<NotificationModel> CreateNotificationAsync(int userId, NotificationSuccessModel notificationDto);

        Task<NotificationModel> UpdateNotificationAsync(int notificationId, NotificationSuccessModel notificationDto);

        Task<Result> DeleteNotificationAsync(int notificationId);
    }
}
