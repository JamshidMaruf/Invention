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
    private readonly MarketService marketService;
    private readonly ProductService productService;
    private readonly SupplierService supplierService;
    private List<WarehouseOperation> warehouseOperations;
    public WarehouseOperationService(
        MarketService marketService,
        ProductService productService,
        SupplierService supplierService)
    {
        this.marketService = marketService;
        this.productService = productService;
        this.supplierService = supplierService;
    }

    public async ValueTask<WarehouseOperationViewModel> AddAsync(WarehouseOperationAddModel model)
    {
        await productService.GetByIdAsync(model.ProductId);
        await supplierService.GetByIdAsync(model.SupplierId);

        warehouseOperations = await FileIO.ReadAsync<WarehouseOperation>(Constants.WarehouseOperationPath);

        var createdWarehouseOperation = warehouseOperations.Create(model.MapTo<WarehouseOperation>());
        createdWarehouseOperation.Time = DateTime.UtcNow;
        createdWarehouseOperation.Type = Emums.WarehouseOperationType.Plus;
        await FileIO.WriteAsync(Constants.WarehouseOperationPath, warehouseOperations);

        return createdWarehouseOperation.MapTo<WarehouseOperationViewModel>();
    }

    public async ValueTask<WarehouseOperationViewModel> RemoveAsync(WarehouseOperationRemoveModel model)
    {
        await marketService.GetByIdAsync(model.MarketId);
        await productService.GetByIdAsync(model.ProductId);
        
        warehouseOperations = await FileIO.ReadAsync<WarehouseOperation>(Constants.WarehouseOperationPath);

        var removedWarehouseOperation = warehouseOperations.Create(model.MapTo<WarehouseOperation>());
        removedWarehouseOperation.Time = DateTime.UtcNow;
        removedWarehouseOperation.Type = Emums.WarehouseOperationType.Minus;
        await FileIO.WriteAsync(Constants.WarehouseOperationPath, warehouseOperations);

        return removedWarehouseOperation.MapTo<WarehouseOperationViewModel>();
    }

    public async ValueTask<IEnumerable<WarehouseOperationViewModel>> GetAllAsync(Filter filter = null)
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
                    var yesterday = dateNow.AddDays(-1);
                    warehouseOperations = warehouseOperations
                        .Where(wo => wo.Time.Year == dateNow.Year &&
                               wo.Time.Month == dateNow.Month &&
                               wo.Time.DayOfWeek == yesterday.DayOfWeek)
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