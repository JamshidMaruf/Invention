namespace Invention.Models.Suppliers;

public class Supplier : Auditable
{
    public string Name { get; set; }
    public string ContactDetail { get; set; }
    public int Code { get; set; }
}