using MyShuttle.API.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;
using MyShuttle.API.Model.Dtos;
using MyShuttle.API.Model;

namespace MyShuttle.API.Data.DocumentDb.Queries
{
    public class TopRatedDriversQuery : BaseDocumentQuery
    {
        public int DesiredDrivers { get; set; }

        public TimeSpan Threshold { get; set; }

        public TopRatedDriversQuery(DocumentDbContext ctx) : base(ctx)
        {
            Threshold = new TimeSpan(366, 0, 0, 0);
            DesiredDrivers = 10;
        }

        public async Task<IEnumerable<RatedDriverDto>> ExecuteAsync()
        {
            var collection = await DocumentContext.GetCollectionAsync(DocumentDbContext.CollectionDrivingStyles);

            var data = DocumentContext.CreateDocumentQuery<RatedDriverDto>(collection,
                "SELECT * FROM c")
                .ToList()
                .OrderByDescending(x => x.ClassificationAvg)
                .Take(DesiredDrivers);

            return data;
        }
    }
}
