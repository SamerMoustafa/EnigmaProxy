using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLibrary
{
    interface IRequestHandler
    {
        void ParseReques(Request request);
        String[] GetRequests();
    }
}
