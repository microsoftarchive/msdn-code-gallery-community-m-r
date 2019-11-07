/*
// Desc: Demonstrates inter-thread communication using Monitor.
//       Producer and consumer threads will ping-pong between each other.
//       Code can be modified in such a way that producer will read and queue 
//		 chunks of data from some source, network stream for example, while
//		 consumer thread will process data from the queue. In that scenario,
//		 ping-ponging will not be required as consumer should be disposing of data
//		 independently of producer queuing it.
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace Bisque
{
    public sealed class ProducerConsumer
    {
        const int MagicNumber = 30;                                 // Indicates how many times to bounce between ping and pong threads
        private Object m_lock = new Object();                       // Lock to protect counter increment
        private Queue<int> m_queue = new Queue<int>();

        // Ctor
        public ProducerConsumer()
        {
        }

        // Ping
        public void Producer()
        {
            int counter = 0;

            lock (m_lock)                                           // Allows only one thread at a time inside m_lock
            {
                while (counter < MagicNumber)
                {
                    Thread.Sleep(500);                              // Get data chunks from some source
                    Monitor.Wait(m_lock);                           // Wait if the thread is busy. 'wait' will hold this loop until something else pulses it to release the wait.
                    Console.WriteLine("producer {0}", counter);
                    m_queue.Enqueue(counter);
                    Monitor.Pulse(m_lock);                          // Releases consumer thread

                    counter++;
                }
            }
        }

        public void Consumer()
        {
            lock (m_lock)                                           // Allows only one thread at a time inside m_lock
            {
                Monitor.Pulse(m_lock);

                while (Monitor.Wait(m_lock, 1000))                  // Wait in the loop while producer is busy. Exit when producer times-out. 1000 = 1 second; app will hang without this time-out value
                {
                    int data = m_queue.Dequeue();
                    Console.WriteLine("consumer {0}", data);
                    Monitor.Pulse(m_lock);                          // Release consumer
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer app = new ProducerConsumer();

            // Create 2 threads
            Thread t_producer = new Thread(new ThreadStart(app.Producer));
            Thread t_consumer = new Thread(new ThreadStart(app.Consumer));

            // Start threads
            t_producer.Start();
            t_consumer.Start();

            // Waith for the threads to complete
            t_producer.Join();
            t_consumer.Join();

            Console.WriteLine("\nPress any key to complete the program.\n");
            Console.ReadKey(false);
        }
    }
}

/*
 * Will print:

producer
consumer
producer
consumer
producer
consumer

Press any key to complete the program.
*/