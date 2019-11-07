using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Infrastructure.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Tests.Data.Domain
{
    public class Category : Entity
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual IList<Product> Products { get; set; }
    }
}
