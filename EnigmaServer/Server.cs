using EnigmaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
            
            _Log.EndSection();
        }

        protected void AcceptClients()
        {
            _ServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            _Log.Log("Waiting for Clients to Connect ...", this);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket connectedSocket = _ServerSocket.EndAccept(ar);
            Client client = new Client(connectedSocket);
            Clients.Add(client);
            _Log.Log("Client Connected to Server From IP : [" + client.Address.ToString() + "]", this);
            _Log.Log("Start Recieving From Client .... ", this);
            connectedSocket.BeginReceive(client.Buffer, 0, MAX_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(RecieveCallback), client);
            AcceptClients();
        }

        private void RecieveCallback(IAsyncResult ar)
        {
            Client client = (Client)ar.AsyncState;
            _Log.Log("Recieved a Request From Client " + client.Address.ToString(), this);
            client.
        }
    }
}
