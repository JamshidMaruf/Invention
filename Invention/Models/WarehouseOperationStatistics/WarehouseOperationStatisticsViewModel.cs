using Invention.Models.Products;

namespace Invention.Models.WarehouseOperationStatistics;

public class WarehouseOperationStatisticsViewModel
{
    public ProductViewModel Product { get; set; }
    public double Quantity { get; set; }
    public decimal Price { get; set; }
    public double Income { get; set; }
    public double Outcome { get; set; }
}
