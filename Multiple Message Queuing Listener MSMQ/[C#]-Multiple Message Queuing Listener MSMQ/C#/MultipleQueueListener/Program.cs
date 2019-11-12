using System;
using System.Linq;
using System.Messaging;
using System.Reflection;
using System.Threading;
using MultipleQueueListener.Interfaces;

namespace MultipleQueueListener
{
    static class Program
    {
        static void Main()
        {
            CreateQueueIfNotExist("Customers");
            CreateQueueIfNotExist("Products");
            CreateQueueIfNotExist("Sales");

            const string @namespace = "MultipleQueueListener.Queues";

            var assembly = Assembly.LoadFrom(string.Format(@"{0}\MultipleQueueListener.Queues.dll", Environment.CurrentDirectory));

            var type = typeof(IMessageQueueHandler);
            var assemblyClasses = assembly.GetTypes().ToList().Where(p => p.IsClass && p.Namespace == @namespace && type.IsAssignableFrom(p));

            foreach (var queue in assemblyClasses.Select(@class => assembly.CreateInstance(string.Format("{0}.{1}", @class.Namespace, @class.Name)) as IMessageQueueHandler))
            {
                if (queue != null)
                {
                    Console.WriteLine(string.Format("Start reading {0} queue...", queue.ToString()));
                    var thread = new Thread(new ThreadStart(queue.StartRead));
                    thread.Start();
                }
                else               
                    throw new ArgumentNullException();
            }

            while (true)
            {
                var productsQueue = new MessageQueue(@".\Private$\Products");
                productsQueue.Send(string.Format("The product {0} was salved.", DateTime.Now.Millisecond));
                
                Thread.Sleep(1923);

                var salesQueue = new MessageQueue(@".\Private$\Sales");
                salesQueue.Send(string.Format("Sale number {0} was shipped.", DateTime.Now.Millisecond));

                Thread.Sleep(1532);

                productsQueue = new MessageQueue(@".\Private$\Products");
                productsQueue.Send(string.Format("The product {0}{1} was salved.", DateTime.Now.Second, DateTime.Now.Millisecond));

                var customersQueue = new MessageQueue(@".\Private$\Customers");
                customersQueue.Send(string.Format("The customer {0} checkout.", DateTime.Now.Millisecond));
                
                Thread.Sleep(2452);
            }
        }

        private static void CreateQueueIfNotExist(string queueName)
        {
            if (!MessageQueue.Exists(string.Format(@".\Private$\{0}", queueName)))
                MessageQueue.Create(string.Format(@".\Private$\{0}", queueName));
        }
    }
}
