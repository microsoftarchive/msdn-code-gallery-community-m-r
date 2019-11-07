using MyShuttle.API.Data;
using MyShuttle.API.Data.DocumentDb;
using MyShuttle.API.DocumentDb.ConsoleApp.HDinsight;
using MyShuttle.API.Model;
using MyShuttle.API.Model.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.DocumentDb.ConsoleApp
{
    class TrackedRidesFiller
    {
        private readonly DocumentDbContext _docCtx;
        private Dictionary<string, GpsPoint[]> _routesGps;
        private IEnumerable<ObdEventLine> _obds;

        public TrackedRidesFiller()
        {
            _docCtx = DocumentDbContextFactory.New();
            _routesGps = new Dictionary<string, GpsPoint[]>();
        }

        public DocumentDbContext DocumentContext
        {
            get
            {
                return _docCtx;
            }
        }

        public async Task<IEnumerable<TrackedRideDocument>> CreateTrackedRides()
        {
            ReadRoutesGps();
            var sqlRides = GetSqlRides();
            //var hiveRides = await GetHiveRides();
            _obds = (await ReadOdbsFromFile()).ToList();
            var rides = GenerateTrackedRides(sqlRides);
            await DocumentContext.AddRange(DocumentDbContext.CollectionTrackedRides, rides);

            return rides;
        }

        //private async Task<string> GetHiveRides()
        //{
        //    Console.WriteLine("Fetching tripdata from HDInsight");
        //    var hdb = HiveDatabaseFactory.New();
        //    var tripdatas = hdb.Tripdata.ToList();
        //    return "";
        //}

        private void ReadRoutesGps()
        {
            var files = Directory.EnumerateFiles(".", "gps_route*.xml");
            foreach (var file in files)
            {
                var gps = new GpsRouteFileParser(file).Read().
                    Select(tuple => new GpsPoint { Lat = tuple.Item1, Lon = tuple.Item2 }).ToArray();
                _routesGps.Add(file, gps);
            }
        }

        private IEnumerable<TrackedRideDocument> GenerateTrackedRides(IEnumerable<Ride> rides)
        {
            var trackedRides = new List<TrackedRideDocument>();
            foreach (var ride in rides)
            {
                var tr = GetOrCreateTrackedRide(ride, trackedRides);
                if (tr.RidesId == null)
                {
                    tr.RidesId = new int[] { ride.RideId };
                    trackedRides.Add(tr);
                }
                else
                {
                    var newIds = new int[tr.RidesId.Length + 1];
                    Array.Copy(tr.RidesId, newIds, tr.RidesId.Length);
                    newIds[newIds.Length - 1] = ride.RideId;
                    tr.RidesId = newIds;
                }
            }

            return trackedRides;
        }

        private TrackedRideDocument GetOrCreateTrackedRide(Ride ride, IEnumerable<TrackedRideDocument> trackedRides)
        {
            var trackedRide = trackedRides.FirstOrDefault(tr => tr.DriverId == ride.DriverId
                && tr.StartTime <= ride.StartDateTime
                && tr.EndTime >= ride.EndDateTime
                && tr.Device == ride.Vehicle.DeviceId);

            return trackedRide ?? CreateTrackedRideForRide(ride);
        }

        private TrackedRideDocument CreateTrackedRideForRide(Ride ride)
        {
            var tr = new TrackedRideDocument();
            tr.Device = ride.Vehicle.DeviceId;
            tr.StartTime = ride.StartDateTime;
            tr.EndTime = ride.EndDateTime;
            tr.DriverId = ride.DriverId;
            AddGpsPoints(tr);
            AddObds(tr);

            return tr;
        }

        private void AddObds(TrackedRideDocument tr)
        {
            int iter = 0;
            var rideObds = _obds.Where(o => o.DriverId == tr.DriverId && o.DeviceId == tr.Device &&
                tr.StartTime <= o.Date && tr.EndTime >= o.Date).ToList();
            if (!rideObds.Any())
            {
                rideObds = _obds.ToList();
            }
            tr.Obds = rideObds.Select(og => new ObdData()
            {
                Code = og.Code,
                Date = tr.StartTime  + TimeSpan.FromMinutes(++iter)
            }).Where(o => o.Date < tr.EndTime).ToArray();
        }

        private async Task<IEnumerable<ObdEventLine>> ReadOdbsFromFile()
        {
            const int CodeIdx = 0;
            const int DriverIdIdx = 1;
            const int DeviceIdx = 2;
            const int DateIdx = 3;
            Console.WriteLine("Fetching ODBEvents from file data");
            var trackedRides = await _docCtx.GetCollectionAsync(DocumentDbContext.CollectionTrackedRides);
            var obds = new CommaSeparatedFileParser("obdevents.txt").Read().
                Select(l => new ObdEventLine
                {
                    Code = l[CodeIdx],
                    DriverId = int.Parse(l[DriverIdIdx]),
                    DeviceId = l[DeviceIdx],
                    Date = l[DateIdx].FromDocumentDbDateTimeString()
                }).OrderBy(o => o.Date);

            return obds;
        }

        private IEnumerable<Ride> GetSqlRides()
        {
            Console.WriteLine("Fetching rides from sql...");
            var dbContext = new MyShuttleDashboardContext();
            var sqlRides = dbContext.Rides.Include("Vehicle").OrderBy(r => r.StartDateTime).ToList();
            Console.WriteLine("Fetched {0} rides from sql", sqlRides.Count);
            return sqlRides;
        }


        private void MergeTrackedRidesWithDbRides(IEnumerable<TrackedRideDocument> trackedRideDocs, IEnumerable<Ride> sqlRides)
        {
            Console.WriteLine("Merging SQL rides and TrackedRides...");
            foreach (var tr in trackedRideDocs)
            {
                tr.RidesId = sqlRides.Where(r => r.StartDateTime >= tr.StartTime && r.EndDateTime <= tr.EndTime).Select(r => r.RideId).ToArray();
            }
        }

        private void AddGpsPoints(TrackedRideDocument trackedRide)
        {
            var rnd = new Random();
            var randomKey = _routesGps.Keys.ToArray()[rnd.Next(0, _routesGps.Keys.Count - 1)];
            var gps = _routesGps[randomKey];
            trackedRide.Gps = gps;
            if (gps.Length > 1)
            {
                var total = 0.0;
                for (var idx = 0; idx < gps.Length - 1; idx++)
                {
                    total += gps[idx + 1].DistanceFrom(gps[idx]);
                }
                trackedRide.Miles = total;
                // Create a random avg speed betwwen 10 and 90
                trackedRide.GpsAverageSpeed = 10.0 + (rnd.NextDouble() * 90.0);
                // Update EndTime accordly to the avg speed
                var hours = trackedRide.Miles / trackedRide.GpsAverageSpeed;
                trackedRide.EndTime = trackedRide.StartTime + TimeSpan.FromSeconds(hours * 3600.0);
            }
        }


    }
}
