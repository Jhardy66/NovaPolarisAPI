using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace novaAPIRev1
{
    //Class takes a JSON string as it's input
    public class HttpCommander
    {
        private HttpWebRequest HttpWebRequest;
        private string jsonRequest = "OK";

        public HttpCommander(string _json)
        {
            jsonRequest = _json;
        }


        //Sends JSON string as an HTTP POST, listens for response, prints it to console. 
        public string Transact()
        {
            string result;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8282");

                httpWebRequest.ContentType = "application/json";

                httpWebRequest.Method = "POST";

                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsonRequestString = jsonRequest;
                    Logging.LogSend(jsonRequestString);
                    streamWriter.Write(jsonRequestString);
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    Logging.LogResponse(result);

                }

                
                return JsonConvert.DeserializeObject(result).ToString();
            }
            catch (WebException ex)
            {
                Logging.Log("NOVA Not Running!");
                return ex.ToString();
            }
        }
    }
}
