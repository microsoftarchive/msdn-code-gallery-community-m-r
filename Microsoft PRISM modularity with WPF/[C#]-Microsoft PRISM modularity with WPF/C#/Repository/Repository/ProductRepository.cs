using Repository.Model;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Repository.Repository
{
    [Export(typeof(IProductRepository))]
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            return new List<Product>()
            {
                new Product(){ID=1,ProductCode="A0001",ProductName="ABCDEF",ManufactureName="Microsoft"},
                new Product(){ID=2,ProductCode="B0001",ProductName="BCDEFG",ManufactureName="Google"},
                new Product(){ID=3,ProductCode="C0001",ProductName="CDEFGH",ManufactureName="Amazon"},
                new Product(){ID=4,ProductCode="D0001",ProductName="DEFGHI",ManufactureName="Microsoft"},
                new Product(){ID=5,ProductCode="E0001",ProductName="EFGHIJ",ManufactureName="Microsoft"},
            };
        }
    }
}
