using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CLMGeo
{
    public class CMBaseGeoPoint 
    {
        public enumGeoPointType GeoDotType;
        public string Description = "";
        public int ItemNom = -1;

        
        public CMBaseGeoPoint()
        {
            GeoDotType = enumGeoPointType.geoptNull;
            
        }

        public CMBaseGeoPoint(enumGeoPointType geodottype)
        {
            GeoDotType = geodottype;
        }
        
        public CMBaseGeoPoint(enumGeoPointType geodottype, int itemNom)
        {
            GeoDotType = geodottype;
            ItemNom = itemNom;
        }


        /// <summary>
        /// Вернуть координаты с дробной частью
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="Min"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static double ShowDegreeFraction(double inDegree, double inMinutes, double inSeconds)
        {

            return Math.Truncate(((inSeconds / 60 + inMinutes) / 60 + inDegree) * 1000000000000) / 1000000000000;
        }

        /// <summary>
        /// Вернуть красиво оформленные координаты
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="Min"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static string ShowDegMiSec(double inDegree, double inMinutes, double inSeconds)
        {
            return String.Format("{0}°{1}'{2}''", inDegree, inMinutes, inSeconds);
        }

        /// <summary>
        /// Вернуть красиво оформленные координаты
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="Min"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static string ShowDegMiSec(double inGeoCoord)
        {

            double degree = 0;
            double minutes = 0;
            double seconds = 0;

            degree = Math.Truncate(inGeoCoord);

            minutes = inGeoCoord % 1 * 60;
            seconds = Math.Round(minutes % 1 * 60, 2);
            minutes = Math.Truncate(minutes);

            return ShowDegMiSec(degree, minutes, seconds);
        }

        public static double GetDegree(double inGeoCoord)
        {
            return Math.Truncate(inGeoCoord);
        }

        public static double GetMinutes(double inGeoCoord)
        {
            return Math.Truncate(inGeoCoord % 1 * 60);
        }

        public static double GetSeconds(double inGeoCoord)
        {
            return Math.Round((inGeoCoord % 1 * 60) % 1 * 60, 2);
        }


        public static double DegreeToRad(double value)
        {
            return Math.PI * value / 180;
        }

        public static double RadToDegree(double value)
        {
            return value * 180 / Math.PI;
        }

    }


}
