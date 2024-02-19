namespace Invention.Extensions;

public static class CommonExtension
{
    public static string GenerateBarcode(this string source, int countryCode, int supplierCode, int productCode)
    {
        return source = $"0{countryCode}{supplierCode}{productCode}";
    }
}
