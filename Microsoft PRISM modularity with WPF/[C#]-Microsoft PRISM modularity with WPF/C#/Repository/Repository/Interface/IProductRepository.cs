using Repository.Model;
using System.Collections.Generic;

namespace Repository.Repository.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
    }
}
