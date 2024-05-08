using Eshop.Models;

namespace Eshop.Lib.Interfaces;

public interface IProductService
{
    public IEnumerable<Product> GetProducts();
}