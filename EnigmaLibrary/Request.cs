using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLibrary
{
    class Request
    {

        public String Path;

        public Dictionary<String, String> Parameters = new Dictionary<string, string>();

        public String AccessToken;

    }
}
