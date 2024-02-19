using Invention.Services;
using Invention.Uis;

//var marketService = new MarketService();
//var consoleUI = new MarketServiceUI(marketService);
//await consoleUI.RunAsync();

var productService = new ProductService();
var consoleUI = new ProductServiceUI(productService);
await consoleUI.RunAsync();