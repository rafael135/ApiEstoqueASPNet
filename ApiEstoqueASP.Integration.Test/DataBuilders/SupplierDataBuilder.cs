using ApiEstoqueASP.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test.DataBuilders
{
    internal class SupplierDataBuilder : Faker<Supplier>
    {
        public SupplierDataBuilder()
        {
            CustomInstantiator(f =>
            {
                string name = f.Name.FullName();
                DateTime registerDate = f.Date.Recent();
                Supplier supplier = new Supplier()
                {
                    Name = name,
                    RegisterDate = registerDate,
                };
                return supplier;
            });
        }
    }
}
