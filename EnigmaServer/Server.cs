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

        protected const int MAX_BUFFER_SIZE = 2048;

        protected const int PORT = 90;

        protected Server()
        {

        }

        public static Server GetInstance()
        {
            return _Instance;
        }

        public void Setup()
        {

        }
    }
}
