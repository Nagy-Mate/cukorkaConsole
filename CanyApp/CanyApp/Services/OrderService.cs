using CandyApp.Models;

namespace CandyApp.Services;

public class OrderService
{
    public void ListOrders(List<Order> orders)
    {
        orders.ForEach(order =>
        {
            Console.WriteLine($"Order ID: {order.Id}, Date: {order.Date}, Total Amount: {order.TotalAmount}Ft \n Items: ");
            order.Items.ForEach(item => 
            { 
                Console.WriteLine($"\tName: {item.Name}, Unit Price: {item.UnitPrice}, Quantity: {item.Quantity}"); 
            });
        });

    }
}
