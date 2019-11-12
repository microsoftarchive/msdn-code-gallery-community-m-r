using System.Linq.Expressions;

namespace MyShuttle.DocumentDBLoader
{
    using System;
    using System.IO;
    using Microsoft.Azure.WebJobs;
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessMachineLearningOutput([BlobTrigger("drivingstyle-output/{name}")] TextReader input, string name, TextWriter log)
        {
            log.WriteLine("Blob name: " + name);

            var content = input.ReadToEnd();
            var driversClassifications = GetDriverClassifications(content);

            Program.db.AddOrUpdateRange(driversClassifications);

            log.WriteLine("Blob processed: {0} documents", driversClassifications.Count());
        }

        private static IEnumerable<DriverClassification> GetDriverClassifications(string content)
        {
            var lines = content.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Skip(1);

            return lines.Select(line => line.Split(',')).Select(values => new DriverClassification
            {
                Timestamp = DateTime.Parse(values[0]),
                DriverId = int.Parse(values[1]),
                Classification = int.Parse(values[2]),
            });
        }
    }
}
