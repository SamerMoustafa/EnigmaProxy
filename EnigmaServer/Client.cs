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
    class Client
    {

        public Socket Socket { get; protected set; }

        public IPAddress Address { get; protected set; }

        public byte[] Buffer = new byte[Server.MAX_BUFFER_SIZE];

        public PacketCrypter Crypter { get; protected set; }

        protected Logger _Log = Logger.GetInstance();

        public Client(Socket clientSocket)
        {
            Socket = clientSocket;
            _Log.Log("Analyze Client End Point....", this);
            Address = IPAddress.Parse(((IPEndPoint)Socket.RemoteEndPoint).Address.ToString());
            _Log.Log("Generate Packet Crypter Instance for Client", this);
            Crypter = new PacketCrypter();
        }

        public void Async()
        {
            Socket.BeginReceive(Buffer, 0, Server.MAX_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(AsyncReceiveCallback), null);
        }

        private void AsyncReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int received = Socket.EndReceive(ar);
                if (received == 1)
                {
                    int RequestCode = (int)Buffer[0];
                    switch (RequestCode)
                    {
                        case 1:
                            _Log.Log("Client Requested a Handshake ...", this);
                            SendPublicKey();
                            break;
                        default: break;
                    }
                }
                else
                {

                }
                Async();
            }
            catch(Exception ex)
            {
                _Log.Log("Client Disconnected, Shutting Down ....", this);
                Shutdown(ex.Message);
                Server.GetInstance().RemoveClient(this);
            }
            

        }


        private void SendPublicKey()
        {
            try
            {
                _Log.Log("Sending Packet Crypter Public Key to the Client...", this);
                Socket.Send(Crypter.PublicKey);
                _Log.Log("Public Key with Size of " + Crypter.PublicKey.Length + " Bytes Has been Sent", this);
            }
            catch(Exception ex)
            {
                _Log.Log("Client Refused to Recieve Packet, Shutdown ....", this);
                Shutdown();
            }
        }

        public void Shutdown(String reason = null)
        {
            Server.GetInstance().RemoveClient(this);
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Dispose();
            if(reason != null)
            {
                _Log.Log("Shutdown Reason : " + reason, this);
            }
        }

        public void SendResponse(Response response)
        {

        }




    }
}
