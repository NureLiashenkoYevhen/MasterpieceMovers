using Core.Entities;
using Core.Models.Notification;
using DAL;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BLL.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NotificationService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<NotificationModel> CreateNotificationAsync(int userId, NotificationSuccessModel notificationModel)
        {
            var dbUser = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (dbUser is null)
            {
                return new NotificationErrorModel
                {
                    Message = $"User with id: {userId} was not found"
                };
            }

            var notification = new Notification()
            {
                Message = notificationModel.Message,
                IsRead = notificationModel.IsRead,
                User = dbUser
            };

            _applicationDbContext.Notifications.Add(notification);
            await _applicationDbContext.SaveChangesAsync();

            return new NotificationSuccessModel
            {
                Message = notification.Message,
                IsRead = notification.IsRead
            };
        }

        public async Task<Result> DeleteNotificationAsync(int id)
        {
            var notification = await _applicationDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (notification is null)
            {
                return Result.Fail($"Notification with id {id} not found.");
            }

            _applicationDbContext.Notifications.Remove(notification);
            await _applicationDbContext.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<List<NotificationSuccessModel>> GetAllNotificationsAsync()
        {
            var notifications = await _applicationDbContext.Notifications.ToListAsync();

            return notifications.Select(n => new NotificationSuccessModel
            {
                Message = n.Message,
                IsRead = n.IsRead
            }).ToList();
        }

        public async Task<NotificationModel> GetNotificationByIdAsync(int id)
        {
            var notification = await _applicationDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (notification is null)
            {
                return new NotificationErrorModel
                {
                    Message = $"Notification with id: {id} not found."
                };
            }

            return new NotificationSuccessModel
            {
                Message = notification.Message,
                IsRead = notification.IsRead
            };
        }

        public async Task<NotificationModel> UpdateNotificationAsync(int id, NotificationSuccessModel notificationModel)
        {
            var dbNotification = await _applicationDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (dbNotification is null)
            {
                return new NotificationErrorModel
                {
                    Message = $"Notification with id {id} not found."
                };
            }

            dbNotification.Message = notificationModel.Message;
            dbNotification.IsRead = notificationModel.IsRead;

            await _applicationDbContext.SaveChangesAsync();

            return new NotificationSuccessModel
            {
                Message = dbNotification.Message,
                IsRead = dbNotification.IsRead
            };
        }
    }
}
