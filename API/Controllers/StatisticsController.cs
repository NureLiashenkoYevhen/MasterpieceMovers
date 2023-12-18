using API.Attributes;
using BLL.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
public class StatisticsController : Controller
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    
    [HttpGet("TransfersStartCountPerDayLastMonth")]
    public async Task<IActionResult> GetTransfersStartCountPerDayLastMonth()
    {
        var result = await _statisticsService.GetTransfersStartCountPerDayLastMonthAsync();
        
        return Ok(result);
    }

    [HttpGet("TransfersEndCountPerDayLastMonth")]
    public async Task<IActionResult> GetTransfersEndCountPerDayLastMonth()
    {
        var result = await _statisticsService.GetTransfersEndCountPerDayLastMonthAsync();
        
        return Ok(result);
    }
    
    [HttpGet("TransferCountLastWeek")]
    public async Task<IActionResult> GetShipmentCountLastWeek()
    {
        var result = await _statisticsService.GetTransfersLastWeekAsync();
        
        return Ok(result);
    }

    [HttpGet("AverageTransfersPerDay")]
    public async Task<IActionResult> GetAverageTransfersPerDay()
    {
        var result = await _statisticsService.GetAverageTransfersPerDayAsync();

        return Ok(result);
    }

    [HttpGet("UserCount")]
    public async Task<IActionResult> GetUserCount()
    {
        var result = await _statisticsService.GetUserCountAsync();

        return Ok(result);
    }

    [HttpGet("FinishedTransfersCount")]
    public async Task<IActionResult> GetFinishedTransfersCount()
    {
        var result = await _statisticsService.GetFinishedTransfersCountAsync();

        return Ok(result);
    }
}