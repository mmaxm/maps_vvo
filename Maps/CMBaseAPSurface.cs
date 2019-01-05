using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;


namespace Maps
{
    public class CMBaseAPSurface
    {
        public double Height = 0;
        public double HeightAngle = 0;
        public double Length = 0;//900;
        public double Offset = 0;//60;
        public bool Visible = true;
        public double Width = 0;
        public double WidthAngle = 0;
        public string SurfaceName = ""; 

        protected CMAirPortInfo mAPInf;
        protected List<PointLatLng> mPointsList = new List<PointLatLng>();

        public CMBaseAPSurface()
        { 
        
        }

        public List<PointLatLng> PointsList
        {
            get { return mPointsList; }
        }

        public CMAirPortInfo APInf
        {
            get { return mAPInf; }
            set { mAPInf = value; }
        }

    }

}
