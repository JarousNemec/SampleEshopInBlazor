using Eshop.Models;

namespace Eshop.Services;

public class CartService
{
    public delegate void CartChanged(ProductInCart item);
    public delegate void CartCommonChanged();
    public event CartChanged OnAddedItem;
    public event CartCommonChanged OnCartChanged;

    private List<ProductInCart> _products = new List<ProductInCart>();
    public IEnumerable<ProductInCart> Items => _products;

    public void Add(Product product, int count)
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

        OnAddedItem?.Invoke(item);
    }

    public void Remove(Guid id)
    {
        var item = _products.FirstOrDefault(x => x.Product.Id == id);
        if (item != null)
        {
            _products.Remove(item);
            OnCartChanged?.Invoke();
        }
       
    }

    public void ChangeCount(Product product, int count)
    {
        var item = _products.FirstOrDefault(x => x.Product.Id == product.Id);
        if (item != null)
        {
            item.Count = count;
            OnCartChanged?.Invoke();
        }
        
    }
}