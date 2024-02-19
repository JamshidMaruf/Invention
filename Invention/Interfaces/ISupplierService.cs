using Invention.Models.Suppliers;

namespace Invention.Interfaces;

public interface ISupplierService
{
    /// <summary>
    /// Create new supplier
    /// </summary>
    /// <param name="supplier"></param>
    /// <returns></returns>
    ValueTask<SupplierViewModel> CreateAsync(SupplierCreationModel supplier);

    /// <summary>
    /// Update information of exist supplier
    /// </summary>
    /// <param name="id"></param>
    /// <param name="supplier"></param>
    /// <returns></returns>
    ValueTask<SupplierViewModel> UpdateAsync(long id, SupplierUpdateModel supplier, bool isUsedDeleted = false);

    /// <summary>
    /// Delete exist supplier
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Get information of exist supplier via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<SupplierViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist suppliers
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<SupplierViewModel>> GetAllAsync();
}