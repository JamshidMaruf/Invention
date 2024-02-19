using Invention.Emums;

namespace Invention.Models.SuppliedProducts;

public class WarehouseOperation : Auditable
{
    public long ProductId { get; set; }
    public long? SupplierId { get; set; }
    public long? MarketId { get; set; }
    public DateTime Time { get; set; }
    public WarehouseOperationType Type { get; set; }
}