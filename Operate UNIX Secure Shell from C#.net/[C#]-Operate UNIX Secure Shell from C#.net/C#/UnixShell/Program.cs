using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixShell
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var ssh = new SshClient(new UnixConnection().CreateConnection()))
                {
                    ssh.Connect();
                    var command = ssh.CreateCommand("uptime");
                    var result = command.Execute();
                    Console.WriteLine(result);
                    ssh.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Console.Read();


        }
    }
}
    

