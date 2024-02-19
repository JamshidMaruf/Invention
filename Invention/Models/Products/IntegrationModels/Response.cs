using Newtonsoft.Json;

namespace Invention.Models.Products.IntegrationModels;

public class Response
{
    [JsonProperty("message")]
    public string Message { get; set; }


    [JsonProperty("status")]
    public string Status { get; set; }


    [JsonProperty("data")]
    public ResponseData Data { get; set; }
}