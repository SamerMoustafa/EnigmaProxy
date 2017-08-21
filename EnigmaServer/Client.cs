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
    class Client
    {

        public Socket Socket { get; protected set; }

        public IPAddress Address { get; protected set; }

        public byte[] Buffer = new byte[Server.MAX_BUFFER_SIZE];

        public Client(Socket clientSocket)
        {
            Socket = clientSocket;
            Address = IPAddress.Parse(((IPEndPoint)Socket.RemoteEndPoint).Address.ToString());
        }


        public void SendResponse(Response response)
        {

        }




    }
}
