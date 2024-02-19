using Invention.Models.Commons;
using Invention.Models.SuppliedProducts;
using Invention.Services;
using Invention.Uis;
using System.Data;

//var marketService = new MarketService();
//var consoleUI = new MarketServiceUI(marketService);
//await consoleUI.RunAsync();

//var productService = new ProductService();
//var consoleUI = new ProductServiceUI(productService);
//await consoleUI.RunAsync();


var service1 = new SupplierService();

var service2 = new WarehouseOperationService();
var res = await service2.GetAllAsync();


Console.WriteLine(res);