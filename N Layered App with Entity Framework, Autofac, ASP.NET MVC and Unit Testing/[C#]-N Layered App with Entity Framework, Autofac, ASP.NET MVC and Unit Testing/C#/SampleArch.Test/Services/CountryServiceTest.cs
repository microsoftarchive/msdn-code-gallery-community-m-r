using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleArch.Model;
using SampleArch.Service;

namespace SampleArch.Test.Services
{

    [TestClass]
    public class CountryServiceTest
    {
        private ICountryService _service;
        Mock<IContext> _mockContext;
        Mock<DbSet<Country>> _mockSet;
        IQueryable<Country> listCountry;

        [TestInitialize]
        public void Initialize()
        {
            listCountry = new List<Country>() {
             new Country() { Id = 1, Name = "US" },
             new Country() { Id = 2, Name = "India" },
             new Country() { Id = 3, Name = "Russia" }
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Country>>();
            _mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(listCountry.Provider);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(listCountry.Expression);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(listCountry.ElementType);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(listCountry.GetEnumerator());

            _mockContext = new Mock<IContext>();
            _mockContext.Setup(c => c.Set<Country>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.Countries).Returns(_mockSet.Object);

            _service = new CountryService(_mockContext.Object);

        }

        [TestMethod]
        public void Country_Get_All()
        {


            //Act
            List<Country> results = _service.GetAll().ToList() as List<Country>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }


        [TestMethod]
        public void Can_Add_Country()
        {
            //Arrange
            int Id = 1;
            Country country = new Country() { Name = "UK" };

            _mockSet.Setup(m => m.Add(country)).Returns((Country e) =>
            {
                e.Id = Id;
                return e;
            });


            //Act
            _service.Create(country);

            //Assert
            Assert.AreEqual(Id, country.Id);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once()); 
        }
    }
}
