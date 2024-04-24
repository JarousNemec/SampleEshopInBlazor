namespace Eshop.Models;

public class ProductInCart
{
    public ProductInCart(Product product, int count = 1)
    {
        Product = product;
        Count = count;
    }
    public Product Product { get; set; }
    public int Count { get; set; }
}