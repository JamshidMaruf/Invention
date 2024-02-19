using System.ComponentModel.DataAnnotations;

namespace Invention.Models.Suppliers;

public class SupplierCreationModel
{
    public string Name { get; set; }
    public string ContactDetail { get; set; }
    public int Code { get; set; }
}