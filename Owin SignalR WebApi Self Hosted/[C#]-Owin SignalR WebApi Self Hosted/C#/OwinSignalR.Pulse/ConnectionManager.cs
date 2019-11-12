using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace OwinSignalR.Pulse
{
    public class ConnectionManager
    {
        #region Private Members
        private static ConcurrentDictionary<Tuple<string, string>, ClientConnection> _clients;
        #endregion

        #region Public Properties
        public static ClientConnection ActiveConnections(
              string token
            , string clientId)
        {
            if (_clients == null)
            {
                return new ClientConnection();
            }
            else
            {
                ClientConnection value;

                if (_clients.TryGetValue(new Tuple<string, string>(token, clientId), out value))
                {
                    return value;
                }
                else
                {
                    return new ClientConnection();
                }
            }
        }
        #endregion

        #region Static Methods
        public static void Add(
              string token
            , string clientId
            , string connectionId)
        {
            if (_clients == null)
            {
                _clients = new ConcurrentDictionary<Tuple<string, string>, ClientConnection>();
            }

            var user = _clients.GetOrAdd(new Tuple<string, string>(token, clientId), new ClientConnection
            {
                Connections = new List<ClientConnectionDetials>()
            });

            lock (user.Connections)
            {
                user.Connections.Add(new ClientConnectionDetials { ConnectionId = connectionId });
            }
        }

        public static void Remove(
              string token
            , string clientId
            , string connectionId)
        {
            ClientConnection clientConnection;
            _clients.TryGetValue(new Tuple<string, string>(token, clientId), out clientConnection);

            if (clientConnection != null)
            {
                lock (clientConnection.Connections)
                {
                    clientConnection.Connections.RemoveAll(x => x.ConnectionId == connectionId);

                    if (!clientConnection.Connections.Any())
                    {   
                        ClientConnection removedClient;
                        _clients.TryRemove(new Tuple<string, string>(token, clientId), out removedClient);
                    }
                }
            }
        }

        public static void ClearConnections()
        {
            lock (_clients)
            {
                _clients = new ConcurrentDictionary<Tuple<string, string>, ClientConnection>();
            }
        }
        #endregion

        public class ClientConnection
        {
            public List<ClientConnectionDetials> Connections { get; set; }

            public ClientConnection()
            {
                Connections = new List<ClientConnectionDetials>();
            }
        }

        public class ClientConnectionDetials
        {
            public string ConnectionId { get; set; }
        }
    }
}
