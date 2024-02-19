using Invention.Emums;
using Invention.Models.WarehouseOperationStatistics;

namespace Invention.Interfaces;

public interface IWarehouseStatisticsService
{
    ValueTask<List<WarehouseOperationStatisticsViewModel>> GetStatisticsAsync(DateFilter filter);
    ValueTask<WarehouseOperationStatisticsViewModel> GetStatisticAsync(long productId, DateFilter filter);
}