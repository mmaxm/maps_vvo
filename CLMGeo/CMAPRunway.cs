using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;

namespace CLMGeo
{
    public class CMAPRunway
    {
        public string Name {get ; set;}
        public double Length=0;
        public double Width=0;
        public bool Directional = true;
        CMRunwayThreshold mRunwayThreshold1;
        CMRunwayThreshold mRunwayThreshold2;

        public CMAPRunway()
        { 
        
        }

        public CMAPRunway(string name,double length,double width)
        {
            Name = name;
            Length = length;
            Width = width;
        }

        public CMAPRunway(string name, double length, double width, CMRunwayThreshold runwaythreshold1, CMRunwayThreshold runwaythreshold2)
        {
            Name = name;
            Length = length;
            Width = width;
            mRunwayThreshold1 = runwaythreshold1;
            mRunwayThreshold2 = runwaythreshold2;
        }

        /// <summary>
        /// Первый порог ВПП, у которого азимут, относительно центра ВПП, меньше
        /// </summary>
        public CMRunwayThreshold RunwayThreshold1
        {
            get { return mRunwayThreshold1 != null ? mRunwayThreshold1 : new CMRunwayThreshold(); }
            set { mRunwayThreshold1 = value; }
        }

        /// <summary>
        /// Второй порог ВПП, у которого азимут, относительно центра ВПП, больше
        /// </summary>
        public CMRunwayThreshold RunwayThreshold2
        {
            get { return mRunwayThreshold2 != null ? mRunwayThreshold2 : new CMRunwayThreshold();  }
            set { mRunwayThreshold2 = value; }
        }

        public CMRunwayThreshold RunwayBegin
        {
            get 
            {
                if (Directional == true)
                    return RunwayThreshold1;
                else
                    return RunwayThreshold2;
            }
        }

        public CMRunwayThreshold RunwayEnd
        {
            get
            {
                if (Directional == true)
                    return RunwayThreshold2;
                else
                    return RunwayThreshold1;
            }
        }

        public int TakeoffSurfaceDirection
        {
            get { return (Directional == true) ? 2 : 4; }
        }

        public int TransitionSurfaceDirection
        {
            get { return (Directional == true) ? 4 : 2; }
        }

        public List<PointLatLng> PointsList()
        {
            List<PointLatLng> mPointsList = new List<PointLatLng>();

            //mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(RunwayBegin.Coordinates, RunwayBegin.TrueCourse, -(InnerApproachSurface.Offset + InnerApproachSurface.Length)), APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2 + 42 / HeightAngle));


            mPointsList.Add(CMGeoBase.GetCoordinate(RunwayBegin.Coordinates,RunwayBegin.TrueCourse + 90,Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(RunwayEnd.Coordinates, RunwayEnd.TrueCourse - 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(RunwayEnd.Coordinates, RunwayEnd.TrueCourse + 90, Width / 2));
            //mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayBegin.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));
            //mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayEnd.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));
            //mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayEnd.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));
            //mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayBegin.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));

            mPointsList.Add(CMGeoBase.GetCoordinate(RunwayBegin.Coordinates, RunwayBegin.TrueCourse - 90, Width / 2));
            return mPointsList; 
        }

        public List<PointLatLng> PointsListO()
        {
            List<PointLatLng> mPointsList = new List<PointLatLng>();

            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayBegin.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayEnd.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayEnd.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(0, TransitionSurfaceDirection, RunwayBegin.Coordinates, GetCalcTrueCourse()), GetCalcTrueCourse()));

            return mPointsList;
        }


        public double GetCalcTrueCourse()
        {
            
            //return 61.03833333;
            
            return CMGeoBase.Azimut(mRunwayThreshold1.Coordinates, mRunwayThreshold2.Coordinates);
            //return CMGeoBase.Azimut_m(mRunwayThreshold2.Coordinates, mRunwayThreshold1.Coordinates);
        }

    }
}
