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

        public Byte[] PublicKey { get; protected set; }

        public byte[] RecieveBytes = new byte[2048];

        protected Logger _Log = Logger.GetInstance();

        public delegate void ServerConnected();

        public event ServerConnected OnServerConnected;

        public delegate void ServerShakeHanded();

        public event ServerShakeHanded OnServerShakeHand;

        public delegate void ServerDisconnected();

        public event ServerDisconnected OnServerDisconnected;

        protected bool _IsConnected = false;

        public Boolean IsConnected {
            get { return _IsConnected; }
            protected set {
                _IsConnected = value;
                if (value == false) { OnServerDisconnected(); }
                else { OnServerConnected(); }
            }
        }

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
                IsConnected = true;
            })).Start();
        }


        public void Shakehand()
        {
            byte[] shakeHandCodeBuffer = new byte[1] { 1 };
            byte[] recieveBuffer = new byte[2048];
            _Log.Log("Starting Server Shakehand Thread, waiting to Respond....", this);
            new Thread(new ThreadStart(() => {
                try
                { 
                    _ServerSocket.Send(shakeHandCodeBuffer);
                    int recieved = _ServerSocket.Receive(recieveBuffer, SocketFlags.None);
                    PublicKey = new byte[recieved];
                    Array.Copy(recieveBuffer, PublicKey, recieved);
                    OnServerShakeHand();
                }
                catch (Exception ex)
                {
                    _Log.Log("Failed to Shakehand Server, Shutting Down Connection ... ", this);
                    _Log.Log("Shutdown Reason : " + ex.Message, this);
                    _ServerSocket.Shutdown(SocketShutdown.Both);
                    IsConnected = false;
                }
                
            })).Start();
        }




    }
}
