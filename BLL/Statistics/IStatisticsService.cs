using Core.Models.Transfer;

namespace BLL.Statistics
{
    public interface IStatisticsService
    {
        Task<List<DayTransfersModel>> GetTransfersStartCountPerDayLastMonthAsync();

        Task<List<DayTransfersModel>> GetTransfersEndCountPerDayLastMonthAsync();

        Task<int> GetTransfersLastWeekAsync();

        Task<double> GetAverageTransfersPerDayAsync();

        Task<int> GetUserCountAsync();

        Task<int> GetFinishedTransfersCountAsync();
    }
}
