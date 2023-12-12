using Core.Entities;
using Core.Models;
using Core.Models.Analysis;
using Core.Models.Errors;
using DAL;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BLL.Analysises
{
    public class AnalysisService : IAnalysisService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AnalysisService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IModel> CreateAnalysisAsync(int transferId, AnalysisModel analyticModel)
        {
            var transfer = await _applicationDbContext.Transfers.FirstOrDefaultAsync(s => s.Id == transferId);

            if (transfer is null)
            {
                return new ErrorModel
                {
                    Message = $"Transfer with id: {transferId} was not found."
                };
            }

            var analysis = new Analysis()
            {
                Metric = analyticModel.Metrics,
                Value = analyticModel.Value,
                Timestamp = analyticModel.TimeSpan,
                Transfer = transfer,
            };
            _applicationDbContext.Analysises.Add(analysis);
            await _applicationDbContext.SaveChangesAsync();

            return analyticModel;
        }

        public async Task<Result> DeleteAnalysisAsync(int analysisId)
        {
            var analysis = _applicationDbContext.Analysises.FirstOrDefaultAsync(a => a.Id == analysisId);

            if (analysis is null) 
            {
                return Result.Fail($"Analysis with id: {analysisId} was not found.");
            }

            _applicationDbContext.Remove(analysis);
            await _applicationDbContext.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<List<AnalysisModel>> GetAllAnalysisesAsync()
        {
            var analysises = await _applicationDbContext.Analysises.ToListAsync();

            return analysises.Select(a => new AnalysisModel
            {
                Metrics = a.Metric,
                Value  = a.Value,
                TimeSpan = a.Timestamp
            }).ToList();
        }

        public async Task<IModel> GetAnalysisByIdAsync(int analysisId)
        {
            var analysis = await _applicationDbContext.Analysises.FirstOrDefaultAsync(a => a.Id == analysisId);

            if (analysis is null)
            {
                return new ErrorModel
                {
                    Message = $"No analysises with id: {analysisId} was not found."
                };
            }

            return new AnalysisModel 
            { 
                Metrics = analysis.Metric,
                Value = analysis.Value,
                TimeSpan = analysis.Timestamp
            };
        }

        public async Task<IModel> UpdateAnalysisAsync(int analysisId, AnalysisModel analysisModel)
        {
            var dbAnalysis = await _applicationDbContext.Analysises.FirstOrDefaultAsync(a => a.Id == analysisId);

            if (dbAnalysis is null)
            {
                return new ErrorModel
                {
                    Message = $"Analysis with such id: {analysisId} was not found."
                };
            }

            dbAnalysis.Metric = analysisModel.Metrics;
            dbAnalysis.Value = analysisModel.Value;
            dbAnalysis.Timestamp = analysisModel.TimeSpan;

            await _applicationDbContext.SaveChangesAsync();

            return analysisModel;
        }
    }
}
