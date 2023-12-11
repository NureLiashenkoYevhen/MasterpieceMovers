using API.Attributes;
using BLL.Analysises;
using Core.Models.Analysis;
using Core.Models.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnalysisById(int id)
    {
        var result = await _analysisService.GetAnalysisByIdAsync(id);

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

        return Ok(result);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateAnalysis(int id, [FromBody] AnalysisModel analysisModel)
    {
        var result = await _analysisService.CreateAnalysisAsync(id, analysisModel);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnalysis(int id, [FromBody] AnalysisModel analysisModel)
    {
        var result = await _analysisService.UpdateAnalysisAsync(id, analysisModel);

        if (result is ErrorModel)
        {
            var errorResult = result as ErrorModel;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnalysis(int id)
    {
        var result = await _analysisService.DeleteAnalysisAsync(id);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}