using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;


namespace Maps
{
    public class CMShowGeoCoord
    {
        double Degree;
        double Minutes;
        double Seconds;
      //  double FullView;
        /// <summary>
        /// Create new ex and fill data
        /// </summary>
        /// <param name="Inp">degrees in simple view</param>
        /// 
        public CMShowGeoCoord()
        {         }

        public CMShowGeoCoord(double inValue)
        {
           // this.FullView = Inp;

            this.Degree = Convert.ToDouble(Math.Truncate(inValue));
            this.Minutes = (inValue - this.Degree) * 60;
            this.Seconds = Math.Round((this.Minutes - Convert.ToDouble(Math.Truncate(this.Minutes))) * 60, 2);
            this.Minutes = Convert.ToDouble(Math.Truncate(this.Minutes));
        }

                /// <summary>
        /// Create new ex and fill data
        /// </summary>
        /// <param name="InpGr">Degrees</param>
        /// <param name="InpMin">Minutes</param>
        /// <param name="InpSec">Seconds</param>
        public CMShowGeoCoord(double inDegree, double inMinutes, double inSeconds)
        {
            this.Degree = inDegree;
            this.Minutes = inMinutes;
            this.Seconds = inSeconds;

        }

        /// <summary>
        /// Show full view
        /// </summary>
        /// <returns>double data</returns>
        public double ShowFull()
        {
            //return FullView;
            return CMBaseGeoPoint.ShowDegreeFraction(Degree, Minutes, Seconds);
        }
        /// <summary>
        /// Show Simple view
        /// </summary>
        /// <returns>string data</returns>
        public string ShowSimple()
        {
            //return String.Format("{0}°{1}'{2}''", this.Gr, this.Min, this.Sec);
            return CMBaseGeoPoint.ShowDegMiSec(this.Degree, this.Minutes, this.Seconds);
        }

    }
}
