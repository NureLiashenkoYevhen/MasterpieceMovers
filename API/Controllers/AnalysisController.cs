using API.Attributes;
using BLL.Analysises;
using Core.Models.Analysis;
using Core.Models.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
public class AnalysisController : Controller
{
    private readonly IAnalysisService _analysisService;

    public AnalysisController(IAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }
    
    [HttpGet("{analysisId}")]
    public async Task<IActionResult> GetAnalysisById(int analysisId)
    {
        var result = await _analysisService.GetAnalysisByIdAsync(analysisId);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnalysis()
    {
        var result = await _analysisService.GetAllAnalysisesAsync();
        
        //Added test comment
        return Ok(result);
    }

    [HttpPost("{transferId}")]
    public async Task<IActionResult> CreateAnalysis(int transferId, [FromBody] AnalysisModel analysisModel)
    {
        var result = await _analysisService.CreateAnalysisAsync(transferId, analysisModel);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpPut("{analysisId}")]
    public async Task<IActionResult> UpdateAnalysis(int analysisId, [FromBody] AnalysisModel analysisModel)
    {
        var result = await _analysisService.UpdateAnalysisAsync(analysisId, analysisModel);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{analysisId}")]
    public async Task<IActionResult> DeleteAnalysis(int analysisId)
    {
        var result = await _analysisService.DeleteAnalysisAsync(analysisId);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}