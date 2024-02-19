using Invention.Models.Products;

namespace Invention.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Create new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    ValueTask<ProductViewModel> CreateAsync(ProductCreationModel product);

    /// <summary>
    /// Update exist product via ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    ValueTask<ProductViewModel> UpdateAsync(long id, ProductUpdateModel product, bool isUsedDeleted = false);

    /// <summary>
    /// Delete exist product via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist product via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<ProductViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist users
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<ProductViewModel>> GetAllAsync();

    /// <summary>
    /// Get list of product from https://olcha.uz/
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<ProductViewModel>> GetFromApiAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> CheckExistProductsAsync();
}