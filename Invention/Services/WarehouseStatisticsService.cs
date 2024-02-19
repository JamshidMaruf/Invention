using Invention.Emums;
using Invention.Interfaces;
using Invention.Models.WarehouseOperationStatistics;

namespace Invention.Services;

public class WarehouseStatisticsService : IWarehouseStatisticsService
{
    public ValueTask<WarehouseOperationStatisticsViewModel> GetStatisticAsync(long productId, DateFilter filter)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<WarehouseOperationStatisticsViewModel>> GetStatisticsAsync(DateFilter filter)
    {
        throw new NotImplementedException();
    }
}