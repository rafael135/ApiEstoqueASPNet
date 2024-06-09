using ApiEstoqueASP.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test.DataBuilders
{
    internal class UserDataBuilder : Faker<User>
    {
        public UserDataBuilder()
        {
            /*
            CustomInstantiator(f =>
            {

            });
            */
        }
    }
}
