using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    
    class OptEvent
    {
        public string NameSpace;
        public parameter parameters;
        public struct parameter
        {
            public string optId;
            public string eventId;
            public string optType;
            public string optReason;
            public string resourceId;
            public string requestId;

            public parameter(string _optId,string _eventId,string _optType,string _optReason,string _resourceId,string _requestId)
            {
                optId = _optId;
                eventId = _eventId;
                optType = _optType;
                optReason = _optReason;
                resourceId = _resourceId;
                requestId = _requestId;

            }
        }

      

        public OptEvent(string nameSpace, string _optId, string _eventId, string _optType, string _optReason, string _resourceId, string _requestId)
        {
            NameSpace = nameSpace;
            parameters = new parameter(_optId, _eventId, _optType, _optReason, _resourceId, _requestId);
            
        }
    }
}
