using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class OptSchedule
    {
        public string NameSpace;
        public optScheduleParams parameters;

        public struct optScheduleParams
        {
            public string optId;
            public string optType;
            public string optReason;
            public string marketContext;
            public string resourceId;
            public string endDeviceAsset;
            public optAvailability[] availability;

           public optScheduleParams(string _optId,string _optType,string _optReas,string _markCont,string resId,string _endDev, optAvailability[] availArr)
            {
                optId = _optId;
                optType = _optType;
                optReason = _optReas;
                marketContext = _markCont;
                resourceId = resId;
                endDeviceAsset = _endDev;
                availability = availArr;
            }

        }
           
        public struct optAvailability
        {
            public int dtstart;
            public int duration;

            public optAvailability(int start,int _duration)
            {
                dtstart = start;
                duration = _duration;
            }
        }
        public OptSchedule(string _namespace, string _optId,string _optType,string _optReas,string _markCont,string resId,string _endDev,optAvailability[] availArr)
        {
            NameSpace = _namespace;
            parameters = new optScheduleParams(_optId, _optType, _optReas,_markCont, resId, _endDev,availArr);
        }
    }
}
