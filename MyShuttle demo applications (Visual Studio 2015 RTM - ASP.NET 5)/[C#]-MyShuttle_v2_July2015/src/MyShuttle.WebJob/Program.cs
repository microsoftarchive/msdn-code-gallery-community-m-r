namespace MyShuttle.WebJob
{
    using Microsoft.Azure.WebJobs;
    using System;

    class Program
    {
        static void Main()
        {
            var config = new JobHostConfiguration();

            //The maximum number of queue messages that are picked up simultaneously to be executed in parallel(default is 16).
            config.Queues.BatchSize = 8;

            //The maximum number of retries before a queue message is sent to a poison queue (default is 5).
            config.Queues.MaxDequeueCount = 4;

            //  The maximum wait time before polling again when a queue is empty (default is 1 minute).
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(10);

            config.NameResolver = new QueueNameResolver();

            var host = new JobHost(config);

            host.RunAndBlock();
        }
    }
}
