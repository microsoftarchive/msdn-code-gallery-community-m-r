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

namespace Microsoft.Samples.Spike.AppServicesSvcBusQ
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using System.Configuration;
    using System.Threading;

    public class Program
    {

        //****************************************************************************************
        //
        // This sample demonstrates how to send to and receive messages from Azure Service Bus Queues 
        // using the .NET SDK. 
        //
        // TODO: 
        //   1. Open the Azure Management Portal (http://manage.windowsazure.com) to create a service 
        //      bus namespace and retrieve the connection string details 
        //      (see http://go.microsoft.com/fwlink/?LinkID=325250 for details)
        //
        //   2. Open app.config and update [your namespace] with your service bus namespace and
        //      [your access key] with the access key for the corresponding namespace
        // 
        //   3. Run the project
        //****************************************************************************************


        private static QueueClient queueClient;
        private static string QueueName = "SampleQueue";
        const Int16 maxTrials = 4;

        private static string SessionId1 = Guid.NewGuid().ToString();
        private static string SessionId2 = Guid.NewGuid().ToString();

        static void Main(string[] args)
        {
            if (!VerifyConfiguration())
            {
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Creating a Queue");
            CreateQueue();
            Console.WriteLine("Press any key to start sending messages ...");
            Console.ReadKey();
            SendMessages();
            CheckQueue();
            Console.WriteLine("Press any key to start receiving messages that you just sent.  Press 1 to receive using the IMessageSessionHandler.");
            var c = Console.ReadKey();
            
            if (c.KeyChar == '1')
                ReceiveMessagesByIMessageSessionHandler();
            else
                ReceiveMessages();

            CheckQueue();
            Console.WriteLine("Press any key to receive messages from the dead letter queue.");
            Console.ReadKey();
            ReceiveMessagesFromDeadLetter();
            CheckQueue();
            Console.WriteLine("\nEnd of scenario, press any key to exit.");
            Console.ReadKey();
        }

        private static bool VerifyConfiguration()
        {
            bool configOK = true;
            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            if (connectionString.Contains("[your namespace]") || connectionString.Contains("[your access key]"))
            {
                configOK = false;
                Console.WriteLine("Please update the 'Microsoft.ServiceBus.ConnectionString' appSetting in app.config to specify your Service Bus namespace and secret key.");
            }
            return configOK;

        }

        private static void CreateQueue()
        {
            NamespaceManager namespaceManager = NamespaceManager.Create();

            Console.WriteLine("\nCreating Queue '{0}'...", QueueName);

            // Delete if exists
            if (namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.DeleteQueue(QueueName);
            }

            var description = new QueueDescription(QueueName)
            {
                RequiresSession = true
            };

            namespaceManager.CreateQueue(description);
        }

        private static void CheckQueue()
        {
            NamespaceManager namespaceManager = NamespaceManager.Create();

            var queue = namespaceManager.GetQueue(QueueName);
            
            Console.WriteLine(string.Format("Queue {0} has a message count of {1} and a dead letter count of {2}.", QueueName, queue.MessageCountDetails.ActiveMessageCount, queue.MessageCountDetails.DeadLetterMessageCount));
            
        }

        private static void SendMessages()
        {
            queueClient = QueueClient.Create(QueueName);

            List<BrokeredMessage> messageList = new List<BrokeredMessage>();

            messageList.Add(CreateSampleMessage("1", "First message information sent to session 1", SessionId1));
            messageList.Add(CreateSampleMessage("2", "Second message information sent to session 2", SessionId2));
            messageList.Add(CreateSampleMessage("3", "Third message information sent to session 1", SessionId1));

            Console.WriteLine("\nSending messages to Queue...");

            foreach (BrokeredMessage message in messageList)
            {
                while (true)
                {
                    try
                    {
                        queueClient.Send(message);
                    }
                    catch (MessagingException e)
                    {
                        if (!e.IsTransient)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        else
                        {
                            HandleTransientErrors(e);
                        }
                    }
                    Console.WriteLine(string.Format("Message sent: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                    break;
                }
            }

        }

        private static void ReceiveMessages()
        {
            Console.WriteLine("\nReceiving message from Queue...");
            BrokeredMessage message = null;

            var sessions = queueClient.GetMessageSessions();

            foreach (var browser in sessions)
            {
                Console.WriteLine(string.Format("Session discovered: Id = {0}", browser.SessionId));
                var session = queueClient.AcceptMessageSession(browser.SessionId);                

                while (true)
                {
                    try
                    {
                        message = session.Receive(TimeSpan.FromSeconds(5));

                        if (message != null)
                        {
                            Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                            
                            if (session.SessionId == SessionId2)
                                // if this is the second session then let's send to the dead letter for fun
                                message.DeadLetter();
                            else                            
                                // Further custom message processing could go here...
                                message.Complete();
                        }
                        else
                        {
                            //no more messages in the queue
                            break;
                        }
                    }
                    catch (MessagingException e)
                    {
                        if (!e.IsTransient)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        else
                        {
                            HandleTransientErrors(e);
                        }
                    }
                }
            }
            queueClient.Close();
        }

        private static void ReceiveMessagesFromDeadLetter()
        {
            Console.WriteLine("\nReceiving message from Dead Letter Queue...");
            BrokeredMessage message = null;

            var deadLetterPath = QueueClient.FormatDeadLetterPath(QueueName);

            var deadLetterClient = QueueClient.Create(deadLetterPath);

            while (true)
            {
                try
                {
                    message = deadLetterClient.Receive(TimeSpan.FromSeconds(5));

                    if (message != null)
                    {
                        Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                        // Further custom message processing could go here...
                        message.Complete();
                    }
                    else
                    {
                        //no more messages in the queue
                        break;
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    else
                    {
                        HandleTransientErrors(e);
                    }
                }

            }
            queueClient.Close();
        }

        private static void ReceiveMessagesByIMessageSessionHandler()
        {
            Console.WriteLine("\nReceiving message from Queue...");

            queueClient.RegisterSessionHandler(typeof(MyMessageSessionHandler), new SessionHandlerOptions { AutoComplete = false });

             NamespaceManager namespaceManager = NamespaceManager.Create();
            var queue = namespaceManager.GetQueue(QueueName);

            while(queue.MessageCount > 0)
            {
                Thread.CurrentThread.Join(100);
                queue = namespaceManager.GetQueue(QueueName);
            }

            queueClient.Close();
        }


        private static BrokeredMessage CreateSampleMessage(string messageId, string messageBody, string sessionId)
        {
            BrokeredMessage message = new BrokeredMessage(messageBody);
            message.MessageId = messageId;
            message.SessionId = sessionId;
            return message;
        }

        private static void HandleTransientErrors(MessagingException e)
        {
            //If transient error/exception, let's back-off for 2 seconds and retry
            Console.WriteLine(e.Message);
            Console.WriteLine("Will retry sending the message in 2 seconds");
            Thread.Sleep(2000);
        }
    }

    public class MyMessageSessionHandler : IMessageSessionHandler
    {
        private string WhoAmI = Guid.NewGuid().ToString().Substring(0, 4);

        public void OnCloseSession(MessageSession session)
        {
            Console.WriteLine(string.Format("MyMessageSessionHandler {1} close session Id = {0}", session.SessionId, WhoAmI));
        }

        public void OnMessage(MessageSession session, BrokeredMessage message)
        {
            Console.WriteLine(string.Format("MyMessageSessionHandler {3} received messaged on session Id = {0}, Id = {1}, Body = {2}", session.SessionId, message.MessageId, message.GetBody<string>(), WhoAmI));

            message.Complete();
        }

        public void OnSessionLost(Exception exception)
        {
            Console.WriteLine(string.Format("MyMessageSessionHandler {1} OnSessionLost {0}", exception.Message, WhoAmI));
        }
    }

}
