using EnigmaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Console Title
            Console.Title = "[Enigma] Client";
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Initialize Client
            Logger.GetInstance().Log("Initializing Client Object ..", "Main");
            Client.GetInstance();
            String InlineCommand = Console.ReadLine();
            while(InlineCommand != "exit")
            {
                InlineCommand = Console.ReadLine();
            }
            ///TODO: Add Exist Routine
        }
    }
}
