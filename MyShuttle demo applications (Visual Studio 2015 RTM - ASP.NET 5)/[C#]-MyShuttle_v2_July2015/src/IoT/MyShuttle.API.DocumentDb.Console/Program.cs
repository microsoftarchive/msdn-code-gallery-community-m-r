using MyShuttle.API.Data;
using MyShuttle.API.Data.DocumentDb;
using MyShuttle.API.Data.DocumentDb.Queries;
using MyShuttle.API.Model;
using MyShuttle.API.Model.Documents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.DocumentDb.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FillDocs().Wait();
            Console.WriteLine("---");
        }

        private async static Task CreateNeededCollections(DocumentDbContext docdbCtx)
        {
            Console.WriteLine("Recreating collections");
            await docdbCtx.CreateAllNeededCollectionsAsync();
        }

        private async static Task DeleteCollections(DocumentDbContext docdbCtx)
        {
            foreach (var cname in docdbCtx.CollectionNames)
            {
                Console.WriteLine("Deleting collection " + cname);
                await docdbCtx.DeleteCollectionAsync(cname);
            }
        }

        private async static Task FillDocs()
        {
            var deleteSetting = ConfigurationManager.AppSettings["Settings:DeleteDocuments"];
            var deleteFirst = !string.IsNullOrEmpty(deleteSetting) && bool.Parse(deleteSetting);
            var docdbCtx = DocumentDbContextFactory.New();
            if (deleteFirst)
            {
                await DeleteCollections(docdbCtx);
            }
            await CreateNeededCollections(docdbCtx);
            var trackedRidesFiller = new TrackedRidesFiller();
            Console.WriteLine("Filling up TrackedRides collection");
            var rides = await trackedRidesFiller.CreateTrackedRides();
            Console.WriteLine("Creating summary");
            await AddRidesToSummary(trackedRidesFiller.DocumentContext, rides);
        }




        private async static Task AddRidesToSummary(DocumentDbContext docdbCtx, IEnumerable<TrackedRideDocument> rides)
        {
            var summaryCollection = await docdbCtx.GetCollectionAsync(DocumentDbContext.CollectionVehiclesSummary);
            var summary = await new VehicleSummaryQuery(docdbCtx).ExecuteAsync();
            var vehicles = new List<string>(summary.Vehicles);
            foreach (var ride in rides)
            {
                if (!vehicles.Contains(ride.Device))
                {
                    vehicles.Add(ride.Device);
                }


                var rideSpeed = ride.GpsAverageSpeed;
                summary.AverageSpeed = ((summary.AverageSpeed * summary.TotalMiles) + (rideSpeed * ride.Miles)) / (summary.TotalMiles + ride.Miles);
                summary.TotalMiles += ride.Miles;
                summary.TotalSeconds += (ride.EndTime - ride.StartTime).TotalSeconds;
                summary.Breakdowns += ride.Breakdowns;
            }
            summary.Vehicles = vehicles.ToArray();
            summary.VehiclesCount = vehicles.Count;
            await docdbCtx.UpdateDocumentAsync(summary, summaryCollection);
        }


    }


}
