using CandyApp.Models;

namespace CandyApp.Services;

public class ReportService
{
    public void TotalRevenue(List<Order> orders)
    {
        var totalRevenue = orders.Sum(o => o.TotalAmount);
        Console.WriteLine($"Összes bevétel: {totalRevenue}Ft");
    }

    public void MostSoldProduct(List<Order> orders)
    {
        var productSales = new Dictionary<string, int>();
        foreach (var order in orders)
        {
            foreach (var item in order.Items)
            {
                if (productSales.ContainsKey(item.Name))
                {
                    productSales[item.Name] += item.Quantity;
                }
                else
                {
                    productSales[item.Name] = item.Quantity;
                }
            }
        }
        if (productSales.Count == 0)
        {
            Console.WriteLine("Nincs eladott termék.");
            return;
        }
        var mostSoldProduct = productSales.MaxBy(p => p.Value);
        Console.WriteLine($"Legtöbbet eladott termék: {mostSoldProduct.Key} ({mostSoldProduct.Value} darab)");
    }

    public void NotSoldProducts(List<Order> orders, List<Product> products)
    {
        var soldProductIds = new List<int>();
        orders.ForEach(order =>
        {
            order.Items.ForEach(item =>
            {
                if (!soldProductIds.Contains(item.ProductId))
                {
                    soldProductIds.Add(item.ProductId);
                }
            });
        });
        var notSoldProducts = products.Where(p => !soldProductIds.Contains(p.Id)).ToList();
        if (notSoldProducts.Count == 0)
        {
            Console.WriteLine("Minden termékből lett adva.");
            return;
        }
        Console.WriteLine("Nem eladott termékek: ");
        notSoldProducts.ForEach(product =>
        {
            Console.WriteLine($"\tId: {product.Id}, Név: {product.Name}");
        });
    }
}
