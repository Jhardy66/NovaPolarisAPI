using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novaAPIRev1
{
    class CancelOpt
    {
        public string NameSpace;
        public cancelOptParams parameters;

        public struct cancelOptParams
        {
            public string optId;

            public cancelOptParams(string _optId)
            {
                optId = _optId;
            }
        }
        public CancelOpt(string _namespace, string _params)
        {
            NameSpace = _namespace;
            parameters = new cancelOptParams(_params);
        }
    }
}
