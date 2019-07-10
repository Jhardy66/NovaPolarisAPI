using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class Logging
    {
        private static FileStream logStream;
        private static StreamWriter fsWriter;
        private static TextWriter tOut = Console.Out;

        private static string newLogFilePath = "";

        public Logging()
        {
            
        }

        public static void Initialize()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string logDirFolder = "logs";

            string newLogFileDir = Path.Combine(currentDir, logDirFolder);

            if (!Directory.Exists(newLogFileDir))
            {
                Directory.CreateDirectory(newLogFileDir);
            }

            string logFileName = DateTime.UtcNow.ToString("yyyyMMddhhmm") + "log.txt";

            newLogFilePath = Path.Combine(newLogFileDir, logFileName);

        }
        public static void LogSend(string sentString)
        {
            Log("Sending Request API -> NOVA");
            Console.WriteLine("Sending Request API -> NOVA");
            Log(sentString,true,true);
            Console.Write(sentString);
            Space();
        }

        public static void LogResponse(string response)
        {
            Log("Received Response API <- NOVA");
            Log(response,false,true);
            Space();
        }

        private static void Space()
        {
            Console.WriteLine("\r\n");
        }
        public static void Log(string input, bool surpress = false, bool noDate = false)
        {
      

            string inputwDate = "";
            if (!noDate)
            {
               inputwDate =  "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]" + input;
            }
            else
            {
                inputwDate = input;
            }
            try
            {
                using (logStream = new FileStream(newLogFilePath, FileMode.Append, FileAccess.Write))
                {
                    fsWriter = new StreamWriter(logStream);

                    Console.SetOut(fsWriter);
                    Console.WriteLine(inputwDate);
                    Console.SetOut(tOut);
                    if (!surpress)
                    {
                        Console.WriteLine(inputwDate);
                    }
                    fsWriter.Close();

                }
            }catch(Exception ex)
            {

            }

        }

        public static void Close()
        {
            logStream.Close();
            fsWriter.Close();

        }
    }
}
