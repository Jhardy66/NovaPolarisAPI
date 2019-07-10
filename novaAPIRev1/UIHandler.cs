using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace novaAPIRev1
{
    class UIHandler
    {
        const string badInputString = "Did not understand user input, type help to see valid commands";
        
        static void Main(string[] args)
        {

            bool inputValid = true;
            bool help = false;
            string result;

            //Spin a new thread for the notification listener
            StartListenerThreads();
            SetupConsole();

            while (true)
            {
                //Simple menu system
                Console.WriteLine("===== Polaris NOVA API (type 'h' for command list) =====");
                string userInput = Console.ReadLine();

                switch (userInput.ToLower())
                {
                    case "qr":
                        result = QueryRegistration();
                        break;

                    case "vs":
                        result = GetVenStatus();
                        break;

                    case "cpr":
                        result = CreatePartyRegistration();
                        break;
                    case "xpr":
                        result = CancelPartyRegistration();
                        break;

                    case "cr":
                        result = ClearRegistration();
                        break;

                    case "xr":
                        result = ClearReports();
                        break;

                    case "sv":
                        result = StartVen();
                        break;

                    case "xv":
                        result = StopVen();
                        break;
                    case "re":
                        result = RequestEvent();
                        break;
                    case "xe":
                        result = ClearEvents();
                        break;

                    case "svi":
                        result = SetVenID();
                        break;
                    case "cs":
                        result = CreateOptSchedule();
                        break;

                    case "xs":
                        result = CancelOptSchedule();
                        break;

                    case "oe":
                        result = OptEvent();
                        break;

                    case "ce":
                        result = CreatedEvent();
                        break;

                    case "rr":
                        result = RegisterReports();
                        break;

                    case "rn":
                        result = RegisteredNamespace();
                        break;
                    case "h":
                        PrintMenu();
                        result = "";
                        help = true;
                        break;
                    default:
                        result = badInputString;
                        inputValid = false;
                        break;

                }

                if (inputValid)
                {
                    if (!help)
                    {
                        Logging.LogResponse(result);
                    }
                }
                else
                {
                    Console.WriteLine(badInputString);
                    inputValid = true;
                }
                if (!help)
                {
                    Console.WriteLine("Press Any Button to Continue...");
                    Console.ReadKey();
                }
                help = false;
            }
           
        }

        //All these Methods create objects based on user input, or send parameter-less 
        //Commands if that is what is needed.
        static void StartListenerThreads()
        {
            NovaEndpointListener startDistributeEvent = null;
            NovaEndpointListener completeDistributeEvent = null;
            NovaEndpointListener eventListener = null;
            NovaEndpointListener startEvent = null;
            NovaEndpointListener startEventInterval = null;
            NovaEndpointListener cancelEvent = null;
            NovaEndpointListener deleteEvent = null;
            NovaEndpointListener endEvent = null;
            NovaEndpointListener status = null;

            Thread th_startDistribEvent = null;
            Thread th_completeDistEvent = null;
            Thread th_eventListener = null;
            Thread th_startEvent = null;
            Thread th_startEventInterval = null;
            Thread th_cancelEvent = null;
            Thread th_deleteEvent = null;
            Thread th_endEvent = null;
            Thread th_status = null;

            startDistributeEvent = new NovaEndpointListener("8081","startDistributeEvent");
            completeDistributeEvent = new NovaEndpointListener("8083","completeDistributeEvent");
            eventListener = new NovaEndpointListener("8084","event");
            startEvent = new NovaEndpointListener("8085","startEvent");
            startEventInterval = new NovaEndpointListener("8086","startEventInterval");
            cancelEvent = new NovaEndpointListener("8087","cancelEvent");
            deleteEvent = new NovaEndpointListener("8088","deleteEvent");
            endEvent = new NovaEndpointListener("8089","endEvent");
            status = new NovaEndpointListener("8090","status");

            th_startDistribEvent = new Thread(startDistributeEvent.run);
            th_completeDistEvent = new Thread(completeDistributeEvent.run);
            th_eventListener = new Thread(eventListener.run);
            th_startEvent = new Thread(startEvent.run);
            th_startEventInterval = new Thread(startEventInterval.run);
            th_cancelEvent = new Thread(cancelEvent.run);
            th_deleteEvent = new Thread(deleteEvent.run);
            th_endEvent = new Thread(endEvent.run);
            th_status = new Thread(status.run);

            th_startDistribEvent.Start();
            th_completeDistEvent.Start();
            th_eventListener.Start();
            th_startEvent.Start();
            th_startEventInterval.Start();
            th_cancelEvent.Start();
            th_deleteEvent.Start();
            th_endEvent.Start();
            th_status.Start();

        }
        static string QueryRegistration()
        {
            //creates a new JSON request with no parameters, see readme for list of commands
            string _queryReg = JsonConvert.SerializeObject(new NoParamsCMD("ven.queryRegistration"), Formatting.Indented);

            //namespace is a keyword in c#, have to replace capital letters in JSON string for NOVA
            string queryReg = _queryReg.Replace("NameSpace", "namespace");
            HttpCommander httpCommander = new HttpCommander(queryReg);

            return (httpCommander.Transact());
        }

        static string GetVenStatus()
        {
            string _venStatus = JsonConvert.SerializeObject(new NoParamsCMD("ven.status"),Formatting.Indented);

            //namespace is reserved in c#, have to replace capital letters in JSON string for NOVA
            string venStatus = _venStatus.Replace("NameSpace", "namespace");
            HttpCommander httpCommander = new HttpCommander(venStatus);

            return (httpCommander.Transact());
        }

        static string CreatePartyRegistration()
        {
            bool includeId = false;
            bool validEntry = false;
            while (!validEntry)
            {
                Console.WriteLine("Include IDs:(Y/N)");
                string includeID = Console.ReadLine();


                switch (includeID.ToLower())
                {
                    case "y":
                        includeId = true;
                        validEntry = true;
                        break;
                    case "n":
                        includeId = false;
                        validEntry = true;
                        break;
                    default:
                        Console.WriteLine("Could Not Understand.... Retry");
                        break;
                }
                
            }
            //Creates a new Party Register object, formats it as JSON, sends it to NOVA
            PartyRegStruct partyReg = new PartyRegStruct(includeId);

            string _partyRegstr = JsonConvert.SerializeObject(partyReg, Formatting.Indented);

            string partyRegStr = _partyRegstr.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(partyRegStr);

            return (httpCommander.Transact());
         
        }

        static string CancelPartyRegistration()
        {
            string _cancelPartyReg = JsonConvert.SerializeObject(new NoParamsCMD("ven.cancelPartyRegistration"), Formatting.Indented);

            string cancelPartyReg = _cancelPartyReg.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(cancelPartyReg);

            return (httpCommander.Transact());
        }

        static string ClearRegistration()
        {
           return SendNoParamsRequest("ven.clearRegistration");
        }

        static string ClearReports()
        {
            return SendNoParamsRequest("ven.clearReports");
        }

        static string StartVen()
        {
            return SendNoParamsRequest("ven.start");
        }

        static string StopVen()
        {
            return SendNoParamsRequest("ven.stop");
        }

        static string RequestEvent()
        {
           return SendNoParamsRequest("ven.requestEvent");

        }

        static string ClearEvents()
        {
            return SendNoParamsRequest("ven.clearEvents");
        }

        static string SetVenID()
        {
            //Takes User input and makes a setVenID object, converts to JSON, and sends it to NOVA
            Console.WriteLine("Enter Ven ID:");
            
            string venid = Console.ReadLine();

            SetVenIDStruct venidstruct = new SetVenIDStruct(venid);

            string _venidstr = JsonConvert.SerializeObject(venidstruct, Formatting.Indented);
            string venidstr = _venidstr.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(venidstr);
            return httpCommander.Transact();

        }

        static string CreateOptSchedule()
        {
            Console.WriteLine("Enter optId:");
            string optId = Console.ReadLine();
            Console.WriteLine("Enter opt type (optOut/optIn)");
            string optType = Console.ReadLine();
            Console.WriteLine("Enter opt reason:");
            string optReason = Console.ReadLine();
            Console.WriteLine("Enter Market Context:");
            string marketContext = Console.ReadLine();
            Console.WriteLine("Enter resource Id:");
            string resourceID = Console.ReadLine();
            Console.WriteLine("Enter end device asset:");
            string endDevice = Console.ReadLine();

            int i = 0;
            List<OptSchedule.optAvailability> availabilities = new List<OptSchedule.optAvailability>();
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Start Time (Unix Epoch) for Availability #" + (i + 1).ToString() + " or stop if all availabilities have been entered");
                    string startTime = Console.ReadLine();
                    if (startTime.ToLower() == "stop")
                    {
                        break;
                    }


                    int st = Int32.Parse(startTime);

                    Console.WriteLine("Enter Number of Hours for Availability #" + (i + 1).ToString());
                    string duration = Console.ReadLine();
                    int dur = Int32.Parse(duration);
                    OptSchedule.optAvailability avail = new OptSchedule.optAvailability(st, dur);

                    availabilities.Add(avail);
                    i++;
                }catch(Exception ex)
                {
                    availabilities.Clear();
                    Console.WriteLine("Error in entry, retry");
                }
            }

            OptSchedule.optAvailability[] availArr = availabilities.ToArray();
            //OptScheduleParams osParams = new OptScheduleParams(optId, optType, optReason, marketContext, resourceID, endDevice, availArr);

            OptSchedule optSchedule = new OptSchedule("ven.createOptSchedule", optId, optType, optReason, marketContext, resourceID, endDevice, availArr);

            string _json = JsonConvert.SerializeObject(optSchedule, Formatting.Indented);
            string json = _json.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(json);

            return httpCommander.Transact();
        }

        static string CancelOptSchedule()
        {
            Console.WriteLine("Enter optId:");
            string optIdstr = Console.ReadLine();

          
            CancelOpt cancelOpt = new CancelOpt("ven.cancelOptSchedule", optIdstr);

            string _json = JsonConvert.SerializeObject(cancelOpt,Formatting.Indented);
            string json = _json.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(json);

            return httpCommander.Transact();

        }

        static string OptEvent()
        {
            Console.WriteLine("Enter Opt ID:");
            string optID = Console.ReadLine();
            Console.WriteLine("Enter Evend ID:");
            string eventID = Console.ReadLine();
            Console.WriteLine("Enter Opt Type (In/Out)");
            string inout = Console.ReadLine();
            string optType = "opt" + inout;
            Console.WriteLine("Enter Opt Reason:");
            string optReason = Console.ReadLine();
            Console.WriteLine("Enter resourceId:");
            string resourceId = Console.ReadLine();
            Console.WriteLine("Enter requestId:");
            string requestId = Console.ReadLine();

            OptEvent optEvent = new OptEvent("ven.eventOpt",optID,eventID,optType,optReason,resourceId,requestId);

            string _json = JsonConvert.SerializeObject(optEvent, Formatting.Indented);

            string json = _json.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(json);
            
            return httpCommander.Transact();
        }

        static string CreatedEvent()
        {
            Console.WriteLine("Enter event ID:");
            string eventID = Console.ReadLine();
            Console.WriteLine("Enter Opt Type (optIn/optOut):");
            string optType = Console.ReadLine();
            Console.WriteLine("Schedule Event (Y/N):");
            string sched = Console.ReadLine();
            bool scheduleEvent = false;
            if(sched.ToLower() == "y")
            {
                scheduleEvent = true;
            }
            else
            {
                scheduleEvent = false;
            }

            CreatedEvent createdEvent = new CreatedEvent("ven.createdEvent", eventID, optType, scheduleEvent);

            string _json = JsonConvert.SerializeObject(createdEvent, Formatting.Indented);

            string json = _json.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(json);

            return httpCommander.Transact();
           
        }

        static string RegisterReports()
        {
            return SendNoParamsRequest("ven.registerReports");
        }

        static string RegisteredNamespace()
        {
            return SendNoParamsRequest("my.custom.namespace");
        }

        static string SendNoParamsRequest(string _nameSpace)
        {
            string _jsonString = JsonConvert.SerializeObject(new NoParamsCMD(_nameSpace), Formatting.Indented);

            string jsonString = _jsonString.Replace("NameSpace", "namespace");

            HttpCommander httpCommander = new HttpCommander(jsonString);

            return (httpCommander.Transact());

        }

        static void SetupConsole()
        {
       

        }


        //Print out the Help menu for UI
        static void PrintMenu()
        {
            
            Console.WriteLine("===Command List===");
            Console.WriteLine("QR: Query Registration");
            Console.WriteLine("VS: Ven Status");
            Console.WriteLine("CPR: Create Party Registration");
            Console.WriteLine("XPR: Cancel Party Registration");
            Console.WriteLine("CR: Clear Registration");
            Console.WriteLine("XR: Clear Reports");
            Console.WriteLine("SV: Start Ven");
            Console.WriteLine("XV: Stop Ven");
            Console.WriteLine("RE: Request Event");
            Console.WriteLine("XE: Clear Events");
            Console.WriteLine("SVI: Set Ven ID");
            Console.WriteLine("CS: Create Opt Schedule");
            Console.WriteLine("XS: Cancel Opt Schedule");
            Console.WriteLine("OE: Opt Event");
            Console.WriteLine("CE: Created Event");
            Console.WriteLine("RR: Registered Reports");
            Console.WriteLine("RN: Registered Namespace");
        }
    }
}
