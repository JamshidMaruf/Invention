using Invention.Emums;

namespace Invention.Models.SuppliedProducts;

public class WarehouseOperationAddModel
{
    public long ProductId { get; set; }
    public long SupplierId { get; set; }
    public double Quantity { get; set; }
}