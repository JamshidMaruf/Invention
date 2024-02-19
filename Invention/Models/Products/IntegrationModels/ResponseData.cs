using Newtonsoft.Json;

namespace Invention.Models.Products.IntegrationModels;

public class ResponseData
{
    [JsonProperty("products")]
    public List<ResponseProduct> Products { get; set; }
}

