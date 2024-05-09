using System.Security.Claims;
using BLL.Notifications;
using Core.Models.Errors;
using Core.Models.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id)
    {
        var result = await _notificationService.GetNotificationByIdAsync(id);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var result = await _notificationService.GetAllNotificationsAsync();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationModel notificationModel)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _notificationService.CreateNotificationAsync(userId, notificationModel);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationModel notificationModel)
    {
        var result = await _notificationService.UpdateNotificationAsync(id, notificationModel);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var result = await _notificationService.DeleteNotificationAsync(id);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}