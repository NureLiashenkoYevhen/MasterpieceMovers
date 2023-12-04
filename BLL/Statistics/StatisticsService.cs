using Core.Enums;
using Core.Models.Transfer;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BLL.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StatisticsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<double> GetAverageTransfersPerDayAsync()
        {
            var startingDate = await _applicationDbContext.Transfers.MinAsync(s => s.StartingDate);
            var endingDate = DateTime.Today;
            var totalDays = (endingDate - startingDate).TotalDays;

            if (totalDays <= 0)
            {
                return -1;
            }


            var averageShipmentsPerDay = await _applicationDbContext.Transfers.CountAsync() / totalDays;

            return averageShipmentsPerDay;
        }

        public async Task<int> GetFinishedTransfersCountAsync()
        {
            var finishedTransfers = await _applicationDbContext.Transfers.CountAsync(s => s.TransferStatus == TransferStatus.Delivered);

            return finishedTransfers;
        }

        public async Task<List<DayTransfersModel>> GetTransfersEndCountPerDayLastMonthAsync()
        {
            var lastMonthStartDate = DateTime.Today.AddMonths(-1);
            var shipmentsCountPerDay = await _applicationDbContext.Transfers
                .Where(s => s.EndingDate >= lastMonthStartDate)
                .GroupBy(s => s.EndingDate)
                .Select(group => new DayTransfersModel
                {
                    Date = group.Key,
                    TransferCount = group.Count()
                })
                .OrderBy(dto => dto.Date)
                .ToListAsync();

            return shipmentsCountPerDay;
        }

        public async Task<int> GetTransfersLastWeekAsync()
        {
            var lastWeekStartDate = DateTime.Today.AddDays(-7);
            var shipmentCount = await _applicationDbContext.Transfers.CountAsync(s => s.StartingDate >= lastWeekStartDate);

            return shipmentCount;
        }

        public async Task<List<DayTransfersModel>> GetTransfersStartCountPerDayLastMonthAsync()
        {
            var monthStartDate = DateTime.Today.AddMonths(-1);
            var shipmentsCountPerDay = await _applicationDbContext.Transfers
                                                            .Where(s => s.StartingDate >= monthStartDate)
                                                            .GroupBy(s => s.StartingDate.Date)
                                                            .Select(group => new DayTransfersModel
                                                            {
                                                                Date = group.Key,
                                                                TransferCount = group.Count()
                                                            })
                                                            .OrderBy(model => model.Date)
                                                            .ToListAsync();

            return shipmentsCountPerDay;
        }

        public async Task<int> GetUserCountAsync() 
        {
            var users = await _applicationDbContext.Users.CountAsync();

            return users;
        }
    }
}
