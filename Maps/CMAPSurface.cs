using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;

namespace Maps
{
    public class CMAPSurface 
    {
        public double Angle ;
        public double Height ;
        public double Radius ;

        protected CMAirPortInfo mAPInf;

        public CMAPSurface()
        { 
        
        }

        public CMAirPortInfo APInf
        {
            get { return mAPInf; }
            set { mAPInf = value; }
        }

    }
}
