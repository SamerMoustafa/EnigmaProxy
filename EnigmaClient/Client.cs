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
            _Server = Server.GetInstance();
        }
    }
}
