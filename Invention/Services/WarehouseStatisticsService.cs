using Invention.Emums;
using Invention.Interfaces;
using Invention.Models.WarehouseOperationStatistics;

namespace Invention.Services;

public class WarehouseStatisticsService : IWarehouseStatisticsService
{
    private readonly ProductService productService;
    public WarehouseStatisticsService(ProductService productService)
    {
        this.productService = productService;  
    }

    public async ValueTask<WarehouseOperationStatisticsViewModel> GetStatisticAsync(long productId, DateFilter filter)
    {
        await productService.GetByIdAsync(productId);

        throw new NotImplementedException();
    }

    public ValueTask<List<WarehouseOperationStatisticsViewModel>> GetStatisticsAsync(DateFilter filter)
    {
        throw new NotImplementedException();
    }
}