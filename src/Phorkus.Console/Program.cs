﻿using Phorkus.Proto;
using Phorkus.Core.Utils;

namespace Phorkus.Console
{
    class Program
    {
        internal static void Main(string[] args)
        {
            var app = new Application();
            app.Start(args);
        }
    }
}