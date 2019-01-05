using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMGeo
{
    public class CMGeoDot
    {
        public enumGeoDotType GeoDotType;
        public string Description = "";

        public CMGeoDot()
        {
            GeoDotType = enumGeoDotType.geodtNull;
        }

        public CMGeoDot(enumGeoDotType geodottype)
        {
            GeoDotType = geodottype;
        }



    }
}
