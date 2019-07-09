using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    //Structure for the basic NOVA commands that don't need any parameters
    class NoParamsCMD

    {
        public string NameSpace;
        public emptyParams parameters = new emptyParams();

        public struct emptyParams
        {
        
        }
        public NoParamsCMD(string nameSpace)
        {
            NameSpace = nameSpace;
        }

    }
}
