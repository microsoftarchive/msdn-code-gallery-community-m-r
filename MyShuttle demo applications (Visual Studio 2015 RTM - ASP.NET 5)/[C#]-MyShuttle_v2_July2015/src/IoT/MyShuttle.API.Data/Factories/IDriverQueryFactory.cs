using MyShuttle.API.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Factories
{
    public interface IDriverQueryFactory
    {
        TopDriversQuery GetTopDrivers();
        DriverByIdQuery GetDriverById();
    }
}
