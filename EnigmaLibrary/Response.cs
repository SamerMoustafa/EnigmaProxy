using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLibrary
{
    public class Response
    {


        public static Response GetInstance()
        {
            return new Response();
        }

        public Response Parse(byte[] buffer)
        {
            return null;
        }
    }
}
