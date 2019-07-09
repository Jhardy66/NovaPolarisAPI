using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{

    class OptAvailability
    {

        public int dtstart;
        public int duration;

        public OptAvailability(int start,int _duration)
        {
            dtstart = start;
            duration = _duration;
        }
    }
}
