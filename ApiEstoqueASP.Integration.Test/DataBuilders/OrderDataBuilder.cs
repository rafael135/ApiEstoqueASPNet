using ApiEstoqueASP.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test.DataBuilders
{
    internal class OrderDataBuilder : Faker<Order>
    {
        public string? UserId { get; set; }

        public OrderDataBuilder()
        {
            CustomInstantiator(f =>
            {
                string userId = this.UserId ?? "";
                double totalPrice = f.Random.Double() * 2000;
                DateTime performed = f.Date.Recent();

                Order order = new Order()
                {
                    UserId = userId,
                    TotalPrice = totalPrice,
                    Performed = performed
                };
                
                return order;
            });
        }
    }
}
