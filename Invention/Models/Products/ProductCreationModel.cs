using System.ComponentModel.DataAnnotations;

namespace Invention.Models.Products;

public class ProductCreationModel
{
    public string Name { get; set; }
    public int Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}