using Invention.Helpers;
using Invention.Extensions;
using Invention.Interfaces;
using Invention.Configurations;
using Invention.Models.Markets;

namespace Invention.Services;

public class MarketService : IMarketService
{
    private List<Market> markets;

    public async ValueTask<MarketViewModel> CreateAsync(MarketCreationModel market)
    {
        markets = await FileIO.ReadAsync<Market>(Constants.MarketsPath);
        var existMarket = markets.FirstOrDefault(c => c.Phone == market.Phone);

        // If model is exist and was deleted with this phone number, update this model 
        if (existMarket != null && existMarket.IsDeleted)
        {
            // Recover deleted model with this phone number
            return await UpdateAsync(existMarket.Id, market.MapTo<MarketUpdateModel>(), true);
        }

        // If model is exist, throw exception
        if (existMarket is not null)
            throw new Exception($"This Market is already exist with this phone={market.Phone}");

        var createdMarket = markets.Create(market.MapTo<Market>());
        await FileIO.WriteAsync(Constants.MarketsPath, markets);

        return createdMarket.MapTo<MarketViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        markets = await FileIO.ReadAsync<Market>(Constants.MarketsPath);
        var existMarket = markets.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new Exception($"This Market is not found with ID={id}");

        existMarket.IsDeleted = true;
        existMarket.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.MarketsPath, markets);

        return true;
    }

    public async ValueTask<IEnumerable<MarketViewModel>> GetAllAsync()
    {
        markets = await FileIO.ReadAsync<Market>(Constants.MarketsPath);
        return markets.Where(market => !market.IsDeleted).MapTo<MarketViewModel>();
    }

    public async ValueTask<MarketViewModel> GetByIdAsync(long id)
    {
        markets = await FileIO.ReadAsync<Market>(Constants.MarketsPath);
        var existMarket = markets.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new Exception($"This Market is not found with ID={id}");

        return existMarket.MapTo<MarketViewModel>();
    }

    public async ValueTask<MarketViewModel> UpdateAsync(long id, MarketUpdateModel Market, bool isUsedDeleted = false)
    {
        markets = await FileIO.ReadAsync<Market>(Constants.MarketsPath);
        var existMarket = new Market();

        if (isUsedDeleted)
        {
            existMarket = markets.FirstOrDefault(c => c.Id == id);
            existMarket.IsDeleted = false;
        }
        else
        {
            existMarket = markets.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
                ?? throw new Exception($"This Market is not found with ID={id}");
        }

        existMarket.Name = Market.Name;
        existMarket.Email = Market.Email;
        existMarket.Phone = Market.Phone;
        existMarket.Address = Market.Address;
        existMarket.UpdatedAt = DateTime.UtcNow;
        existMarket.Description = Market.Description;

        await FileIO.WriteAsync(Constants.MarketsPath, markets);

        return existMarket.MapTo<MarketViewModel>();
    }
}
