using System;
using MyShuttle.Client.Core.Model;
using System.Threading.Tasks;

namespace MyShuttle.Client.Core.Infrastructure.Abstractions.Services
{
    public interface ILocationServiceSingleton
    {
        Task<Location> CalculatePositionAsync();
    }
}
