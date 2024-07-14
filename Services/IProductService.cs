using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IProductService
    {
        List<Product> GetAll();

        Product GetProductById(int id);

        void SaveProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
