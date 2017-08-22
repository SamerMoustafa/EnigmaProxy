using EnigmaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnigmaClient
{
    class Server
    {

        protected static Server _Instance = new Server();

        protected Socket _ServerSocket;

        protected String ServerHost = "192.168.1.108";

        protected int ServerPort = 90;

        public Byte[] PublicKey;

        public byte[] RecieveBytes = new byte[2048];

        protected Logger _Log = Logger.GetInstance();

        public delegate void ServerConnected();

        public event ServerConnected OnServerConnected;

        public delegate void ServerShakeHanded();

        public event ServerShakeHanded OnServerShakeHand;

        protected Boolean IsConnected = false;

        protected Server()
        {

        }

        public static Server GetInstance()
        {
            return _Instance;
        }

        public void Connect()
        {
            _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Loop Til it Connect in a Separate Thread
            _Log.Log("Starting Server Connection Thread, waiting to connect....", this);
            new Thread(new ThreadStart(() => {
                int Attempts = 0;
                while (!_ServerSocket.Connected)
                {
                    try { _ServerSocket.Connect(ServerHost, ServerPort); }
                    catch (Exception ex) { }
                    Attempts++;
                }
                _Log.Log("Connected to the Proxy Server within " + Attempts + " Attemp(s).", this);
                _Log.Log("Triggering OnServerConnected Event...", this);
                OnServerConnected();
            })).Start();
        }

        public void Shakehand()
        {

        }




    }
}
