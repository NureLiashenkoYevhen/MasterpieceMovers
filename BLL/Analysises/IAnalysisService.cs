using Core.Models;
using Core.Models.Analysis;
using FluentResults;

namespace BLL.Analysises
{
    public interface IAnalysisService
    {
        Task<IModel> GetAnalysisByIdAsync(int analyticId);

        Task<List<AnalysisModel>> GetAllAnalysisesAsync();

        Task<IModel> CreateAnalysisAsync(int shipmentId, AnalysisModel analyticDto);

        Task<IModel> UpdateAnalysisAsync(int analyticId, AnalysisModel analyticDto);

        Task<Result> DeleteAnalysisAsync(int analyticId);
    }
}
