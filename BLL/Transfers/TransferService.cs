﻿using Core.Entities;
using Core.Enums;
using Core.Models;
using Core.Models.Errors;
using Core.Models.Transfer;
using DAL;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BLL.Transfers
{
    public class TransferService : ITransferService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TransferService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IModel> CreateTransferAsync(int userId, CreateTransferModel transfer)
        {
            var startLocation = _applicationDbContext.Locations.Add(new Location()
            {
                Latitude = transfer.StartingLocationLatitude,
                Longitude = transfer.StartingLocationLongitude,
            });

            var endLocation = _applicationDbContext.Locations.Add(new Location()
            {
                Latitude = transfer.EndingLocationLatitude,
                Longitude = transfer.EndingLocationLongitude,
            });

            var transferCondition = _applicationDbContext.TransferConditions.Add(new TransferCondition()
            {
                MinTemperature = transfer.MinTemperature,
                MaxTemperature = transfer.MaxTemperature,
                MinHumidity = transfer.MinHumidity,
                MaxHumidity = transfer.MaxHumidity,
            });

            var dbUser = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (dbUser is null)
            {
                return new ErrorModel
                {
                    Message = $"No user with id: {userId} was found"
                };
            }

            _applicationDbContext.Transfers.Add(new Transfer
            {
                StartingDate = transfer.StartingDate,
                StartingLocation = startLocation.Entity,
                EndingDate = transfer.EndingDate,
                EndingLocation = endLocation.Entity,
                TransferCondition = transferCondition.Entity,
                TransferStatus = TransferStatus.Pending,
                User = dbUser
            });

            await _applicationDbContext.SaveChangesAsync();

            return transfer;
        }

        public async Task<List<TransferPassModel>> GetAllTransfersAsync()
        {
            var transfers = await _applicationDbContext.Transfers.ToListAsync();

            var returnList = new List<TransferPassModel>();

            foreach (var t in transfers)
            {
                returnList.Add(new TransferPassModel
                {
                    Id = t.Id,
                    StartingLocation = PassLocationModel(await _applicationDbContext.Locations.FindAsync(t.StartingLocationId)),
                    EndingLocation = PassLocationModel(await _applicationDbContext.Locations.FindAsync(t.EndingLocationId)),
                    Condition = PassConditionModel(await _applicationDbContext.TransferConditions
                        .FirstOrDefaultAsync(x => x.TransferId == t.Id)),
                });
            }

            return returnList;
        }

        public async Task<List<Transfer>> GetTransfersByStatusAsync(TransferStatus status)
        {
            return await _applicationDbContext.Transfers.Where(t => t.TransferStatus == status).ToListAsync();
        }

        public async Task<IModel> GetTransferByIdAsync(int id)
        {
            var transfer = await _applicationDbContext.Transfers.FirstOrDefaultAsync(t => t.Id == id);

            if (transfer is null)
            {
                return new ErrorModel
                {
                    Message = $"Transfer with id: {id} was not found"
                };
            }

            return new TransferPassModel
            {
                Id = transfer.Id,
                StartingLocation = transfer.StartingLocation,
                EndingLocation = transfer.EndingLocation,
                Condition = transfer.TransferCondition
            };
        }

        public async Task<IModel> UpdateTransferAsync(UpdateTransferModel transfer)
        {
            var dbTransfer = await _applicationDbContext.Transfers.FirstOrDefaultAsync(t => t.Id == transfer.Id);

            if (dbTransfer is null)
            {
                return new ErrorModel
                {
                    Message = $"Transfer with id: {transfer.Id} was not found"
                };
            }

            dbTransfer.StartingLocation = await _applicationDbContext.Locations.FirstOrDefaultAsync(l => l.Id == dbTransfer.StartingLocationId);
            dbTransfer.EndingLocation = await _applicationDbContext.Locations.FirstOrDefaultAsync(l => l.Id == dbTransfer.EndingLocationId);
            dbTransfer.TransferCondition = await _applicationDbContext.TransferConditions.FirstOrDefaultAsync(c => c.TransferId == dbTransfer.Id);

            dbTransfer.StartingDate = transfer.StartingDate;
            dbTransfer.StartingLocation.Latitude = transfer.StartingLocationLatitude;
            dbTransfer.StartingLocation.Longitude = transfer.StartingLocationLongitude;
            dbTransfer.EndingDate = transfer.EndingDate;
            dbTransfer.EndingLocation.Latitude = transfer.EndingLocationLatitude;
            dbTransfer.EndingLocation.Longitude = transfer.EndingLocationLongitude;
            dbTransfer.TransferCondition.MinTemperature = transfer.MinTemperature;
            dbTransfer.TransferCondition.MaxTemperature = transfer.MaxTemperature;
            dbTransfer.TransferCondition.MinHumidity = transfer.MinHumidity;
            dbTransfer.TransferCondition.MaxHumidity = transfer.MaxHumidity;
            dbTransfer.TransferStatus = transfer.TransferStatus;

            await _applicationDbContext.SaveChangesAsync();

            return transfer;    
        }

        public async Task<Result> DeleteTransferAsync(int id)
        {
            var dbTransfer = await _applicationDbContext.Transfers.FirstOrDefaultAsync(t => t.Id == id);

            if (dbTransfer is null)
            {
                return Result.Fail("Transfer was not found");
            }

            _applicationDbContext.Transfers.Remove(dbTransfer);
            await _applicationDbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private Location PassLocationModel(Location location)
        {
            var toReturn = new Location
            {
                Id = location.Id,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            return toReturn;
        }

        private TransferCondition PassConditionModel(TransferCondition condition)
        {
            var toReturn = new TransferCondition()
            {
                Id = condition.Id,
                MinHumidity = condition.MinHumidity,
                MaxHumidity = condition.MaxHumidity,
                MaxTemperature = condition.MaxTemperature,
                MinTemperature = condition.MinTemperature,
                TransferId = condition.TransferId
            };

            return toReturn;
        }
    }
}
