using Invention.Models.Commons;
using Invention.Models.SuppliedProducts;
using Invention.Services;
using Invention.Uis;
using System.Data;

//var marketService = new MarketService();
//var marketUi = new MarketServiceUI(marketService);
//await marketUi.RunAsync();

//var productService = new ProductService();
//var productUi = new ProductServiceUI(productService);
//await productUi.RunAsync();

var supplierService = new SupplierService();
var supplierUi = new SupplierServiceUi(supplierService);
await supplierUi.RunAsync();

//var service1 = new SupplierService();

//var service2 = new WarehouseOperationService();
//var res = await service2.GetAllAsync();


//Console.WriteLine(res);