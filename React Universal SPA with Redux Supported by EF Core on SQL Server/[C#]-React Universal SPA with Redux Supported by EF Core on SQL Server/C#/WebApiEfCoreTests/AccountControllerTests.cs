using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Auth.Models;
using DataAccessEfCore.DTOs;
using Xunit;

namespace WebApiEfCoreTests
{
    public class AccountControllerTests: TestBase
    {
        public static IEnumerable<object[]> GetUsers(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[]
                {
                    new LoginDTO
                    {
                        Email = "Alice.Brown@example.com",
                        Password = "AliceBrown1234#"
                    }
                },
                new object[]
                {
                    new LoginDTO
                    {
                        Email = "Ann.Blare@example.com",
                        Password = "AnnBlare1234#"
                    }
                },
                new object[]
                {
                    new LoginDTO
                    {
                        Email = "Cathy.Jones@example.com",
                        Password = "CathyJones1234#"
                    }
                },
                new object[]
                {
                    new LoginDTO
                    {
                        Email = "John.Miller@example.com",
                        Password = "JohnMiller1234#"
                    }
                },
                new object[]
                {
                    new LoginDTO
                    {
                        Email = "John.Miller@example.com",
                        Password = "JohnMiller12#"
                    }
                }
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetUsers), parameters: 5)]
        public async Task TestLoginAsync(LoginDTO loginModel)
        {
            // Arrange
            var url = $"{Host}/api/Account/login";
            var request = new HttpRequestMessage(new HttpMethod("POST"), url)
            {
                Content = GetPostData(loginModel)
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = GetResponseResultAsync<UserDTO>(response).Result;

            if (result.UserId < 0)
            {
                Assert.True(string.IsNullOrEmpty(result.Email));
                Assert.True(string.IsNullOrEmpty(result.FullName));
            }
            else
            {
                Assert.False(string.IsNullOrEmpty(result.ScreenName));
                Assert.False(string.IsNullOrEmpty(result.Email));
                Assert.False(string.IsNullOrEmpty(result.FullName));
            }

            // Cleanup
            TestCheckLoginStatus().Wait();

            TestLogoutAsync().Wait();

            TestCheckLoginStatus().Wait();
        }

        [Fact]
        public async Task TestCheckLoginStatus()
        {
            // Arrange
            var url = $"{Host}/api/Account/checkLoginStatus";
            var request = new HttpRequestMessage(new HttpMethod("GET"), url);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = GetResponseResultAsync<UserDTO>(response).Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (string.IsNullOrEmpty(result.Email))
            {
                Assert.Equal(-2, result.UserId);
                Assert.Equal("not loggedIn", result.ScreenName);
                Assert.True(string.IsNullOrEmpty(result.FullName));
            }
            else
            {
                Assert.True(result.UserId > 0);
                Assert.False(string.IsNullOrEmpty(result.ScreenName));
                Assert.False(string.IsNullOrEmpty(result.FullName));
            }
        }

        [Fact]
        public async Task TestLogoutAsync()
        {
            // Arrange
            var url = $"{Host}/api/Account/logout";
            var request = new HttpRequestMessage(new HttpMethod("POST"), url);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

            var result = GetResponseResultAsync<bool>(response).Result;

            Assert.True(result);
        }
    }
}
