﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLibrary
{
    class Logger
    {

        protected static readonly Logger _Instance = new Logger();

        protected Logger()
        {

        }

        public static Logger GetInstance()
        {
            return _Instance;
        }

        public void Log(String message)
        {
            Console.WriteLine(message);
        }

        public void Log(String message, object reference)
        {
            message = String.Format("[ {0} ] : {1}", reference.GetType().ToString(), message);
            Log(message);
        }

        public void Section(String name)
        {
            Log("");
            Log(String.Format("#----------------------------------------[{0}]", name));
            Log("");
        }

        public void EndSection()
        {
            Log("");
            Log(String.Format("#------------------------------------------------"));
            Log("");
        }
    }
}
