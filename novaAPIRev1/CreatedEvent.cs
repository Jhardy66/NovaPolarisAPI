using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class CreatedEvent
    {
        public string NameSpace;
        public param parameters;
        public struct param
        {
            public string eventId;
            public string optType;
            public bool scheduleEvent;

            public param(string _eventId,string _optType,bool _scheduleEvent)
            {
                eventId = _eventId;
                optType = _optType;
                scheduleEvent = _scheduleEvent;
            }
        }

        public CreatedEvent(string _nameSpace,string _eventId,string _optType, bool _scheduleEvent)
        {
            NameSpace = _nameSpace;

            parameters = new param(_eventId, _optType, _scheduleEvent);
        }
    }
}
