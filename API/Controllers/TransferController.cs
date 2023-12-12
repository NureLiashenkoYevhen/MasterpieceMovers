using System.Security.Claims;
using BLL.IoT;
using BLL.Transfers;
using Core.Models.Errors;
using Core.Models.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("Api/[controller]")]
public class TransferController : Controller
{
    private readonly ITransferService _transferService;
    private readonly IIoTService _iotService;

    public TransferController(ITransferService transferService, IIoTService iotService)
    {
        _transferService = transferService;
        _iotService = iotService;
    }

    [HttpGet("GetCurrentTransferCondition/{transferId}")]
    public async Task<IActionResult> GetCurrentTransferCondition(int transferId)
    {
        var result = await _iotService.GetCurrentTransferCondition(transferId);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }
    
    [HttpGet("GetCurrentTransferLocation/{transferId}")]
    public async Task<IActionResult> GetCurrentTransferLocation(int transferId)
    {
        var result = await _iotService.GetCurrentTransferLocation(transferId);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransfers()
    {
        var result = await _transferService.GetAllTransfersAsync();

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransferById(int id)
    {
        var result = await _transferService.GetTransferByIdAsync(id);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferModel transfer)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _transferService.CreateTransferAsync(userId, transfer);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok("Transfer was successfully created.");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransfer(int id, [FromBody] UpdateTransferModel transfer)
    {
        if (id != transfer.Id)
        {
            return BadRequest("Invalid input Id");
        }

        var result = await _transferService.UpdateTransferAsync(transfer);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipment(int id)
    {
        var result = await _transferService.DeleteTransferAsync(id);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}