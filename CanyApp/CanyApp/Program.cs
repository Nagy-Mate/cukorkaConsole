using CandyApp.Services;



var fileService = new FileService();
var productService = new ProductService();
var orderService = new OrderService();

var products = fileService.LoadData().Products;
var orders = fileService.LoadData().Orders;


var running = true;
while (running)
{

    Console.Write("\nÖsszes Termék[1], Új termék hozzása[2], Termék Törlés[3], Termék Frissítése[4], Rendelés[5], Összes Rendelés[6], Kilép és Mentés[0]: ");
    var checkChoice = int.TryParse(Console.ReadLine(), out int choice );
    if(checkChoice)
    {
        switch (choice)
        {
            case 0:
                fileService.SaveData(products, orders);
                running = false;
                return;
            case 2:
                productService.CreateProduct(products);
                break;
            case 1:
                productService.ListProducts(products);
                break;
            case 3:
                productService.DeleteProduct(products);
                break;
            case 4:
                productService.UpdateProduct(products);
                break;
            case 5:
                orderService.CreateOrder(orders, products);
                break;
            case 6:
                orderService.ListOrders(orders);
                break;
            default:
                break;
        }
    }
    
}
