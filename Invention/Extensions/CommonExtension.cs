namespace Invention.Extensions;

public static class CommonExtension
{
    public static string GenerateBarcode(this string source, int countryCode, int supplierCode, int productCode)
    {
        return source = $"0{countryCode}{supplierCode}{productCode}";
    }

    public static int GenerateProductCode(this int source, long productId) 
    {
        var random = new Random();
        return Convert.ToInt32($"{productId}{random.Next(10, 99)}");
    }
}
