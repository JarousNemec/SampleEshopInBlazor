namespace Eshop.Models;

public class RedisCartCacheModel
{
    public List<ProductInCart> Products { get; set; }
    public DateTime Created { get; set; }
    public string UserId { get; set; }
}