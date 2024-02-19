using Invention.Helpers;
using Invention.Exceptions;
using Invention.Extensions;
using Invention.Interfaces;
using Invention.Configurations;
using Invention.Models.Suppliers;

namespace Invention.Services;

public class SupplierService : ISupplierService
{
    private List<Supplier> suppliers;
    public async ValueTask<SupplierViewModel> CreateAsync(SupplierCreationModel supplier)
    {
        suppliers = await FileIO.ReadAsync<Supplier>(Constants.SupplierPath);
        var existSupplier = suppliers.FirstOrDefault(c => c.Code == supplier.Code);

        // If model is exist and was deleted with this code, update this model 
        if (existSupplier != null && existSupplier.IsDeleted)
        {
            // Recover deleted model with this code
            return await UpdateAsync(existSupplier.Id, supplier.MapTo<SupplierUpdateModel>(), true);
        }

        // If model is exist, throw exception
        if (existSupplier is not null)
            throw new AlreadyExistException<Supplier>();

        var createdMarket = suppliers.Create(supplier.MapTo<Supplier>());
        await FileIO.WriteAsync(Constants.SupplierPath, suppliers);

        return createdMarket.MapTo<SupplierViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        suppliers = await FileIO.ReadAsync<Supplier>(Constants.SupplierPath);
        var existSupplier = suppliers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new NotFoundException<Supplier>();

        existSupplier.IsDeleted = true;
        existSupplier.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.SupplierPath, suppliers);

        return true;
    }

    public async ValueTask<IEnumerable<SupplierViewModel>> GetAllAsync()
    {
        suppliers = await FileIO.ReadAsync<Supplier>(Constants.SupplierPath);
        return suppliers.Where(supplier => !supplier.IsDeleted).MapTo<SupplierViewModel>();
    }

    public async ValueTask<SupplierViewModel> GetByIdAsync(long id)
    {
        suppliers = await FileIO.ReadAsync<Supplier>(Constants.SupplierPath);
        var existSupplier = suppliers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new NotFoundException<Supplier>();

        return existSupplier.MapTo<SupplierViewModel>();
    }

    public async ValueTask<SupplierViewModel> UpdateAsync(long id, SupplierUpdateModel supplier, bool isUsedDeleted = false)
    {
        suppliers = await FileIO.ReadAsync<Supplier>(Constants.SupplierPath);
        var existSupplier = new Supplier();

        if (isUsedDeleted)
        {
            existSupplier = suppliers.FirstOrDefault(c => c.Id == id);
            existSupplier.IsDeleted = false;
        }
        else
        {
            existSupplier = suppliers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
                ?? throw new NotFoundException<Supplier>();
        }

        existSupplier.Id = id;
        existSupplier.Code = supplier.Code;
        existSupplier.Name = supplier.Name;
        existSupplier.UpdatedAt = DateTime.UtcNow;
        existSupplier.ContactDetail = supplier.ContactDetail;

        await FileIO.WriteAsync(Constants.SupplierPath, suppliers);

        return existSupplier.MapTo<SupplierViewModel>();
    }
}
