using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService()
        {
            this.productRepository = new ProductRepository();
        }

        public void DeleteProduct(Product product)
        {
            this.productRepository.DeleteProduct(product);
        }

        public List<Product> GetAll()
        {
            return this.productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return this.productRepository.GetProductById(id);
        }

        public void SaveProduct(Product product)
        {
            this.productRepository.SaveProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            this.productRepository.UpdateProduct(product);
        }
    }
}
