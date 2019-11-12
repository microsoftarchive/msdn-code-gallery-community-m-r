using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.techphernalia.windows.forms.techieUI;

namespace MongoStart
{
    [Title("Starting Mongo")]
    [Prefix("Mongo")]
    public class MongoGeneral : TreeDataList
    {
        /*
         * mongo
         * mongo 192.168.56.104:27017
         * mongo --host 192.168.56.104 --port 27017
         * 
         * db
         * show dbs
         * use someDb
         * 
         * show collections
         * show users
         * 
         */

        #region 1. Getting Server
        [Title("Default Settings")]
        [Category("1. Getting Server")]
        [Description("We can use MongoDB.Driver.MongoClient to create Client to interact with MongoDB. If we do not specify any arguement then it connects with mongodb instance running on localhost on default port [27017]")]
        [Code("Console", "mongo")]
        [Code("C#", "new MongoDB.Driver.MongoClient()\n\t.GetServer()\n\t.Connect()")]
        public void Mongo000DefaultConnect()
        {
            try
            {
                Console.WriteLine("Creating client...");
                MongoDB.Driver.MongoClient _client = new MongoDB.Driver.MongoClient();
                Console.WriteLine("Getting Server...");
                MongoDB.Driver.MongoServer _server = _client.GetServer();
                Console.WriteLine("Connecting...");
                _server.Connect();
                Console.WriteLine("Connection Successful.");
            }
            catch (Exception exc)
            {
                new frmException(exc, "You are not running MongoDB on localhost:27017." + Environment.NewLine + "So it is failing.").ShowDialog();
                Console.WriteLine("Connection UnSuccessful.");
            }
        }
        [Title("Using ConnectionString")]
        [Category("1. Getting Server")]
        [Description("We can provide MongoDB ConnectionString in following format\n\n mongodb://[username:password@]hostname[:port][/[database][?options]]\n\nwhich will connect and authenticate with given database if the same is provided.")]
        [Code("Console", "mongo 127.0.0.1:27017")]
        [Code("C#", "new MongoDB.Driver.MongoClient(\"mongodb://127.0.0.1:27017\")\n\t.GetServer()\n\t.Connect()")]
        public void Mongo010ConnectConnectionString()
        {
            try
            {
                String connString = "mongodb://" + Program.MongoData.Host + ":" + Program.MongoData.Port;
                Console.WriteLine("Creating client...");
                Console.WriteLine(connString);
                MongoDB.Driver.MongoClient _client = new MongoDB.Driver.MongoClient(connString);
                Console.WriteLine("Getting Server...");
                MongoDB.Driver.MongoServer _server = _client.GetServer();
                Console.WriteLine("Connecting...");
                _server.Connect();
                Console.WriteLine("Connection Successful.");
            }
            catch (Exception exc)
            {
                new frmException(exc, "You are not running MongoDB on " + Program.MongoData.Host + ":" + Program.MongoData.Port + "." + Environment.NewLine + "So it is failing.").ShowDialog();
                Console.WriteLine("Connection UnSuccessful.");
            }
        }
        [Title("Using ClientSettings")]
        [Category("1. Getting Server")]
        [Description("We can provide MongoDB ClientSettings as MongoDB.Driver.MongoClientSettings object.")]
        [Code("Console", "mongo --host 127.0.0.1 --port 27017")]
        [Code("C#", "new MongoDB.Driver.MongoClient(new MongoDB.Driver.MongoClientSettings { Server = new MongoDB.Driver.MongoServerAddress(\"127.0.0.1\", 27017) })\n\t.GetServer()\n\t.Connect()")]
        public void Mongo020ConnectClientSettings()
        {
            try
            {
                Console.WriteLine("Creating client...");
                MongoDB.Driver.MongoClient _client = new MongoDB.Driver.MongoClient(new MongoDB.Driver.MongoClientSettings { Server = new MongoDB.Driver.MongoServerAddress(Program.MongoData.Host, Program.MongoData.Port) });
                Console.WriteLine("Getting Server...");
                MongoDB.Driver.MongoServer _server = _client.GetServer();
                Console.WriteLine("Connecting...");
                _server.Connect();
                Console.WriteLine("Connection Successful.");
            }
            catch (Exception exc)
            {
                new frmException(exc, "You are not running MongoDB on " + Program.MongoData.Host + ":" + Program.MongoData.Port + "." + Environment.NewLine + "So it is failing.").ShowDialog();
                Console.WriteLine("Connection UnSuccessful.");
            }
        }
        #endregion

