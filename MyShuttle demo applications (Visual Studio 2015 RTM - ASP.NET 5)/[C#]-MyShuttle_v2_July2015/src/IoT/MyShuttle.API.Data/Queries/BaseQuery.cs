using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Queries
{
    public class BaseQuery 
    {
        private readonly MyShuttleDashboardContext _context;
        protected MyShuttleDashboardContext DbContext
        {
            get
            {
                return _context;
            }
        }

        protected BaseQuery(MyShuttleDashboardContext ctx)
        {
            _context = ctx;
        }
    }

}
