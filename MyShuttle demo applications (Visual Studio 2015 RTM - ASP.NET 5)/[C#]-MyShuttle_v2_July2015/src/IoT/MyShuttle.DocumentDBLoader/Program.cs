namespace MyShuttle.DocumentDBLoader
{
    using Microsoft.Azure.WebJobs;

    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        internal static DocumentDBContext db;

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            Initialize();

            var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        private async static void Initialize()
        {
            db = new DocumentDBContext();

            var database = await db.RetrieveOrCreateDatabaseAsync();
            await db.RetrieveOrCreateCollectionAsync(database.SelfLink);
        }
    }
}
