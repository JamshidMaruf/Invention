using Invention.Models.Suppliers;
using Invention.Services;
using Spectre.Console;

namespace Invention.Uis;

public class SupplierServiceUi
{
    private readonly SupplierService _supplierService;

    public SupplierServiceUi(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Supplier Service - Main Menu")
                    .AddChoices(new[] { "Create Supplier", "Update Supplier", "Delete Supplier", "View Supplier", "View All Suppliers", "Exit" }));

            switch (option)
            {
                case "Create Supplier":
                    await CreateSupplier();
                    break;
                case "Update Supplier":
                    await UpdateSupplier();
                    break;
                case "Delete Supplier":
                    await DeleteSupplier();
                    break;
                case "View Supplier":
                    await GetSupplierById();
                    break;
                case "View All Suppliers":
                    await ViewAllSuppliers();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task CreateSupplier()
    {
        AnsiConsole.Clear();
        var supplier = new SupplierCreationModel();

        supplier.Code = AnsiConsole.Ask<int>("Enter the supplier code:");
        supplier.Name = AnsiConsole.Ask<string>("Enter the supplier name:");
        supplier.ContactDetail = AnsiConsole.Ask<string>("Enter the contact details:");

        try
        {
            var createdSupplier = await _supplierService.CreateAsync(supplier);
            Console.WriteLine("Supplier created successfully!");
            Console.WriteLine();
            DisplaySupplierDetails(createdSupplier);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating supplier: {ex.Message}");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow italic]Press any key to continue...[/]");
        Console.ReadKey();
    }

    private async Task UpdateSupplier()
    {
        Console.Clear();
        var suppliers = await _supplierService.GetAllAsync();
        var selectedSuplier = AnsiConsole.Prompt(new SelectionPrompt<SupplierViewModel>()
            .Title("Updating Supplier!")
            .AddChoices(suppliers)
            .UseConverter(p => p.Name)
            );
        var check = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"Are you sure to delete? [green]({selectedSuplier.Name})[/]")
            .AddChoices(new[]
                {
                    "Yes", "No"
                }));
        var supplierId = selectedSuplier.Id;

        if ( check == "Yes" )
            try
            {
                var existingSupplier = await _supplierService.GetByIdAsync(supplierId);

                var supplier = new SupplierUpdateModel();
                supplier.Code = AnsiConsole.Ask<int>("Enter the new supplier code:", existingSupplier.Code);
                supplier.Name = AnsiConsole.Ask<string>("Enter the new supplier name:", existingSupplier.Name);
                supplier.ContactDetail = AnsiConsole.Ask<string>("Enter the new contact details:", existingSupplier.ContactDetail);

                var updatedSupplier = await _supplierService.UpdateAsync(supplierId, supplier);
                Console.WriteLine("Supplier updated successfully!");
                Console.WriteLine();
                DisplaySupplierDetails(updatedSupplier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating supplier: {ex.Message}");
            }
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow italic]Press any key to continue...[/]");
        Console.ReadKey();
    }

    private async Task DeleteSupplier()
    {
        Console.Clear();
        var suppliers = await _supplierService.GetAllAsync();
        var selectedSuplier = AnsiConsole.Prompt(new SelectionPrompt<SupplierViewModel>()
            .Title("Deleting Supplier!")
            .AddChoices(suppliers)
            .UseConverter(p => p.Name)
            );
        var check = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"Are you sure to delete? [green]({selectedSuplier.Name})[/]")
            .AddChoices(new[]
                {
                    "Yes", "No"
                }));
        var supplierId = selectedSuplier.Id;
        if(check == "Yes")
            try
            {
                var deleted = await _supplierService.DeleteAsync(supplierId);

                if (deleted)
                {
                    AnsiConsole.MarkupLine("[green]Supplier deleted successfully![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Failed to delete supplier.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error deleting supplier: [/]{ex.Message}");
            }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow italic]Press any key to continue...[/]");
        Console.ReadKey();
    }
    private async Task GetSupplierById()
    {
        Console.Clear();
        var suppliers = await _supplierService.GetAllAsync();
        var selectedSuplier = AnsiConsole.Prompt(new SelectionPrompt<SupplierViewModel>()
            .Title("Choose Supplier!")
            .AddChoices(suppliers)
            .UseConverter(p => p.Name)
            );

        var supplierId = selectedSuplier.Id;

        try
        {
            var supplier = await _supplierService.GetByIdAsync(supplierId);
            Console.WriteLine();
            DisplaySupplierDetails(supplier);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving supplier: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    private async Task ViewAllSuppliers()
    {
        AnsiConsole.Clear();
            
        try
        {
            var suppliers = await _supplierService.GetAllAsync();

            if (suppliers != null && suppliers.Any())
            {
                AnsiConsole.WriteLine("All Suppliers:");
                AnsiConsole.WriteLine();

                foreach (var supplier in suppliers)
                {
                    DisplaySupplierDetails(supplier);
                    Console.WriteLine();
                }
            }
            else
            {
                AnsiConsole.WriteLine("No suppliers found.");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error retrieving suppliers: {ex.Message}");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow italic]Press any key to continue...[/]");
        Console.ReadKey();
    }

    private void DisplaySupplierDetails(SupplierViewModel supplier)
    {
        AnsiConsole.MarkupLine($"[yellow]ID: [/]{supplier.Id}");
        AnsiConsole.MarkupLine($"[yellow]Code: [/]{supplier.Code}");
        AnsiConsole.MarkupLine($"[yellow]Name: [/]{supplier.Name}");
        AnsiConsole.MarkupLine($"[yellow]Contact Details: [/]{supplier.ContactDetail}");
    }
}