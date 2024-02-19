using Invention.Models.Markets;

namespace Invention.Interfaces;

public interface IMarketService
{
    /// <summary>
    /// Create new Market
    /// </summary>
    /// <param name="Market"></param>
    /// <returns></returns>
    ValueTask<MarketViewModel> CreateAsync(MarketCreationModel market);

    /// <summary>
    /// Update exist Market
    /// </summary>
    /// <param name="id"></param>
    /// <param name="market"></param>
    /// <returns></returns>
    ValueTask<MarketViewModel> UpdateAsync(long id, MarketUpdateModel market, bool isUsedDeleted = false);

    /// <summary>
    /// Delete exist Market via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist user via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<MarketViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist Markets
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<MarketViewModel>> GetAllAsync();
}