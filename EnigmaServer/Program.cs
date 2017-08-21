using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Console Title
            Console.Title = "[Enigma] Server";
            Console.ForegroundColor = ConsoleColor.Yellow;


            
            String InlineCommand = Console.ReadLine();
            while (InlineCommand != "exit")
            {
                InlineCommand = Console.ReadLine();
            }
            ///TODO: Add Exist Routine
        }
    }
}
