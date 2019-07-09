
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Net.Sockets;
namespace novaAPIRev1
{
    //This thread listens to notifications from NOVA on localhost port 8081, and responds with a 
    //simple "OK" JSON-formatted HTTP Response
    class NovaEndpointListener
    {
        //JSON file transformed into a string
        const string OK_RESPONSE = "{\r\n\r\n  \"status\": {\r\n\r\n    \"code\": 200,\r\n\r\n    \"message\": \"OK\"\r\n  }\r\n\r\n}\r\n";

        public NovaEndpointListener()
        {

        }


        public void run()
        {
            //Create an HTTP listener and add port 8081 to it
            HttpListener statusListener = null;

            using (statusListener = new HttpListener())
            {
                statusListener.Prefixes.Add("http://localhost:8081/");

                statusListener.Start();

                //Constantly listen on that port, wait for NOVA to send a message
                //with the GetContext method
                while (true)
                {
                    try
                    {
                        HttpListenerContext context = statusListener.GetContext();
                        HttpListenerRequest request = context.Request;

                        //Read the entire POST request received from NOVA, print it to console
                        string requestStr = "";
                        Stream recStream = request.InputStream;
                        requestStr = new StreamReader(recStream, Encoding.UTF8).ReadToEnd();

                        string printNotification = "[Received Notification:]" + requestStr;
                        Console.WriteLine(printNotification);

                        //Send standard HTTP response to NOVA on same port
                        using (HttpListenerResponse response = context.Response)
                        {
                            response.ContentType = "application/json";
                            response.StatusCode = 200;
                            string responseString = OK_RESPONSE;
                            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                            response.ContentLength64 = buffer.Length;

                            using (Stream output = response.OutputStream)
                            {
                                output.Write(buffer, 0, buffer.Length);
                            }
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                 
            }


            }
        }
    }
}