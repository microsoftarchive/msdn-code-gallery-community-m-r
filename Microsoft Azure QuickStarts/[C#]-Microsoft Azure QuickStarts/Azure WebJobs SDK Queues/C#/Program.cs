//----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//----------------------------------------------------------------------------------

using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace ComputeWebJobsSDKStorageQueue
{
    //******************************************************************************************************
    // This will show you how to perform common scenarios using the Microsoft Azure Queue storage service using 
    // the Microsoft Azure WebJobs SDK. The scenarios covered include triggering a function when a new message comes
    // on a queue, sending a message on a queue.   
    // 
    // In this sample, the Program class starts the JobHost and creates the demo data. The Functions class
    // contains methods that will be invoked when messages are placed on the queues, based on the attributes in 
    // the method headers.
    //
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    //
    // TODO: Open app.config and paste your Storage connection string into the AzureWebJobsDashboard and
    //      AzureWebJobsStorage connection string settings.
    //*****************************************************************************************************

    class Program
    {
        static void Main()
        {
            if (!VerifyConfiguration())
            {
                Console.ReadLine();
                return;
            }

            CreateDemoData();

            JobHost host = new JobHost();
            host.RunAndBlock();
        }

        private static bool VerifyConfiguration()
        {
            string webJobsDashboard = ConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString;
            string webJobsStorage = ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString;

            bool configOK = true;
            if (string.IsNullOrWhiteSpace(webJobsDashboard) || string.IsNullOrWhiteSpace(webJobsStorage))
            {
                configOK = false;
                Console.WriteLine("Please add the Azure Storage account credentials in App.config");

            }
            return configOK;
        }

        private static void CreateDemoData()
        {
            Console.WriteLine("Creating Demo data");
            Console.WriteLine("Functions will store logs in the 'azure-webjobs-hosts' container in the specified Azure storage account. The functions take in a TextWriter parameter for logging.");
           
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("initialorder");
            CloudQueue queue2 = queueClient.GetQueueReference("initialorderproperty");
            queue.CreateIfNotExists();
            queue2.CreateIfNotExists();

            Order person = new Order()
            {
                Name = "Alex",
                OrderId = Guid.NewGuid().ToString("N").ToLower()
            };

            queue.AddMessage(new CloudQueueMessage(JsonConvert.SerializeObject(person)));
            queue2.AddMessage(new CloudQueueMessage(JsonConvert.SerializeObject(person)));
        }
    }
}
