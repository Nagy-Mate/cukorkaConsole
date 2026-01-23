using CandyApp.Models;

namespace CandyApp.Services;

public class ProductService
{
    public void ListProducts(List<Product> products)
    {
        Console.WriteLine("Termékek: ");
        products.ForEach(product =>
        {
            Console.WriteLine($"\tProduct ID: {product.Id}, Name: {product.Name}, Price: {product.Price}Ft, Available Stock: {product.Stock} ");
        });
    }

    public void ListAvailableProducts(List<Product> products)
    {
        Console.WriteLine("Elérhető termékek: ");
        products.Where(p => p.Stock > 0).ToList().ForEach(product =>
        {
            Console.WriteLine($"\tProduct ID: {product.Id}, Name: {product.Name}, Price: {product.Price}Ft, Available Stock: {product.Stock} ");
        });
    }


    public void CreateProduct(List<Product> products)
    {
        var newProduct = new Product();
        Console.Clear();

        while (true)
        {
            Console.Write($"\nAdja meg az új termék nevét: ");
            var name = Console.ReadLine(); 
            if(string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("A termék neve nem lehet üres.");
            }
            else 
            { 
                newProduct.Name = name; 
                break; 
            }
        }

        while (true)
        {
            Console.Write("Adja meg az új termék árát: ");
            var priceCheck = decimal.TryParse(Console.ReadLine(), out decimal price);
            if (!priceCheck || price <= 0 )
            {
                Console.WriteLine("Az ár csak pozitív szám lehet.");
            }
            else 
            {
                newProduct.Price = price;
                break; 
            }
        }

        while (true)
        {
            Console.Write("Adja meg az új termék készletet: ");
            var stockCheck = int.TryParse(Console.ReadLine(), out int stock);
            if (!stockCheck || stock < 0)
            {
                Console.WriteLine("Az készlet csak pozitív egész szám lehet.");
            }
            else 
            { 
                newProduct.Stock = stock;
                break; 
            }
        }
        newProduct.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
        products.Add(newProduct);
    }

    public void DeleteProduct(List<Product> products)
    {
        Console.Clear();
        ListProducts(products);

        while (true)
        {
            Console.Write("Adja meg az Id-t a törléshez: ");
            var idCheck = int.TryParse(Console.ReadLine(), out int id);
            if (!idCheck || !products.Any(p => p.Id == id))
            {
                Console.WriteLine("Hibás Id.");
            }
            else
            {
                products.RemoveAll(p => p.Id == id); 
                break;
            }
        }
    }
    public void UpdateProduct(List<Product> products)
    {
        Console.Clear();
        ListProducts(products);
        var productToUpdate = new Product();

        while (true)
        {
            Console.Write("Adja meg az Id-t: ");
            var idCheck = int.TryParse(Console.ReadLine(), out int id);
            if (!idCheck || !products.Any(p => p.Id == id))
            {
                Console.WriteLine("Hibás Id.");
            }
            else
            {
                productToUpdate.Id = id;
                break;
            }
        }


        Console.Write($"\nAdja meg az termék új nevét(Enter-régi név): ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            productToUpdate.Name = products.FirstOrDefault(p => p.Id == productToUpdate.Id).Name;
        }
        else
        {
            productToUpdate.Name = name;
        }
        

        while (true)
        {
            Console.Write("Adja meg az termék új árát(Enter-régi ár): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                productToUpdate.Price = products.FirstOrDefault(p => p.Id == productToUpdate.Id).Price; 
                break;
            }

            var priceCheck = decimal.TryParse(input, out decimal price);
            if (!priceCheck || price <= 0)
            {
                Console.WriteLine("Az ár csak pozitív szám lehet.");
            }
            else
            {
                productToUpdate.Price = price;
                break;
            }
        }

        while (true)
        {
            Console.Write("Adja meg az új termék készletet(Enter-régi készlet): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                productToUpdate.Stock = products.FirstOrDefault(p => p.Id == productToUpdate.Id).Stock;
                break;
            }
            var stockCheck = int.TryParse(input, out int stock);
            if (!stockCheck || stock < 0)
            {
                Console.WriteLine("Az készlet csak pozitív egész szám lehet.");
            }
            else
            {
                productToUpdate.Stock = stock;
                break;
            }
        }

        var product = products.FirstOrDefault(p => p.Id == productToUpdate.Id);
        product.Name = productToUpdate.Name;
        product.Price = productToUpdate.Price;
        product.Stock = productToUpdate.Stock;


    }

}
