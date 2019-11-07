using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShuttle.Device.Enums;
using MyShuttle.Model;
using System.IO;

namespace MyShuttle.Device.Services
{
    public static class DataService
    {
        private static readonly int NumDataRegisters = Properties.Settings.Default.NumDataRegisters;
        private static readonly int NumCrashDataRegisters = Properties.Settings.Default.CrashDataRegisters;

        private static readonly List<AccelerometerEvent> GoodDriverData = GenerateGoodDriverData().ToList();
        private static readonly List<AccelerometerEvent> BadDriverData = GenerateBadDriverData().ToList();

        public static string DeviceId { get; set; }
        public static int DriverId { get; set; }

        public static CompassEvent GetCompassData()
        {
            var rand = new Random();
            var heading = rand.Next(0, 180);

            return new CompassEvent
            {
                DeviceId = DeviceId,
                DriverId = DriverId,
                EventDateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                HeadingDegrees = heading
            };
        }

        public static AccelerometerEvent GetDrivingData(int index, SessionStatus status)
        {
            var acecelEven = (status == SessionStatus.GoodDriver) ? GoodDriverData[index] : BadDriverData[index];
            acecelEven.DeviceId = DeviceId;
            acecelEven.DriverId = DriverId;
            return acecelEven;
        }

        public static AccelerometerEvent GetCrashData(int index)
        {
            return BadDriverData[index];
        }

        public static OBDEvent GetOBDData(string code)
        {
            return new OBDEvent
            {
                DriverId = DriverId,
                DeviceId = DeviceId,
                Code = code
            };
        }

        private static IEnumerable<AccelerometerEvent> GenerateGoodDriverData()
        {
            return LoadDataFromCSV(@".\Resources\GoodDriver.csv");
        }

        private static IEnumerable<AccelerometerEvent> GenerateBadDriverData()
        {
            return LoadDataFromCSV(@".\Resources\BadDriver.csv");
        }

        private static IEnumerable<AccelerometerEvent> LoadDataFromCSV(string fileName)
        {
            return from line in File.ReadAllLines(fileName).Skip(1)
                   let columns = line.Split(',')
                   select new AccelerometerEvent
                   {
                       DriverId = DriverId,
                       DeviceId = DeviceId,
                       X = Double.Parse(columns[3], CultureInfo.InvariantCulture),
                       Y = Double.Parse(columns[4], CultureInfo.InvariantCulture),
                       Z = Double.Parse(columns[5], CultureInfo.InvariantCulture)
                   };
        }

    }
}

