namespace Invention.Models.SuppliedProducts;

public class WarehouseOperationRemoveModel
{
    public long ProductId { get; set; }
    public long MarketId { get; set; }
    public double Quantity { get; set; }
}