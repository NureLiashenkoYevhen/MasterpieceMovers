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

        public Task<IModel> CreateAnalysisAsync(int transferId, AnalysisModel analysisModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeleteAnalysisAsync(int id)
        {
            var analysis = _applicationDbContext.Analysises.FirstOrDefaultAsync(a => a.Id == id);

            if (analysis is null) 
            {
                return Result.Fail($"Analysis with id: {id} was not found.");
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

        public async Task<IModel> GetAnalysisByIdAsync(int id)
        {
            var analysis = await _applicationDbContext.Analysises.FirstOrDefaultAsync(a => a.Id == id);

            if (analysis is null)
            {
                return new ErrorModel
                {
                    Message = $"No analysises with id: {id} was not found."
                };
            }

            return new AnalysisModel 
            { 
                Metrics = analysis.Metric,
                Value = analysis.Value,
                TimeSpan = analysis.Timestamp
            };
        }

        public async Task<IModel> UpdateAnalysisAsync(int id, AnalysisModel analysisModel)
        {
            var dbAnalysis = await _applicationDbContext.Analysises.FirstOrDefaultAsync(a => a.Id == id);

            if (dbAnalysis is null)
            {
                return new ErrorModel
                {
                    Message = $"Analysis with such id: {id} was not found."
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
