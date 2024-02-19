

using Invention.Services;

var service = new MarketService();

await service.CreateAsync(new Invention.Models.Markets.MarketCreationModel
{
    Address = "TEst",
    Name = "TEst",
    Description = "TEst",
    Email = "TEst",
    Phone = "asd"
});