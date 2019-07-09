using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class Logging
    {
        public static void LogSend(string sentString)
        {
            Console.WriteLine("Sending Request API -> NOVA");
            Console.Write(sentString);
            Space();
        }

        public static void LogResponse(string response)
        {
            Console.WriteLine("Received Response API <- NOVA");
            Console.WriteLine(response);
            Space();
        }

        private static void Space()
        {
            Console.WriteLine("\r\n");
        }
    }
}
