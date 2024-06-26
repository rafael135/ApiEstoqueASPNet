﻿using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Services.Interfaces
{
    public interface IProductService
    {
        Product CreateNewProduct(Product product);

        List<Product> GetProductsByName(string productName);

        Product GetProductById(int productId);

        Product? UpdateProduct(int id, UpdateProductDto dto);
    }
}
