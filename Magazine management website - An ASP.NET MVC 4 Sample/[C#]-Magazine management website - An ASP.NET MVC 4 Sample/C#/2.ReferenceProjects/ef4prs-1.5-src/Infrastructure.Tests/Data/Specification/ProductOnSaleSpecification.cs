using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Data.Specification;
using Infrastructure.Tests.Data.Domain;

namespace Infrastructure.Tests.Data.Specification
{
    public class ProductOnSaleSpecification : Specification<Product>
    {
        public ProductOnSaleSpecification() : base(p => p.Price < 100) { }
    }
}
