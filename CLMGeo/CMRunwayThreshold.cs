using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;

namespace CLMGeo
{
    public class CMRunwayThreshold : CMGeoPoint
    {

        public double MagneticCourse = 0;
        public double TrueCourse = 0;
        public double Height1800 = 0;


        public CMRunwayThreshold()
        { 
            
        }
        
        public CMRunwayThreshold(enumGeoPointType geodottype)
            : base(enumGeoPointType.geoptRunwayThreshold)
        {

        }

        public CMRunwayThreshold(enumGeoPointType geodottype, double lat, double lng, double height = 0, double truecourse = 0, double magneticcourse = 0)
            : base(enumGeoPointType.geoptRunwayThreshold, lat, lng, height)
        {
            TrueCourse = truecourse;
            MagneticCourse = magneticcourse;
        }

        public CMRunwayThreshold(enumGeoPointType geodottype, double lat, double lng, double height = 0, double truecourse = 0, double magneticcourse = 0, string text = "")
            : base(enumGeoPointType.geoptRunwayThreshold, lat, lng, height,text)
        {
            TrueCourse = truecourse;
            MagneticCourse = magneticcourse;
        }

        public CMRunwayThreshold(enumGeoPointType geodottype, double lat, double lng, double height = 0, double truecourse = 0, double magneticcourse = 0, string text = "", double height1800 = 0)
            : base(enumGeoPointType.geoptRunwayThreshold, lat, lng, height, text)
        {
            TrueCourse = truecourse;
            MagneticCourse = magneticcourse;
            Height1800 = height1800;
        }


        public CMRunwayThreshold(enumGeoPointType geodottype, PointLatLng coordinates, double height = 0, double truecourse = 0, double magneticcourse = 0)
            : base(enumGeoPointType.geoptRunwayThreshold, coordinates, height)
        {
            TrueCourse = truecourse;
            MagneticCourse = magneticcourse;
        }

        public double BackTrueCourse
        {
            get 
            
            {
                //double result =   TrueCourse + (180 * ((TrueCourse >=180) ? -1 : 1));
                //if (result > 360)
                //    result -= 360;

                return TrueCourse + (180 * ((TrueCourse >= 180) ? -1 : 1));
            
            
            }
        }

        public double AvgHeight1800
        {
            get { return ((Height + Height1800)/2); }
        }

    }
}
