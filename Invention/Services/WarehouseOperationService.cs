using Invention.Configurations;
using Invention.Emums;
using Invention.Exceptions;
using Invention.Extensions;
using Invention.Helpers;
using Invention.Interfaces;
using Invention.Models.Commons;
using Invention.Models.SuppliedProducts;

namespace Invention.Services;

public class WarehouseOperationService : IWarehouseOperationService
{
    private List<WarehouseOperation> warehouseOperations;

    public async ValueTask<WarehouseOperationViewModel> AddAsync(WarehouseOperationAddModel model)
    {
        warehouseOperations = await FileIO.ReadAsync<WarehouseOperation>(Constants.WarehouseOperationPath);

        var createdWarehouseOperation = warehouseOperations.Create(model.MapTo<WarehouseOperation>());
        createdWarehouseOperation.Time = DateTime.UtcNow;
        createdWarehouseOperation.Type = Emums.WarehouseOperationType.Plus;
        await FileIO.WriteAsync(Constants.WarehouseOperationPath, warehouseOperations);

        return createdWarehouseOperation.MapTo<WarehouseOperationViewModel>();
    }

    public async ValueTask<WarehouseOperationViewModel> RemoveAsync(WarehouseOperationRemoveModel model)
    {
        warehouseOperations = await FileIO.ReadAsync<WarehouseOperation>(Constants.WarehouseOperationPath);

        var removedWarehouseOperation = warehouseOperations.Create(model.MapTo<WarehouseOperation>());
        removedWarehouseOperation.Time = DateTime.UtcNow;
        removedWarehouseOperation.Type = Emums.WarehouseOperationType.Minus;
        await FileIO.WriteAsync(Constants.WarehouseOperationPath, warehouseOperations);

        return removedWarehouseOperation.MapTo<WarehouseOperationViewModel>();
    }

    public async ValueTask<IEnumerable<WarehouseOperationViewModel>> GetAllAsync(Filter filter)
    {
        warehouseOperations = await FileIO.ReadAsync<WarehouseOperation>(Constants.WarehouseOperationPath);
        
        if(filter != null && filter.SupplierId != null)
        {
            warehouseOperations = warehouseOperations.Where(wo => wo.SupplierId == filter.SupplierId).ToList();
        }

        if (filter != null && filter.MarketId != null)
        {
            warehouseOperations = warehouseOperations.Where(wo => wo.MarketId == filter.MarketId).ToList();
        }

        if (filter != null && filter.ProductId != null)
        {
            warehouseOperations = warehouseOperations.Where(wo => wo.ProductId == filter.ProductId).ToList();
        }

        if (filter != null && filter.Date != null)
        {
            var dateNow = DateTime.UtcNow;
            switch (filter.Date)
            {
                case DateFilter.ThisYear:
                    warehouseOperations = warehouseOperations.Where(wo => wo.Time.Year == dateNow.Year).ToList();
                    break;
                case DateFilter.ThisMonth:
                    warehouseOperations = warehouseOperations
                        .Where(wo => wo.Time.Year == dateNow.Year && wo.Time.Month == dateNow.Month)
                        .ToList();
                    break;
                case DateFilter.ThisWeek:
                    warehouseOperations = warehouseOperations
                        .Where(wo => wo.Time.Year == dateNow.Year &&
                               wo.Time.Month == dateNow.Month &&
                               wo.Time.DayOfWeek >= DayOfWeek.Monday &&
                               wo.Time.DayOfWeek <= dateNow.DayOfWeek)
                        .ToList();
                    break;
                case DateFilter.ThisDay:
                    warehouseOperations = warehouseOperations
                        .Where(wo => wo.Time.Year == dateNow.Year &&
                               wo.Time.Month == dateNow.Month &&
                               wo.Time.DayOfWeek == dateNow.DayOfWeek)
                        .ToList();
                    break;
                case DateFilter.Yesterday:
                    warehouseOperations = warehouseOperations
                        .Where(wo => wo.Time.Year == dateNow.Year &&
                               wo.Time.Month == dateNow.Month &&
                               wo.Time.DayOfWeek == dateNow.DayOfWeek - 1)
                        .ToList();
                    break;
            }
        }

        return warehouseOperations.MapTo<WarehouseOperationViewModel>();
    }

    public async ValueTask<WarehouseOperationViewModel> GetByIdAsync(long id)
    {
        warehouseOperations = await FileIO.ReadAsync<WarehouseOperation>(Constants.WarehouseOperationPath);
        var existWarehouseOperation = warehouseOperations.FirstOrDefault(wo => wo.Id == id && !wo.IsDeleted)
            ?? throw new NotFoundException<WarehouseOperation>();

        return existWarehouseOperation.MapTo<WarehouseOperationViewModel>();
    }
}