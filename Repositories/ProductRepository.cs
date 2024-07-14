using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product product)
            => ProductDAO.Instance.DeleteProduct(product);

        public List<Product> GetAll()
            => ProductDAO.Instance.GetAll();

        public Product GetProductById(int id)
            => ProductDAO.Instance.GetProductById(id);

        public void SaveProduct(Product product)
            => ProductDAO.Instance.SaveProduct(product);

        public void UpdateProduct(Product product)
            => ProductDAO.Instance.UpdateProduct(product);
    }
}
