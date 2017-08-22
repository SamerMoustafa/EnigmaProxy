using EnigmaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaClient
{
    class Server
    {

        protected static Server _Instance = new Server();

        protected Socket _ServerSocket;

        public Byte[] PublicKey;

        protected Logger _Log = Logger.GetInstance();

        protected Server()
        {
            Initiate();
        }

        public static Server GetInstance()
        {
            return _Instance;
        }

        protected void Initiate()
        {
            _Log.Log("Initialize Server Object", this);
            _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _Log.Log("Try Connect to Server .... ", this);
            int attempts = 0;
            while(_ServerSocket.Connected != true)
            {
                try
                {
                    _ServerSocket.Connect("192.168.1.108", 90);
                }
                catch(Exception ex)
                {
                    _Log.Log(ex.Message, this);
                }
                attempts++;
            }
            _Log.Log("Connected to Server after " + attempts + " attempt(s)", this);
            _Log.Log("Start Asynchronous Communications with Server", this);
            Async();
        }

        protected void Async()
        {

        }


    }
}
