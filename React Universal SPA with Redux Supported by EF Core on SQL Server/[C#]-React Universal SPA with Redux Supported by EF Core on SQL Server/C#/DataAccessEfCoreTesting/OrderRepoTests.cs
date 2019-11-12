using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories;
using Xunit;

namespace DataAccessEfCoreTesting
{
    public class OrderRepoTests: BaseTests
    {
        public static IEnumerable<object[]> GetNewOrders(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[] { new OrderToAddDTO // quantities over stock
                {
                    Email = "test@example.com",
                    FullName = "Alice Brown",
                    ProvinceId = 3,
                    City = "Calgary",
                    AddressLine = "1001 1st street",
                    PostalCode = "123456",
                    TotalValue = 2298.00M,
                    CreatedDateTime = DateTime.Now,
                    OrderItems = new []
                    {
                        new OrderItemToAddDTO
                        {
                            SkuId = 60,
                            Price = 938.00M,
                            Quantity = 5
                        },
                        new OrderItemToAddDTO
                        {
                            SkuId = 75,
                            Price = 938.00M,
                            Quantity = 5
                        }
                    }
                }},
                new object[]
                {
                    new OrderToAddDTO // valid
                    {
                        Email = "test@example.com",
                        FullName = "Alice Brown",
                        ProvinceId = 3,
                        City = "Calgary",
                        AddressLine = "1001 1st street",
                        PostalCode = "123456",
                        TotalValue = 2298.00M,
                        CreatedDateTime = DateTime.Now,
                        OrderItems = new []
                        {
                            new OrderItemToAddDTO
                            {
                                SkuId = 14,
                                Price = 938.00M,
                                Quantity = 2
                            },
                            new OrderItemToAddDTO
                            {
                                SkuId = 15,
                                Price = 938.00M,
                                Quantity = 4
                            }
                        }
                    }
                }
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetNewOrders), parameters: 2)]
        public void TestAddOrder(OrderToAddDTO order)
        {
            // arrange 
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new OrderRepo(_dbContext, _mapperProvider, _mapper).AddOrder(order);

            // assert
            Assert.IsType<ResultDTO_AddOrder>(result);

            if (result.SkuIdsOverStock.Any())
            {
                Assert.True(result.OrderId == -1);
            }
            else
            {
                Assert.True(result.OrderId > 0);
                Assert.All(result.Skus, sku =>
                {
                    Assert.True(sku.Quantity == 0);
                });
                Assert.True(result.StyleStates.First().SoldOut);
            }
        }

        [Fact]
        public void TestGetProvinces()
        {
            // arrange 
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new OrderRepo(_dbContext, _mapperProvider, _mapper).GetProvinces().ToList();

            // assert
            Assert.True(result.Count == 13);

            Assert.All(result, province =>
            {
                Assert.True(province.ProvinceId > 0);
                Assert.False(string.IsNullOrEmpty(province.ProvinceName));
            });
        }

        public static IEnumerable<object[]> GetUserIds(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[] {1}, // valid
                new object[] {4},  // no orders for this user
                new object[] {1111} // no user
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetUserIds), parameters: 3)]
        public void TestGetOrdersByUserId(int userId)
        {
            // arrange 
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new OrderRepo(_dbContext, _mapperProvider, _mapper).GetOrdersByUserId(userId).ToList();

            // assert
            if (result.Any())
            {
                Assert.All(result, order =>
                {
                    Assert.True(order.OrderId > 0);
                    Assert.IsType<Guid>(order.CustomerOrderId);
                    Assert.False(string.IsNullOrEmpty(order.City));
                    Assert.True(order.TotalValue > 0);
                    Assert.False(string.IsNullOrEmpty(order.CreatedDateTime));
                });
            }
        }

        public static IEnumerable<object[]> GetCustomerOrderIds(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[] {Guid.Parse("e3ef6231-843f-4d07-9c90-e4f09e2b5ac2")}, // valid
                new object[] {Guid.Parse("e3ef6231-843f-4d07-9c90-e4f09e2b5ac1")} // no orders for this
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetCustomerOrderIds), parameters: 2)]
        public void TestGetOrderById(Guid customerOrderId)
        {
            // arrange 
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new OrderRepo(_dbContext, _mapperProvider, _mapper).GetOrderById(customerOrderId);

            // assert
            if (result == null) return;

            Assert.True(result.OrderId > 0);
            Assert.IsType<Guid>(result.CustomerOrderId);
            Assert.False(string.IsNullOrEmpty(result.FullName));
            Assert.False(string.IsNullOrEmpty(result.ProvinceName));
            Assert.False(string.IsNullOrEmpty(result.City));
            Assert.False(string.IsNullOrEmpty(result.AddressLine));
            Assert.False(string.IsNullOrEmpty(result.PostalCode));
            Assert.True(result.TotalValue > 0);
            Assert.True(result.CreatedDateTime < DateTime.Now);
            Assert.True(result.OrderItems.Any());

            Assert.All(result.OrderItems, item =>
            {
                Assert.True(item.SkuId > 0);
                Assert.False(string.IsNullOrEmpty(item.Skis));
                Assert.True(item.Quantity > 0);
                Assert.True(item.Price > 0);
                Assert.True(item.SubTotal > 0);
            });
        }

        public static IEnumerable<object[]> GetCustomerOrderIdEmails(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[] {Guid.Parse("e3ef6231-843f-4d07-9c90-e4f09e2b5ac2"), "Alice.Brown@example.com"}, // valid
                new object[] {Guid.Parse("e3ef6231-843f-4d07-9c90-e4f09e2b5ac2"), "Alice.Brown@example.com" } // invalid email
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetCustomerOrderIdEmails), parameters: 2)]
        public void TestGetOrderByIdEmail(Guid customerOrderId, string email)
        {
            // arrange 
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new OrderRepo(_dbContext, _mapperProvider, _mapper).GetOrderByIdEmail(customerOrderId, email);

            // assert
            if (result == null) return;

            Assert.True(result.OrderId > 0);
            Assert.IsType<Guid>(result.CustomerOrderId);
            Assert.False(string.IsNullOrEmpty(result.FullName));
            Assert.False(string.IsNullOrEmpty(result.ProvinceName));
            Assert.False(string.IsNullOrEmpty(result.City));
            Assert.False(string.IsNullOrEmpty(result.AddressLine));
            Assert.False(string.IsNullOrEmpty(result.PostalCode));
            Assert.True(result.TotalValue > 0);
            Assert.True(result.CreatedDateTime < DateTime.Now);
            Assert.True(result.OrderItems.Any());

            Assert.All(result.OrderItems, item =>
            {
                Assert.True(item.SkuId > 0);
                Assert.False(string.IsNullOrEmpty(item.Skis));
                Assert.True(item.Quantity > 0);
                Assert.True(item.Price > 0);
                Assert.True(item.SubTotal > 0);
            });
        }
    }
}
