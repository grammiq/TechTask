using System;
using System.Collections.Generic;
using System.Linq;

// I assumed SortProduct method receives c# object as input,
// If it must be JSON object as in task example, deserealize must be performed before sorting
// and serealize after sorting 
namespace LoggerProject
{
    public class InventoryManager
    {
        public static List<Product> SortProducts(List<Product> products, string sortKey, bool isAscending)
        {
            switch (sortKey.ToLowerInvariant())
            {
                case "name":
                    return isAscending == true
                        ? products.OrderBy(p => p.Name).ToList()
                        : products.OrderByDescending(p => p.Name).ToList();

                case "price":
                    return isAscending == true
                        ? products.OrderBy(p => p.Price).ToList()
                        : products.OrderByDescending(p => p.Price).ToList();

                case "stock":
                    return isAscending == true
                        ? products.OrderBy(p => p.Stock).ToList()
                        : products.OrderByDescending(p => p.Stock).ToList();

                default:
                    throw new ArgumentException("Invalid input. Please try with valid values");
            }
        }
    }
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    private static void Main(string[] args)
    {
        List<Product> productList = JsonConvert.DeserializeObject<List<Product>>(jsonString);

        List<Product> productList = new List<Product>
        {
           new Product { Name = "Product B", Price = 200, Stock = 3 },
           new Product { Name = "Product A", Price = 100, Stock = 5 },
           new Product { Name = "Product C", Price = 50, Stock = 10 }
        };

        Console.WriteLine("Original List:");
        ShowProducts(productList);

        // Sort by name in ascending order
        List<Product> sortedListByNameAsc = InventoryManager.SortProducts(productList, "name", true);
        ShowProducts(sortedListByNameAsc);

        // Sort by price in descending order
        List<Product> sortedListByPriceDesc = InventoryManager.SortProducts(productList, "price", false);
        ShowProducts(sortedListByPriceDesc);

        // Sort by stock in ascending order
        List<Product> sortedListByStockAsc = InventoryManager.SortProducts(productList, "stock", true);
        ShowProducts(sortedListByStockAsc);
    }

    static void ShowProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
        }
    }
}

