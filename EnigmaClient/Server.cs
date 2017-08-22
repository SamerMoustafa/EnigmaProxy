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

        public byte[] RecieveBytes = new byte[2048];

        protected Logger _Log = Logger.GetInstance();

        public delegate void ServerConnected();

        public event ServerConnected OnServerConnected;

        public delegate void ServerShakeHanded();

        public event ServerShakeHanded OnServerShakeHand;

        protected Boolean IsConnected = false;

        protected Server()
        {
            Initialize();
        }

        public static Server GetInstance()
        {
            return _Instance;
        }

        protected void Initialize()
        {
            
        }


    }
}
