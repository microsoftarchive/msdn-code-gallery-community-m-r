using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Queries
{
    public class BaseDocumentQuery
    {
        private readonly DocumentDbContext _context;
        protected DocumentDbContext DocumentContext
        {
            get
            {
                return _context;
            }
        }

        protected BaseDocumentQuery(DocumentDbContext ctx)
        {
            _context = ctx;
        }
    }
}
