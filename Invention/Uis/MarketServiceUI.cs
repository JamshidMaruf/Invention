using Invention.Interfaces;
using Invention.Models.Markets;
using Spectre.Console;

namespace Invention.Uis;

public class MarketServiceUI
{
    private readonly IMarketService _marketService;

    public MarketServiceUI(IMarketService marketService)
    {
        _marketService = marketService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Market Service")
                    .PageSize(10)
                    .AddChoices("Create Market", "Update Market", "Delete Market", "Get All Markets", "Get Market by ID", "Exit")
            );

            switch (selectedOption)
            {
                case "Create Market":
                    await CreateMarketAsync();
                    break;
                case "Update Market":
                    await UpdateMarketAsync();
                    break;
                case "Delete Market":
                    await DeleteMarketAsync();
                    break;
                case "Get All Markets":
                    await GetAllMarketsAsync();
                    break;
                case "Get Market by ID":
                    await GetMarketByIdAsync();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task CreateMarketAsync()
    {
        Console.Clear();
        var market = new MarketCreationModel();

        market.Name = AnsiConsole.Ask<string>("Enter the market name:");
        market.Email = AnsiConsole.Ask<string>("Enter the market email:");
        market.Phone = AnsiConsole.Ask<string>("Enter the market phone number:");
        market.Address = AnsiConsole.Ask<string>("Enter the market address:");
        market.Description = AnsiConsole.Ask<string>("Enter the market description:");

        try
        {
            var createdMarket = await _marketService.CreateAsync(market);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[green]Market created successfully:[/]");
            AnsiConsole.MarkupLine("------------------------------------------------");
            AnsiConsole.MarkupLine($"[yellow]Name : [/]{createdMarket.Name}");
            AnsiConsole.MarkupLine($"[yellow]Description : [/]{createdMarket.Description}");
            AnsiConsole.MarkupLine($"[yellow]Phone : [/]{createdMarket.Phone}");
            AnsiConsole.MarkupLine($"[yellow]Address : [/]{createdMarket.Address}");
            AnsiConsole.MarkupLine($"[yellow]Email : [/]{createdMarket.Email}");
            await Console.Out.WriteLineAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error creating market: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task UpdateMarketAsync()
    {
        Console.Clear();
        var marketId = AnsiConsole.Ask<long>("Enter the market ID to update:");

        try
        {
            var existingMarket = await _marketService.GetByIdAsync(marketId);
            var updatedMarket = new MarketUpdateModel();

            updatedMarket.Name = AnsiConsole.Ask<string>("Enter the updated market name:", existingMarket.Name);
            updatedMarket.Email = AnsiConsole.Ask<string>("Enter the updated market email:", existingMarket.Email);
            updatedMarket.Phone = AnsiConsole.Ask<string>("Enter the updated market phone number:", existingMarket.Phone);
            updatedMarket.Address = AnsiConsole.Ask<string>("Enter the updated market address:", existingMarket.Address);
            updatedMarket.Description = AnsiConsole.Ask<string>("Enter the updated market description:", existingMarket.Description);

            var result = await _marketService.UpdateAsync(marketId, updatedMarket);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[green]Market created successfully:[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("------------------------------------------------");
            AnsiConsole.MarkupLine($"[yellow]Name : [/]{result.Name}");
            AnsiConsole.MarkupLine($"[yellow]Description : [/]{result.Description}");
            AnsiConsole.MarkupLine($"[yellow]Phone : [/]{result.Phone}");
            AnsiConsole.MarkupLine($"[yellow]Address : [/]{result.Address}");
            AnsiConsole.MarkupLine($"[yellow]Email : [/]{result.Email}");
            await Console.Out.WriteLineAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error updating market: {ex.Message}");
        }
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task DeleteMarketAsync()
    {
        Console.Clear();
        var marketId = AnsiConsole.Ask<long>("Enter the market ID to delete:");

        try
        {
            var result = await _marketService.DeleteAsync(marketId);
            if (result)
            {
                AnsiConsole.WriteLine("Market deleted successfully.");
            }
            else
            {
                AnsiConsole.WriteLine($"Error deleting market with ID={marketId}");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error deleting market: {ex.Message}");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task GetAllMarketsAsync()
    {
        Console.Clear();

        try
        {
            // error getting markets!
            var markets = await _marketService.GetAllAsync();
            AnsiConsole.MarkupLine("[blue]All Markets:[/]");
            foreach (var market in markets)
            {
                AnsiConsole.MarkupLine("------------------------------------------------");
                AnsiConsole.MarkupLine($"[yellow]Name : [/]{market.Name}");
                AnsiConsole.MarkupLine($"[yellow]Description : [/]{market.Description}");
                AnsiConsole.MarkupLine($"[yellow]Phone : [/]{market.Phone}");
                AnsiConsole.MarkupLine($"[yellow]Address : [/]{market.Address}");
                AnsiConsole.MarkupLine($"[yellow]Email : [/]{market.Email}");
                await Console.Out.WriteLineAsync();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error getting all markets: {ex.Message}");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task GetMarketByIdAsync()
    {
        Console.Clear();
        var marketId = AnsiConsole.Ask<long>("Enter the market ID to retrieve:");

        try
        {
            var market = await _marketService.GetByIdAsync(marketId);
            AnsiConsole.WriteLine("Market:");
            AnsiConsole.MarkupLine("------------------------------------------------");
            AnsiConsole.MarkupLine($"[yellow]Name : [/]{market.Name}");
            AnsiConsole.MarkupLine($"[yellow]Description : [/]{market.Description}");
            AnsiConsole.MarkupLine($"[yellow]Phone : [/]{market.Phone}");
            AnsiConsole.MarkupLine($"[yellow]Address : [/]{market.Address}");
            AnsiConsole.MarkupLine($"[yellow]Email : [/]{market.Email}");
            await Console.Out.WriteLineAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error getting market: {ex.Message}");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey();
    }
}
