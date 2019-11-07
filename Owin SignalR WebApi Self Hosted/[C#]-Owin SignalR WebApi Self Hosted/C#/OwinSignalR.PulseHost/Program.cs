using System;

namespace OwinSignalR.PulseHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pulse.Host.Start();
                Console.ReadLine();
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp);
                Console.ReadLine();
            }
            finally 
            {
                Pulse.Host.Stop();
            }
        }
    }
}
