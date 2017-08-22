using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaLibrary;

namespace EnigmaClient
{
    class Client
    {
        protected static Client _Instance = null;

        protected Server _Server;

        protected Logger _Log = Logger.GetInstance();

        protected Client()
        {
            Initialize();
        }

        public static Client GetInstance()
        {
            if(_Instance == null)
            {
                _Instance = new Client();
            }
            return _Instance;
        }

        protected void Initialize()
        {
            _Log.Section("Client Initialization");
            _Log.Log("Initialize Server Object", this);
            _Server = Server.GetInstance();
            _Log.Log("Inject Event Handler for Server Connectivity", this);
            _Server.OnServerConnected += _Server_OnServerConnected;
            _Log.EndSection();
            
            _Server.Connect();
        }

        private void _Server_OnServerConnected()
        {
            _Log.Log("Client Announced with Server Connectivity", this);
            _Log.Log("Request a Public Key for Further Connections.....", this);
            _Server.OnServerShakeHand += _Server_OnServerShakeHand;
            _Server.Shakehand();
        }

        private void _Server_OnServerShakeHand()
        {
            
        }
    }
}
