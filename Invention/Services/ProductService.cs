using Newtonsoft.Json;
using Invention.Helpers;
using Invention.Interfaces;
using Invention.Exceptions;
using Invention.Extensions;
using Invention.Configurations;
using Invention.Models.Products;
using Invention.Models.Products.IntegrationModels;

namespace Invention.Services;

public class ProductService : IProductService
{
    private List<Product> products;
    public async ValueTask<ProductViewModel> CreateAsync(ProductCreationModel product)
    {
        products = await FileIO.ReadAsync<Product>(Constants.ProductPath);
        var existProduct = products.FirstOrDefault(p => p.Code == product.Code);
        
        if(existProduct != null && existProduct.IsDeleted)
        {
            return await UpdateAsync(existProduct.Id, product.MapTo<ProductUpdateModel>(), true);
        }

        if (existProduct is not null)
            throw new AlreadyExistException<Product>();

        var createdProduct = products.Create(product.MapTo<Product>());
        await FileIO.WriteAsync(Constants.ProductPath, products);

        return createdProduct.MapTo<ProductViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        products = await FileIO.ReadAsync<Product>(Constants.ProductPath);
        var existProduct = products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

        if (existProduct is  null)
            throw new NotFoundException<Product>();

        existProduct.IsDeleted = true;
        existProduct.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.ProductPath, products);
        
        return true;
    }

    public async ValueTask<IEnumerable<ProductViewModel>> GetAllAsync()
    {
        products = await FileIO.ReadAsync<Product>(Constants.ProductPath);
        return products.Where(p => !p.IsDeleted).MapTo<ProductViewModel>();
    }

    public async ValueTask<ProductViewModel> GetByIdAsync(long id)
    {
        products = await FileIO.ReadAsync<Product>(Constants.ProductPath);
        var existProduct = products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

        if (existProduct is null)
            throw new NotFoundException<Product>();

        return existProduct.MapTo<ProductViewModel>();
    }

    public async ValueTask<IEnumerable<ProductViewModel>> GetFromApiAsync()
    {
        using var httpClient = new HttpClient();
        var respone = await httpClient.GetAsync(Constants.ProductPath);
        respone.EnsureSuccessStatusCode();
        var content = await respone.Content.ReadAsStringAsync();

        var products = new List<Product>();
        var result = JsonConvert.DeserializeObject<Response>(content);

        var createdProducts = products.BulkCreate(result.Data.Products.MapTo<Product>());

        createdProducts.ForEach(product =>
            {
                product.Code = Convert.ToInt32(product.ProductId);
                product.Barcode = product.Barcode.GenerateBarcode(
                    Constants.UzbBarcode,
                    Constants.IntegrationCode,
                    Convert.ToInt32(product.Code));
            });

        await FileIO.WriteAsync(Constants.ProductPath, createdProducts);

        return products.MapTo<ProductViewModel>();
    }

    public async ValueTask<ProductViewModel> UpdateAsync(long id, ProductUpdateModel product, bool isUsedDeleted = false)
    {
        this.products = await FileIO.ReadAsync<Product>(Constants.ProductPath);
        var existProduct = new Product();

        if (isUsedDeleted)
        {
            existProduct = this.products.FirstOrDefault(c => c.Id == id);
            existProduct.IsDeleted = false;
        }
        else
        {
            existProduct = this.products.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
                ?? throw new NotFoundException<Product>();
        }

        existProduct.Id = id;
        existProduct.Name = product.Name;
        existProduct.Price = product.Price;
        existProduct.Barcode = product.Barcode;
        existProduct.UpdatedAt = DateTime.UtcNow;
        existProduct.Description = product.Description;

        await FileIO.WriteAsync(Constants.ProductPath, products);

        return existProduct.MapTo<ProductViewModel>();
    }
}