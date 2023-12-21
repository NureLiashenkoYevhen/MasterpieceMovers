using IoTProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace IoTProvider.Controllers;

[ApiController]
[Route("/")]
public class DataController : ControllerBase
{
    // Статичні дані для значень координат та умов перевезеннь.
    public static float Latitude = 45.2205405f;
    public static float Longitude = 10.525633f;
    public static float Temperature = 21.0f;
    public static float Humidity = 50.0f;
        
    // Метод для отримання поточних умов перевезеннь.
    [HttpGet("/getCurrentTransferCondition")]
    public IActionResult GetCurrentTransferCondition(int id)
    {
        // Створення випадкових відхилень для температури та вологості.
        var random = new Random();
        var temperature = Temperature + (float)(random.NextDouble() * 0.1 - 0.05);
        var humidity = Humidity + (float)(random.NextDouble() * 0.1 - 0.05);

        // Повернення відповіді з об'єктом TransferConditionModel.
        return Ok(new TransferConditionModel
        {
            Temperature = temperature,
            Humidity = humidity
        });
    }
        
    // Метод для отримання поточного місцезнаходження перевезеннь.
    [HttpGet("/getCurrentTransferLocation")]
    public IActionResult GetCurrentTransferLocation(int id)
    {
        // Створення випадкових відхилень для широти та довготи.
        var random = new Random();
        var latitude = Latitude + (float)(random.NextDouble() * 0.1 - 0.05);
        var longitude = Longitude + (float)(random.NextDouble() * 0.1 - 0.05);

        // Повернення відповіді з об'єктом TransferLocation.
        return Ok(new TransferLocation 
        {
            Latitude = latitude,
            Longitude = longitude
        });
    }
}
