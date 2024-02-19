namespace Invention.Models.Products;

public class ProductUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Barcode { get; set; }
}