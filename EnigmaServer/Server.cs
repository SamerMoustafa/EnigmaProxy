using EnigmaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnigmaServer
{
    class Server
    {

        protected static Server _Instance = new Server();

        protected Socket _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public const int MAX_BUFFER_SIZE = 2048;

        protected const int PORT = 90;

        protected const int BACKLOG = 5;

        protected const short KEEPALIVE_TTL = 42;

        protected const int KEEPALIVE_INTERVAL = 2000;

        protected List<Client> Clients = new List<Client>();

        protected Logger _Log = Logger.GetInstance();

        protected Server() { }

        
        public static Server GetInstance()
        {
            return _Instance;
        }

        public void Setup()
        {
            _Log.Section("Server Setup");
            _Log.Log("Binding Server IPEndPoint and Port ....", this);
            _ServerSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            _Log.Log("Start Listening with backlog of " + BACKLOG + ".....", this);
            _ServerSocket.Listen(BACKLOG);
            _Log.Log("Accepting Clients ....", this);
            AcceptClients();
            _Log.Log("Server is Listening for Connection on Port "+PORT, this);
            _Log.EndSection();
            KeepAlive();
        }

        protected void KeepAlive()
        {
            new Thread(new ThreadStart(() => {
                while (true)
                {
                    Client[] ClientsArray = Clients.ToArray();
                    foreach (Client client in ClientsArray)
                    {
                        try {
                            short originalTTL = client.Socket.Ttl;
                            client.Socket.Ttl = KEEPALIVE_TTL;
                            client.Socket.Send(new byte[1] { 0 });
                            client.Socket.Ttl = originalTTL;
                        }
                        catch (Exception ex)
                        {
                            
                            int ClientIndex = Clients.IndexOf(client);
                            _Log.Log("Client #" + ClientIndex + " Disconnected, Shutting Down ....");
                            client.Shutdown(ex.Message);
                            Clients.Remove(client);
                        }
                    }
                    Thread.Sleep(KEEPALIVE_INTERVAL);
                }
                
            })).Start();
        }

        public void RemoveClient(Client client)
        {
            int clientIndex = Clients.IndexOf(client);
            if ( clientIndex >= 0)
            {
                Clients.Remove(client);
                _Log.Log("Client #" + clientIndex.ToString() + " has been removed from Clients List", this);
            }
        }

        protected void AcceptClients()
        {
            _ServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket connectedSocket = _ServerSocket.EndAccept(ar);
            _Log.Log("A Client Requested to Connect to Server....", this);
            Client client = new Client(connectedSocket);
            Clients.Add(client);
            _Log.Log("["+client.Address.ToString()+"] Client #[" + Clients.IndexOf(client) + "] Connected to Server", this);
            //Keep Async with Client
            client.Async();
            //Accept More Clients
            AcceptClients();
        }

    }
}
