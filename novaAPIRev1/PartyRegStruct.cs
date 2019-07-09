using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class PartyRegStruct
    {
        public string NameSpace = "ven.createPartyRegistration";
        public PartyRegParams parameters;


        public struct PartyRegParams
        {
            public bool includeIds;

            public PartyRegParams(bool _includeIds)
            {
                includeIds = _includeIds;
            }
        }
        private bool includeIds = false;

        public PartyRegStruct(bool _includeIDs)
        {
            includeIds = _includeIDs;
            parameters = new PartyRegParams(includeIds);
        }
    }
}
