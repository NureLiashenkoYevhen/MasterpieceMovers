using Core.Models;
using Core.Models.Notification;
using FluentResults;

namespace BLL.Notifications
{
    public interface INotificationService
    {
        Task<IModel> GetNotificationByIdAsync(int notificationId);

        Task<List<NotificationModel>> GetAllNotificationsAsync();

        Task<IModel> CreateNotificationAsync(int userId, NotificationModel notificationDto);

        Task<IModel> UpdateNotificationAsync(int notificationId, NotificationModel notificationDto);

        Task<Result> DeleteNotificationAsync(int notificationId);
    }
}
