using ApiEstoqueASP.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test.DataBuilders
{
    internal class ProductDataBuilder : Faker<Product>
    {
        public int? SupplierId { get; set; }

        public ProductDataBuilder()
        {
            CustomInstantiator(f =>
            {
                string name = f.Lorem.Word();
                double price = f.Random.Double() * 900.0;
                int inStock = f.Random.Int(20, 100);
                int supplierId = this.SupplierId ?? 1;
                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    InStock = inStock,
                    SupplierId = supplierId
                };
                return product;
            });
        }
    }
}
