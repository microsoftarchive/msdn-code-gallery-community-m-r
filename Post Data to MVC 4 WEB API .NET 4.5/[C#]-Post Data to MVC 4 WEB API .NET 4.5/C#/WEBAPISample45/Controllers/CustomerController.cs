using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WEBAPISample45.Models;

namespace WEBAPISample45.Controllers
{
    public class CustomerController : ApiController
    {
        private CompanyDBEntities context = new CompanyDBEntities();
        // GET /api/customer
        public IEnumerable<CustomerModel> Get()
        {
            IQueryable<CustomerModel> list = null;
            list = (from c in context.Customers
                    select new CustomerModel { Id = c.Id, Name = c.Name, Salary = (long)c.Salary }).AsQueryable<CustomerModel>();
            return list;
        }

        // GET /api/customer/5
        public CustomerModel Get(int id)
        {

            return (from c in context.Customers
                    where c.Id == id
                    select new CustomerModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Salary = (long)c.Salary
                    }).FirstOrDefault<CustomerModel>();

        }

        // POST /api/customer
        public void Post(CustomerModel customer)
        {
            context.AddToCustomers(new Customer { Id = customer.Id, Name = customer.Name, Salary = customer.Salary });
            context.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
        }

        // PUT /api/customer/5
        public void Put(int id, CustomerModel customer)
        {
            var cust = context.Customers.First(c => c.Id == id);
            cust.Name = customer.Name;
            cust.Salary = customer.Salary;
            context.SaveChanges();
        }


        // DELETE /api/customer/5
        public void Delete(int id)
        {
            var cust = context.Customers.First(c => c.Id == id);
            context.Customers.DeleteObject(cust);
            context.SaveChanges();

        }
    }
}
