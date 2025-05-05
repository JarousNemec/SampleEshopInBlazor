using System.Security.Claims;
using System.Text.Json;
using Eshop.Data;
using Eshop.Lib.Interfaces;
using Eshop.Models;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using IDatabase = Microsoft.EntityFrameworkCore.Storage.IDatabase;

namespace Eshop.Services;

public class CartService : ICartService
{
    public event ICartService.CartChanged? OnAddedItem;
    public event ICartService.CartCommonChanged? OnCartChanged;
    private StackExchange.Redis.IDatabase? _redis;
    private List<ProductInCart> _products = new List<ProductInCart>();

    private readonly UserManager<ApplicationUser> _userManager;

    // public IEnumerable<ProductInCart> Items => _products;
    private readonly ConfigurationManager _configuration;

    public CartService(UserManager<ApplicationUser> userManager, ConfigurationManager configurationManager)
    {
        _userManager = userManager;
        _configuration = configurationManager;
        PrepareRedis();
    }

    private void PrepareRedis()
    {
        var redisConnectionString = _configuration.GetConnectionString("RedisConnection");
        if (redisConnectionString == null)
            return;
        ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);
        _redis = connection.GetDatabase();
    }

    public void Add(Product product, int count, ClaimsPrincipal user)
    {
        var item = _products.FirstOrDefault(x => x.Product.Id == product.Id);
        if (item == null)
        {
            item = new ProductInCart(product, count);
            _products.Add(item);
        }
        else
        {
            item.Count += count;
        }

        SaveRedisCache(user);
        OnAddedItem?.Invoke(item);
    }

    public void Remove(Guid id, ClaimsPrincipal user)
    {
        var item = _products.FirstOrDefault(x => x.Product.Id == id);
        if (item != null)
        {
            _products.Remove(item);
            SaveRedisCache(user);
            OnCartChanged?.Invoke();
        }
    }

    public void ChangeCount(Product product, int count, ClaimsPrincipal user)
    {
        var item = _products.FirstOrDefault(x => x.Product.Id == product.Id);
        if (item != null)
        {
            item.Count = count;
            SaveRedisCache(user);
            OnCartChanged?.Invoke();
        }
    }

    public IEnumerable<ProductInCart> GetItems(ClaimsPrincipal user)
    {
        var userId = _userManager.GetUserId(user);
        if (string.IsNullOrEmpty(userId) || _redis == null)
            return _products;
        string cache = _redis.StringGet(userId)!;
        if (string.IsNullOrEmpty(cache))
            return _products;
        var data = JsonSerializer.Deserialize<RedisCartCacheModel>(cache);
        if (data != null)
            _products = data.Products;
        return _products;
    }

    private void SaveRedisCache(ClaimsPrincipal user)
    {
        var userId = _userManager.GetUserId(user);
        if (string.IsNullOrEmpty(userId) || _redis == null)
            return;
        var cacheItem = new RedisCartCacheModel()
        {
            Created = DateTime.Now,
            Products = _products,
            UserId = userId
        };
        var data = JsonSerializer.Serialize(cacheItem);
        _redis.StringSet(userId, data);
    }
}