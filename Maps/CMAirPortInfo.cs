using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;


namespace Maps
{
    /// <summary>
    /// Базовая информация по аэропорту
    /// </summary>
    

    

    public class CMAirPortInfo
    {
        public CMGeoPoint KTA ;
        public double AirfieldHeight = 0; // высшая точка на ВПП
        public CMAPRunways Runways;
        
        public CMAirPortInfo ()
        {
            KTA = new CMGeoPoint();
            Runways = new CMAPRunways();
        }

        public CMAPRunway CRW
        {
            get { return Runways.Current(); }
        }
    }
}
