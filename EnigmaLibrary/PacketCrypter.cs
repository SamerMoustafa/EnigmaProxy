using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EnigmaLibrary
{
    public class PacketCrypter
    {
        public Byte[] PublicKey { get; protected set; }

        public Byte[] PrivateKey { get; protected set; }

        protected Logger _Log = Logger.GetInstance();

        protected RSACryptoServiceProvider CryptoService;

        public const int KEY_BITS = 2048;

        public PacketCrypter()
        {
            ExtractKeys();
        }

        protected void ExtractKeys()
        {
            _Log.Log("Generating Asymmetric Keys for Packets Signing Process ...", this);
            CryptoService = new RSACryptoServiceProvider(KEY_BITS);
            CryptoService.PersistKeyInCsp = false;
            PublicKey = CryptoService.ExportCspBlob(false);
            PrivateKey = CryptoService.ExportCspBlob(true);
        }
        

    }
}
