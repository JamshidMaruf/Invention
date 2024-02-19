using Invention.Emums;
using Invention.Models.Markets;
using Invention.Models.Products;
using Invention.Models.Suppliers;

namespace Invention.Models.SuppliedProducts;

public class WarehouseOperationViewModel
{
    public ProductViewModel Product { get; set; }
    public SupplierViewModel Supplier { get; set; }
    public MarketViewModel Market { get; set; }
    public DateTime Time { get; set; }
    public WarehouseOperationType Type { get; set; }
}