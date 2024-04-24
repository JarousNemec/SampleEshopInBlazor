using Eshop.Models;

namespace Eshop.Services;

public class ProductService
{
    private const string ESHOP_MODE_ENV = "ESHOP_MODE";
    private const string ESHOP_MODE_ENV_DEV = "dev";
    private List<Product> _products = new List<Product>();

    public IEnumerable<Product> Products => _products;

    public ProductService()
    {
        InitProducts();
    }

    private void InitProducts()
    {
        // var mode = Environment.GetEnvironmentVariable(ESHOP_MODE_ENV);
        
        // if (string.IsNullOrEmpty(mode))
        //     throw new Exception($"Missing {ESHOP_MODE_ENV}");
        
        // if (mode == ESHOP_MODE_ENV_DEV)
        {
            _products.Add(new Product()
            {
                Id = new Guid("90DA80A8-8A06-4F8F-A21A-5BE87EF51D12"),
                Name = "roklik",
                Description = "bbb",
                ImageLink = "https://img.privezemenakup.cz/images/Rohl%c3%adk%20tukov%c3%bd%2043g.jpg?vid=1&tid=23&r=A",
                Price = 10,
            });
            Console.WriteLine("Product added!");
            _products.Add(new Product()
            {
                Id = new Guid("D69C26EC-8733-4278-A968-646F683B9C49"),
                Name = "houska",
                Description = "hhh",
                ImageLink = "https://www.rohlik.cz/cdn-cgi/image/f=auto,w=500,h=500,q=75/https://cdn.rohlik.cz/images/grocery/products/1286405/1286405-1513608655.jpg",
                Price = 400,
            });
            Console.WriteLine("Product added!");
            _products.Add(new Product()
            {
                Id = new Guid("24E78C15-B8B3-47CA-B050-E547D2D61406"),
                Name = "banan",
                Description = "ddd",
                ImageLink = "https://cdn.rohlik.cz/cdn-cgi/image/f=auto,w=650/https://cdn.rohlik.cz/uploads/Vendy/banana-2449019_640.jpg",
                Price = 500,
            });
            Console.WriteLine("Product added!");
        }
        // else
        // {
        //     Console.WriteLine("No product loaded.");
        // }
    }
}