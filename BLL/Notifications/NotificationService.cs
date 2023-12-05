using Core.Entities;
using Core.Models;
using Core.Models.Errors;
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

        public async Task<IModel> CreateNotificationAsync(int userId, NotificationModel notificationModel)
        {
            var dbUser = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (dbUser is null)
            {
                return new ErrorModel
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

            return new NotificationModel
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

        public async Task<List<NotificationModel>> GetAllNotificationsAsync()
        {
            var notifications = await _applicationDbContext.Notifications.ToListAsync();

            return notifications.Select(n => new NotificationModel
            {
                Message = n.Message,
                IsRead = n.IsRead
            }).ToList();
        }

        public async Task<IModel> GetNotificationByIdAsync(int id)
        {
            var notification = await _applicationDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (notification is null)
            {
                return new ErrorModel
                {
                    Message = $"Notification with id: {id} not found."
                };
            }

            return new NotificationModel
            {
                Message = notification.Message,
                IsRead = notification.IsRead
            };
        }

        public async Task<IModel> UpdateNotificationAsync(int id, NotificationModel notificationModel)
        {
            var dbNotification = await _applicationDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (dbNotification is null)
            {
                return new ErrorModel
                {
                    Message = $"Notification with id {id} not found."
                };
            }

            dbNotification.Message = notificationModel.Message;
            dbNotification.IsRead = notificationModel.IsRead;

            await _applicationDbContext.SaveChangesAsync();

            return new NotificationModel
            {
                Message = dbNotification.Message,
                IsRead = dbNotification.IsRead
            };
        }
    }
}
