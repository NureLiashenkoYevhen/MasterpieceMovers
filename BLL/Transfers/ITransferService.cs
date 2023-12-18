using Core.Entities;
using Core.Enums;
using Core.Models;
using Core.Models.Transfer;
using FluentResults;

namespace BLL.Transfers
{
    public interface ITransferService
    {
        Task<List<TransferPassModel>> GetAllTransfersAsync();

        Task<IModel> GetTransferByIdAsync(int id);

        Task<IModel> CreateTransferAsync(int userId, CreateTransferModel transfer);

        Task<IModel> UpdateTransferAsync(UpdateTransferModel transfer);

        Task<Result> DeleteTransferAsync(int id);

        Task<List<Transfer>> GetTransfersByStatusAsync(TransferStatus status);
    }
}
