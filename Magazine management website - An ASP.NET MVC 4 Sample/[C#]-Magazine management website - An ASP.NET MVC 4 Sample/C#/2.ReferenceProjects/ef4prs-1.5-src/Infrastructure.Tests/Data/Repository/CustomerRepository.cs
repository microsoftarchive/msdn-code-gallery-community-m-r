using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Data;
using Infrastructure.Tests.Data.Domain;
using System.Data.Objects;
using Infrastructure.Data.EntityFramework;

namespace Infrastructure.Tests.Data.Repository
{
    public interface ICustomerRepository : IRepository
    {
        IList<Customer> NewlySubscribed();

        Customer FindByName(string firstname, string lastname);
    }

    /// <summary>
    /// demostrate an implementation of specific repository
    /// </summary>
    public class CustomerRepository : GenericRepository, ICustomerRepository
    {
        public CustomerRepository()
            : base()
        {
        }

        public CustomerRepository(ObjectContext context)
            : base(context)
        {
        }

        public IList<Customer> NewlySubscribed()
        {
            var lastMonth = DateTime.Now.Date.AddMonths(-1);

            return GetQuery<Customer>().Where(c => c.Inserted >= lastMonth)
                              .ToList();
        }

        public Customer FindByName(string firstname, string lastname)
        {
            return GetQuery<Customer>().Where(c => c.Firstname == firstname && c.Lastname == lastname)
                              .FirstOrDefault();
        }
    }
}
