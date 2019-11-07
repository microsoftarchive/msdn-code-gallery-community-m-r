using MyShuttle.API.Model.Documents;
using MyShuttle.API.Model.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Queries
{
    public class VehicleSummaryQuery : BaseDocumentQuery
    {
        public VehicleSummaryQuery(DocumentDbContext ctx) : base(ctx)
        {
        }

        public async Task<VehiclesSummaryDocument> ExecuteAsync()
        {
            var ssql = string.Format("SELECT * FROM c");
            var collection = await DocumentContext.GetCollectionAsync(DocumentDbContext.CollectionVehiclesSummary);
            var data = DocumentContext.CreateDocumentQuery<VehiclesSummaryDocument>(collection, ssql).ToArray();
            var summary = data.SingleOrDefault();

            return summary ?? new VehiclesSummaryDocument()
            {
                Id = Guid.NewGuid().ToString(), 
                Vehicles = new string[] { }
            };

            //var items = 0;
            //double totalSeconds = 0;
            //var codes = new List<string>();
            //foreach (var tr in data)
            //{
            //    items++;
            //    totalSeconds += (tr.EndTime - tr.StartTime).TotalSeconds;
            //    summary.TotalMiles += tr.Miles;
            //    summary.Breakdowns += tr.Breakdowns;
            //    if (!codes.Contains(tr.Device))
            //    {
            //        codes.Add(tr.Device);
            //    }
            //}

            //summary.AverageSpeed = summary.TotalMiles / totalSeconds * 3600.0;
            //summary.Accidents = 0;
            //summary.VehiclesCount = codes.Count;
            //summary.TotalSeconds = totalSeconds;
            //return summary;
        }
    }
}