        #region 2. DB
        [Title("List Databases")]
        [Category("2. DB")]
        [Description("Here we are trying to list all the available databases in connected MongoDB Server.")]
        [Code("Console", "show dbs")]
        [Code("C#", "new MongoDB.Driver.MongoClient()\n\t.GetServer()\n\t.GetDatabaseNames()")]
        public void Mongo000ShowDbs()
        {
            try
            {
                String connString = "mongodb://" + Program.MongoData.Host + ":" + Program.MongoData.Port;
                Console.WriteLine("Creating client...");
                Console.WriteLine(connString);
                MongoDB.Driver.MongoClient _client = new MongoDB.Driver.MongoClient(connString);
                Console.WriteLine("Getting Server...");
                MongoDB.Driver.MongoServer _server = _client.GetServer();
                Console.WriteLine("Connecting...");
                String[] databases = _server.GetDatabaseNames().ToArray();
                foreach (String db in databases)
                {
                    Console.WriteLine("\t" + db);
                }
                Console.WriteLine(databases.Length+" databases listed.");
            }
            catch (Exception exc)
            {
                new frmException(exc, "You are not running MongoDB on " + Program.MongoData.Host + ":" + Program.MongoData.Port + "." + Environment.NewLine + "Or you do not have rights to list databases.").ShowDialog();
                Console.WriteLine("Connection UnSuccessful.");
            }
        }
        [Title("Connect to Database")]
        [Category("2. DB")]
        [Description("Here we are trying to connect to one of the available databases in connected MongoDB Server or create one if it already does not exist.")]
        [Code("Console", "use techphernalia")]
        [Code("C#", "new MongoDB.Driver.MongoClient()\n\t.GetServer()\n\t.GetDatabase(\"techPhernalia\")")]
        public void Mongo010UseDB()
        {
            try
            {
                String connString = "mongodb://" + Program.MongoData.Host + ":" + Program.MongoData.Port;
                Console.WriteLine("Creating client...");
                Console.WriteLine(connString);
                MongoDB.Driver.MongoClient _client = new MongoDB.Driver.MongoClient(connString);
                Console.WriteLine("Getting Server...");
                MongoDB.Driver.MongoServer _server = _client.GetServer();
                Console.WriteLine("Getting database "+Program.MongoData.Database+"...");
                MongoDB.Driver.MongoDatabase _database = _server.GetDatabase(Program.MongoData.Database);
                Console.WriteLine("Done!");
            }
            catch (Exception exc)
            {
                new frmException(exc, "You are not running MongoDB on " + Program.MongoData.Host + ":" + Program.MongoData.Port + "." + Environment.NewLine + "Or you do not have rights to list databases.").ShowDialog();
                Console.WriteLine("Connection UnSuccessful.");
            }
        }
        [Title("Get CurrentDatabase Name")]
        [Category("2. DB")]
        [Description("We want to know with which database server are we connected to.")]
        [Code("Console", "db")]
        [Code("C#", "MongoDB.Driver.MongoDatabase _database = new MongoDB.Driver.MongoClient()\n\t.GetServer()\n\t.GetDatabase(\"techPhernalia\");\n\n_database.Name")]
        public void Mongo020DB()
        {
            try
            {
                String connString = "mongodb://" + Program.MongoData.Host + ":" + Program.MongoData.Port;
                Console.WriteLine("Creating client...");
                Console.WriteLine(connString);
                MongoDB.Driver.MongoClient _client = new MongoDB.Driver.MongoClient(connString);
                Console.WriteLine("Getting Server...");
                MongoDB.Driver.MongoServer _server = _client.GetServer();
                Console.WriteLine("Getting database " + Program.MongoData.Database + "...");
                MongoDB.Driver.MongoDatabase _database = _server.GetDatabase(Program.MongoData.Database);
                Console.WriteLine(_database.Name);
            }
            catch (Exception exc)
            {
                new frmException(exc, "You are not running MongoDB on " + Program.MongoData.Host + ":" + Program.MongoData.Port + "." + Environment.NewLine + "Or you do not have rights to list databases.").ShowDialog();
                Console.WriteLine("Connection UnSuccessful.");
            }
        }
        #endregion
    }
}
