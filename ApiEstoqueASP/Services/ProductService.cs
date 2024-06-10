using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;
using ApiEstoqueASP.Services.Interfaces;
using AutoMapper;

namespace ApiEstoqueASP.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Product GetProductById(int productId) =>
            _productRepository.GetProductById(productId);

        public List<Product> GetProductsByName(string productName) =>
            _productRepository.GetProductsByName(productName);

        public Product CreateNewProduct(Product product)
        {
            Product createdProduct = _productRepository.CreateProduct(product);

            return createdProduct;
        }

        public Product? UpdateProduct(int id, UpdateProductDto dto)
        {
            Product? product = this._productRepository.GetProductById(id);

            if(product is null)
            {
                return null;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;

            this._productRepository.Update(product);

            return product;
        }
    }
}
