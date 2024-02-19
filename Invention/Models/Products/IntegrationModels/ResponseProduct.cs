using Newtonsoft.Json;

namespace Invention.Models.Products.IntegrationModels;

public class ResponseProduct
{
    public long Id { get; set; }

    [JsonProperty("id")]
    public long ProductId { get; set; }

    [JsonProperty("name_oz")]
    public string Name { get; set; }

    [JsonProperty("short_description_oz")]
    public string Description { get; set; }

    [JsonProperty("total_price")]
    public decimal Price { get; set; }

    [JsonProperty("quantity")]
    public double Quantity { get; set; }
}