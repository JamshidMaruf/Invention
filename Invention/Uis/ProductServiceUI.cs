using Invention.Interfaces;
using Invention.Models.Products;
using Spectre.Console;

namespace Invention.Uis;

public class ProductServiceUI
{
    private readonly IProductService _productService;

    public ProductServiceUI(IProductService productService)
    {
        _productService = productService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Product Service")
                    .PageSize(10)
                    .AddChoices(
                        "Create Product",
                        "Update Product",
                        "Delete Product",
                        "Get All Products",
                        "Get Product by ID",
                        "Import Products from API",
                        "Exit"
                    )
            );

            switch (selectedOption)
            {
                case "Create Product":
                    await CreateProductAsync();
                    break;
                case "Update Product":
                    await UpdateProductAsync();
                    break;
                case "Delete Product":
                    await DeleteProductAsync();
                    break;
                case "Get All Products":
                    await GetAllProductsAsync();
                    break;
                case "Get Product by ID":
                    await GetProductByIdAsync();
                    break;
                case "Import Products from API":
                    await ImportProductsFromApiAsync();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task CreateProductAsync()
    {
        Console.Clear();
        var product = new ProductCreationModel();

        product.Code = AnsiConsole.Ask<int>("Enter the product code:");
        product.Name = AnsiConsole.Ask<string>("Enter the product name:");
        product.Price = AnsiConsole.Ask<decimal>("Enter the product price:");
        product.Description = AnsiConsole.Ask<string>("Enter the product description:");

        try
        {
            var createdProduct = await _productService.CreateAsync(product);
            AnsiConsole.WriteLine("Product created successfully:");
            AnsiConsole.WriteLine($"Id : {createdProduct.Id}");
            AnsiConsole.WriteLine($"Name : {createdProduct.Name}");
            AnsiConsole.WriteLine($"Price : {createdProduct.Price}");
            AnsiConsole.WriteLine($"Quantity : {createdProduct.Quantity}");
            AnsiConsole.WriteLine($"Barcode : {createdProduct.Barcode}");
            await Console.Out.WriteLineAsync();
            AnsiConsole.WriteLine($"Description : {createdProduct.Description}");
            await Console.Out.WriteLineAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error creating product: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task UpdateProductAsync()
    {
        Console.Clear();
        var productId = AnsiConsole.Ask<long>("Enter the product ID to update:");

        try
        {
            var existingProduct = await _productService.GetByIdAsync(productId);
            var updatedProduct = new ProductUpdateModel();

            updatedProduct.Name = AnsiConsole.Ask<string>("Enter the updated product name:", existingProduct.Name);
            updatedProduct.Price = AnsiConsole.Ask<decimal>("Enter the updated product price:", existingProduct.Price);
            updatedProduct.Description = AnsiConsole.Ask<string>("Enter the updated product description:", existingProduct.Description);

            var result = await _productService.UpdateAsync(productId, updatedProduct);
            AnsiConsole.WriteLine("Product updated successfully:");
            AnsiConsole.WriteLine($"Id : {result.Id}");
            AnsiConsole.WriteLine($"Name : {result.Name}");
            AnsiConsole.WriteLine($"Price : {result.Price}");
            AnsiConsole.WriteLine($"Quantity : {result.Quantity}");
            AnsiConsole.WriteLine($"Barcode : {result.Barcode}");
            await Console.Out.WriteLineAsync();
            AnsiConsole.WriteLine($"Description : {result.Description}");
            await Console.Out.WriteLineAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error updating product: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task DeleteProductAsync()
    {
        Console.Clear();
        var productId = AnsiConsole.Ask<long>("Enter the product ID to delete:");

        try
        {
            var result = await _productService.DeleteAsync(productId);
            if (result)
            {
                AnsiConsole.WriteLine("Product deleted successfully.");
            }
            else
            {
                AnsiConsole.WriteLine($"Error deleting product with ID={productId}");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error deleting product: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task GetAllProductsAsync()
    {
        Console.Clear();

        try
        {
            var products = await _productService.GetAllAsync();
            AnsiConsole.WriteLine("All Products:");
            foreach (var product in products)
            {
                AnsiConsole.MarkupLine("-----------------------------------------------------------");
                AnsiConsole.WriteLine($"Id : {product.Id}");
                AnsiConsole.WriteLine($"Name : {product.Name}");
                AnsiConsole.WriteLine($"Price : {product.Price}");
                AnsiConsole.WriteLine($"Quantity : {product.Quantity}");
                AnsiConsole.WriteLine($"Barcode : {product.Barcode}");
                await Console.Out.WriteLineAsync();
                AnsiConsole.WriteLine($"Description : {product.Description}");
                await Console.Out.WriteLineAsync();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error getting all products: {ex.Message}");
        }

        await Console.Out.WriteLineAsync();
        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task GetProductByIdAsync()
    {
        Console.Clear();
        var productId = AnsiConsole.Ask<long>("Enter the product ID to retrieve:");

        try
        {
            var product = await _productService.GetByIdAsync(productId);
            AnsiConsole.WriteLine("Product:");
            AnsiConsole.WriteLine(product.ToString());
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error getting product: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private async Task ImportProductsFromApiAsync()
    {
        Console.Clear();

        try
        {
            // Error getting from API
            var importedProducts = await _productService.GetFromApiAsync();
            AnsiConsole.WriteLine("Imported Products:");
            foreach (var product in importedProducts)
            {
                AnsiConsole.MarkupLine("-----------------------------------------------------------");
                AnsiConsole.WriteLine($"Id : {product.Id}");
                AnsiConsole.WriteLine($"Name : {product.Name}");
                AnsiConsole.WriteLine($"Price : {product.Price}");
                AnsiConsole.WriteLine($"Quantity : {product.Quantity}");
                AnsiConsole.WriteLine($"Barcode : {product.Barcode}");
                await Console.Out.WriteLineAsync();
                AnsiConsole.WriteLine($"Description : {product.Description}");
                await Console.Out.WriteLineAsync();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error importing products from API: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}