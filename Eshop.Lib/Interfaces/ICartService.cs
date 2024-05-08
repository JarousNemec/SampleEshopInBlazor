using System.Security.Claims;
using Eshop.Models;

namespace Eshop.Lib.Interfaces;

public interface ICartService
{
    public delegate void CartChanged(ProductInCart item);

    public delegate void CartCommonChanged();

    public event CartChanged OnAddedItem;
    public event CartCommonChanged OnCartChanged;
    public void Add(Product product, int count,ClaimsPrincipal user);
    public void Remove(Guid id,ClaimsPrincipal user);
    public void ChangeCount(Product product, int count,ClaimsPrincipal user);
    public IEnumerable<ProductInCart> GetItems(ClaimsPrincipal user);
}