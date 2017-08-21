using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaLibrary;

namespace EnigmaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Console Title
            Console.Title = "[Enigma] Server";
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Start Server
            Logger.GetInstance().Log("Starting Server Routine ....", "Main");
            Server.GetInstance().Setup();
            
            String InlineCommand = Console.ReadLine();
            while (InlineCommand != "exit")
            {
                InlineCommand = Console.ReadLine();
            }
            ///TODO: Add Exist Routine
        }
    }
}
