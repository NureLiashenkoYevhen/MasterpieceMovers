using Core.DTO.Transfer;
using Core.Models;
using Core.Models.Errors;
using Core.Models.Transfer;
using DAL;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BLL.IoT
{
    public class IoTService : IIoTService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;

        public IoTService(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _apiUrl = configuration.GetSection("IoT:Url").Value;
            _httpClient = new HttpClient();
        }

        public async Task<IModel> GetCurrentTransferCondition(int id)
        {
            var transfer = await _applicationDbContext.Transfers.FirstOrDefaultAsync(x => x.Id == id);

            if (transfer is null)
            {
                return new ErrorModel
                {
                    Message = $"Transfer with id: {id} was not found."
                };
            }

            var response = await _httpClient.GetAsync($"{_apiUrl}/getCurrentTransferCondition?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                new ErrorModel
                {
                    Message = $"Failed to retrieve location from API. Status code: {response.StatusCode}"
                };
            }

            var content = await response.Content.ReadAsStringAsync();
            var conditionModel = JsonConvert.DeserializeObject<TransferConditionModel>(content);

            return conditionModel;
        }

        public async Task<IModel> GetCurrentTransferLocation(int id)
        {
            var transfer = await _applicationDbContext.Transfers.FirstOrDefaultAsync(s => s.Id == id);

            if (transfer is null)
            {
                return new ErrorModel
                {
                    Message = $"TRansfer with id: {id} was not found."
                };
            }

            var response = await _httpClient.GetAsync($"{_apiUrl}/getCurrentShipmentLocation?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                return new ErrorModel
                {
                    Message = $"Failed to retrieve location from API. Status code: {response.StatusCode}"
                };
            }

            var content = await response.Content.ReadAsStringAsync();
            var conditionDto = JsonConvert.DeserializeObject<TransferLocationModel>(content);

            return conditionDto;
        }
    }
}
