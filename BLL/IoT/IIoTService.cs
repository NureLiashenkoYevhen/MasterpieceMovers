using Core.Models;

namespace BLL.IoT
{
    public interface IIoTService
    {
        Task<IModel> GetCurrentTransferLocation(int id);

        Task<IModel> GetCurrentTransferCondition(int id);
    }
}
