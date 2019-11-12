
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Infrastructure.Data;
using NUnit.Framework;
using System.Linq.Expressions;
using Infrastructure.Tests.Data.Repository;
using Infrastructure.Tests.Data.Domain;
using Infrastructure.Data.Specification;
using Infrastructure.Tests.Data.Specification;
using Infrastructure.Data.EntityFramework;

namespace Infrastructure.Tests.Data.Repository
{
    [TestFixture]
    public class RepositoryTest
    {
        private ICustomerRepository customerRepository;
        private IRepository repository;

        public RepositoryTest()
        {
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            ObjectContextManager.InitStorage(new SimpleObjectContextStorage());
            ObjectContextManager.Init("DefaultDb", new[] { "Infrastructure.Tests" }, true);

            customerRepository = new CustomerRepository();
            repository = new GenericRepository();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ObjectContextManager.CloseAllObjectContexts();
        }

        [Test]
        public void Test()
        {
            DoAction(() => CreateCustomer());
            DoAction(() => CreateProducts());
            DoAction(() => AddOrders());
            DoAction(() => FindOneCustomer());
            DoAction(() => FindByKey());
            DoAction(() => FindManyOrdersForJohnDoe());
            DoAction(() => FindNewlySubscribed());
            DoAction(() => FindOrderWithInclude());
            DoAction(() => FindProductByName());
            DoAction(() => FindBySpecification());
            DoAction(() => FindByAndCompositeSpecification());
            DoAction(() => FindByOrCompositeSpecification());
            DoAction(() => FindByConcretSpecification());
            DoAction(() => FindByConcretCompositeSpecification());
            DoAction(() => FindByChainOfSpecifications());
            DoAction(() => GetProductsWithPaging());
            DoAction(() => FindCategoryWithInclude());
            DoAction(() => GetCount());
            DoAction(() => UpdateProduct());
        }                
       
        private void FindProductByName()
        {
            IEnumerable<Product> products = repository.Find<Product>(p => p.Name == "Windows XP Professional");
            Assert.AreEqual(1, products.Count());
        }

        private void FindBySpecification()
        {
            Specification<Product> specification = new Specification<Product>(p => p.Name == "Windows XP Professional");
            IEnumerable<Product> products = repository.Find<Product>(specification);
            Assert.AreEqual(1, products.Count());
        }

        private void FindByAndCompositeSpecification()
        {
            IEnumerable<Product> products = repository.Find<Product>(
                new Specification<Product>(p => p.Price < 100).And(new Specification<Product>(p => p.Name == "Windows XP Professional")));
            Assert.AreEqual(1, products.Count());
        }

        private void FindByOrCompositeSpecification()
        {
            IEnumerable<Product> products = repository.Find<Product>(
                new Specification<Product>(p => p.Price < 100).Or(new Specification<Product>(p => p.Name == "Windows XP Professional")));
            Assert.AreEqual(2, products.Count());
        }

        private void FindByConcretSpecification()
        {
            ProductOnSaleSpecification specification = new ProductOnSaleSpecification();
            IEnumerable<Product> productsOnSale = repository.Find<Product>(specification);
            Assert.AreEqual(2, productsOnSale.Count());
        }

        private void FindByConcretCompositeSpecification()
        {
            IEnumerable<Product> products = repository.Find<Product>(
                new AndSpecification<Product>(
                    new ProductOnSaleSpecification(),
                    new ProductByNameSpecification("Windows XP Professional")));
            Assert.AreEqual(1, products.Count());
        }

        private void FindByChainOfSpecifications()
        {
            IEnumerable<Product> products = repository.Find<Product>(
                new ProductOnSaleSpecification()
                    .And(new ProductByNameSpecification("Windows XP Professional")));
            Assert.AreEqual(1, products.Count());
        }

        private void FindOrderWithInclude()
        {
            var c = customerRepository.FindByName("John", "Doe");
            List<Order> orders = repository.Find<Order>(x => x.Customer.Id == c.Id).ToList();
            Console.Write("Found {0} Orders with {1} OrderLines", orders.Count(), orders.ToList()[0].OrderLines.Count);
        }

