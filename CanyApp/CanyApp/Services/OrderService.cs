using CandyApp.Models;

namespace CandyApp.Services;

public class OrderService
{
    private ProductService productService = new ProductService();
    public void ListOrders(List<Order> orders)
    {   
        Console.WriteLine("Rendelések: ");
        orders.ForEach(order =>
        {
            Console.WriteLine($"\nOrder ID: {order.Id}, Date: {order.Date}, Total Amount: {order.TotalAmount}Ft \n Items: ");
            order.Items.ForEach(item => 
            { 
                Console.WriteLine($"\tName: {item.Name}, Unit Price: {item.UnitPrice}, Quantity: {item.Quantity}"); 
            });
        });

    }

    public void CreateOrder(List<Order> orders, List<Product> products)
    {
        var newOrder = new Order();
        newOrder.Items = new List<OrderItem>();

        Console.Clear();
        productService.ListAvailableProducts(products);

        decimal totalAmount = 0;
        while (true)
        {

            Console.Write("Adja meg az Id-t a rendeléshez adáshoz[Enter-kész]: ");

            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;

            var productIdCheck = int.TryParse(input, out int productId);
            if (!productIdCheck || !products.Where(p => p.Stock > 0).Any(p => p.Id == productId))
            {
                Console.WriteLine("Hibás Id");
            }
            else
            {
                var selectedProduct = products.FirstOrDefault(p => p.Id == productId);

                while (true)
                {

                    Console.Write($"Adja meg a mennyiséget (Elérhető: {selectedProduct.Stock}): ");
                    var quantityCheck = int.TryParse(Console.ReadLine(), out int quantity);
                    if (!quantityCheck || quantity < 0 || quantity > selectedProduct.Stock)
                    {
                        Console.WriteLine("Hibás mennyiség.");
                    }else if (quantity == 0)
                    {
                        break;
                    }
                    else
                    {
                        var orderItem = new OrderItem
                        {
                            ProductId = selectedProduct.Id,
                            Name = selectedProduct.Name,
                            UnitPrice = selectedProduct.Price,
                            Quantity = quantity
                        };
                        newOrder.Items.Add(orderItem);
                        totalAmount += orderItem.UnitPrice * orderItem.Quantity;
                        selectedProduct.Stock -= quantity;
                        break;
                    }
                }
            }
        }
        if(newOrder.Items.Count == 0) return;
        
        newOrder.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
        newOrder.TotalAmount = totalAmount;
        newOrder.Date = DateTime.Now;
        orders.Add(newOrder);
    }
}
