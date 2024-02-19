using Invention.Models.Commons;
using Invention.Models.SuppliedProducts;

namespace Invention.Interfaces;

public interface IWarehouseOperationService
{
    /// <summary>
    /// Supplier brings product to the warehouse
    /// </summary>
    /// <param name="suppliedProduct"></param>
    /// <returns></returns>
    ValueTask<WarehouseOperationViewModel> AddAsync(WarehouseOperationAddModel model);

    /// <summary>
    /// Remove product from the warehouse
    /// </summary>
    /// <param name="suppliedProduct"></param>
    /// <returns></returns>
    ValueTask<WarehouseOperationViewModel> RemoveAsync(WarehouseOperationRemoveModel model);

    /// <summary>
    /// Get exist supplied product via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<WarehouseOperationViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Get list of warehouse operations via filter, 
    /// </summary>
    /// <param name="supplierId"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<WarehouseOperationViewModel>> GetAllAsync(Filter filter);
}