        private void CreateProducts()
        {
            Category osCategory = new Category { Name = "Operating System" };
            Category msProductCategory = new Category { Name = "MS Product" };

            repository.Add(osCategory);
            repository.Add(msProductCategory);

            var p1 = new Product { Name = "Windows Seven Professional", Price = 100 };
            p1.Categories.Add(osCategory);
            p1.Categories.Add(msProductCategory);
            //osCategory.Products.Add(p1);
            //msProductCategory.Products.Add(p1);
            repository.Add(p1);

            var p2 = new Product { Name = "Windows XP Professional", Price = 20 };
            p2.Categories.Add(osCategory);
            p2.Categories.Add(msProductCategory);
            repository.Add(p2);

            var p3 = new Product { Name = "Windows Seven Home", Price = 80 };
            p3.Categories.Add(osCategory);
            p3.Categories.Add(msProductCategory);
            repository.Add(p3);

            var p4 = new Product { Name = "Windows Seven Ultimate", Price = 110 };
            p4.Categories.Add(osCategory);
            p4.Categories.Add(msProductCategory);
            repository.Add(p4);

            var p5 = new Product { Name = "Windows Seven Premium", Price = 150 };
            p5.Categories.Add(osCategory);
            p5.Categories.Add(msProductCategory);
            repository.Add(p5);

            repository.UnitOfWork.SaveChanges();

            Console.Write("Saved five Products in 2 Category");
        }

        private void FindManyOrdersForJohnDoe()
        {
            var c = customerRepository.FindByName("John", "Doe");
            var orders = repository.Find<Order>(x => x.Customer.Id == c.Id);

            Console.Write("Found {0} Orders with {1} OrderLines", orders.Count(), orders.ToList()[0].OrderLines.Count);
        }

        private void FindNewlySubscribed()
        {
            var newCustomers = customerRepository.NewlySubscribed();

            Console.Write("Found {0} new customers", newCustomers.Count);
        }

        private void AddOrders()
        {
            var c = customerRepository.FindByName("John", "Doe");

            var winXP = repository.FindOne<Product>(x => x.Name == "Windows XP Professional");
            var winSeven = repository.FindOne<Product>(x => x.Name == "Windows Seven Professional");

            var o = new Order
            {
                OrderDate = DateTime.Now,
                Customer = c,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Price = 200, Product = winXP, Quantity = 1},
                    new OrderLine { Price = 699.99, Product = winSeven, Quantity = 5 }
                }
            };

            repository.Add(o);
            repository.UnitOfWork.SaveChanges();
            Console.Write("Saved one order");
        }

        private void FindByKey()
        {
            var c = customerRepository.FindByName("John", "Doe");

            var customer = customerRepository.GetByKey<Customer>(c.Id);

            Console.Write("Found Customer by its PK: {0}", customer != null);
        }

        private void CreateCustomer()
        {
            var c = new Customer { Firstname = "John", Lastname = "Doe", Inserted = DateTime.Now };
            customerRepository.Add(c);
            customerRepository.UnitOfWork.SaveChanges();
        }

        private void FindOneCustomer()
        {
            var c = customerRepository.FindOne<Customer>(x => x.Firstname == "John" &&
                                                    x.Lastname == "Doe");

            Console.Write("Found Customer: {0} {1}", c.Firstname, c.Lastname);
        }

        private void GetProductsWithPaging()
        {
            var output = repository.Get<Product, string>(x => x.Name, 1, 5).ToList();
            Assert.IsTrue(output[0].Name == "Windows Seven Home");
            Assert.IsTrue(output[1].Name == "Windows Seven Premium");
            Assert.IsTrue(output[2].Name == "Windows Seven Professional");
            Assert.IsTrue(output[3].Name == "Windows Seven Ultimate");
            Assert.IsTrue(output[4].Name == "Windows XP Professional");
        }

        private void GetCount()
        {
            int count = repository.Count<Product>();
            Assert.AreEqual(5, count);
        }

        private void FindCategoryWithInclude()
        {
            var category = repository.GetQuery<Category>(x => x.Name == "Operating System").Include(c => c.Products).SingleOrDefault();
            Assert.IsNotNull(category);
            Assert.Greater(category.Products.Count, 0);
        }

        private void UpdateProduct()
        {
            var output = repository.FindOne<Product>(x => x.Name == "Windows XP Professional");
            Assert.IsNotNull(output);

            output.Name = "Windows XP Home";
            repository.Update<Product>(output);
            repository.UnitOfWork.SaveChanges();

            var updated = repository.FindOne<Product>(x => x.Name == "Windows XP Home");
            Assert.IsNotNull(updated);
        }

        private static void DoAction(Expression<Action> action)
        {
            Console.Write("Executing {0} ... ", action.Body.ToString());

            var act = action.Compile();
            act.Invoke();

            Console.WriteLine();
        }        
    }
}
