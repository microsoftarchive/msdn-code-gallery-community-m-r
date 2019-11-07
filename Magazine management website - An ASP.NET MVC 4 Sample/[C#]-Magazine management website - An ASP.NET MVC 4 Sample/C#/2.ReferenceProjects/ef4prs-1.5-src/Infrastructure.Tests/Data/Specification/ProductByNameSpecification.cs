using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Data.Specification;
using Infrastructure.Tests.Data.Domain;
using System.Linq.Expressions;

namespace Infrastructure.Tests.Data.Specification
{
    public class ProductByNameSpecification : Specification<Product>
    {
        public ProductByNameSpecification(string nameToMatch)
            : base(p => p.Name == nameToMatch)
        { 
        }        
    }
}
