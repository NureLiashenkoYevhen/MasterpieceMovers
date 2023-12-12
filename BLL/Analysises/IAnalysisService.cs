using Core.Models;
using Core.Models.Analysis;
using FluentResults;

namespace BLL.Analysises
{
    public interface IAnalysisService
    {
        Task<IModel> GetAnalysisByIdAsync(int analysisId);

        Task<List<AnalysisModel>> GetAllAnalysisesAsync();

        Task<IModel> CreateAnalysisAsync(int transferId, AnalysisModel analyticModel);

        Task<IModel> UpdateAnalysisAsync(int analysisId, AnalysisModel analyticDto);

        Task<Result> DeleteAnalysisAsync(int analysisId);
    }
}
