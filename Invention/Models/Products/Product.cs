namespace Invention.Models.Products;

public class Product : Auditable
{
    public string Name { get; set; }
    public int Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
    public string Barcode { get; set; }
    public long? ProductId { get; set; }
}