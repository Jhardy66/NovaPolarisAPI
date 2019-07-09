using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class SetVenIDStruct
    {
        public string NameSpace = "ven.setVenId";
        public VenIDParams parameters;


        public struct VenIDParams
        {
            public string venId;

            public VenIDParams(string _venID)
            {
                venId = _venID;
            }
        }
        public SetVenIDStruct(string _venid)
        {
            parameters = new VenIDParams(_venid);
        }
    }
}
