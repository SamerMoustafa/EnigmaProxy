using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLibrary
{
    public interface IPacket
    {
        IPacket GetInstance();
        IPacket Parse(byte[] buffer);
    }
}
