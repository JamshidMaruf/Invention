namespace Invention.Models.Products;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
    public string Barcode { get; set; }
}
