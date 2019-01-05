using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;
using GMap.NET;

namespace Maps
{
    public class CMBAPSurfaceNoise : CMBaseAPSurface
    {
        public double RadiusA = 0;
        public double RadiusB = 0;
        public double AzimutOffset = 0;


        public CMBAPSurfaceNoise()
        { 
        }

        public CMBAPSurfaceNoise(CMAirPortInfo apinf)
        {
            mAPInf = apinf;
        }

        public CMBAPSurfaceNoise(double radiusA, double radiusB, double azimutOffset, string surfaceName)
        {

            RadiusA = radiusA;
            RadiusB = radiusB;
            AzimutOffset = azimutOffset;
            SurfaceName = surfaceName;
        }


        public void LoadPointsList()
        {
            mPointsList.Clear();

            for (double fi = 0; fi < 360; fi += 1)
            {

                double distance = Math.Sqrt(Math.Pow((RadiusA * Math.Cos((fi * CMGeoBase.DegToRad))), 2) + Math.Pow((RadiusB * Math.Sin((fi * CMGeoBase.DegToRad))), 2));
                double Fi4Ellipse = GetFi4Ellipse(fi, RadiusA, RadiusB);
                //TextResult.Text += string.Format("< {0} << {1} d {2}", fi, Fi4Ellipse, distance) + " \r\n"; ;

                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, Fi4Ellipse, distance).Coordinates);
            }


        }

        double GetFi4Ellipse(double fi, double a, double b)
        {

            double Fi4Ellipse = 0;
            double X = a * Math.Cos((fi * CMGeoBase.DegToRad));
            double Y = -(b * Math.Sin((fi * CMGeoBase.DegToRad)));

            if (fi == 0 || fi == 90 || fi == 180 || fi == 270 || fi == 360)
                Fi4Ellipse = fi * CMGeoBase.DegToRad;
            else
            {

                if (fi > 0 && fi < 90)
                    Fi4Ellipse = Math.Atan(Math.Abs(Y / X));
                else if (fi > 90 && fi < 180)
                    Fi4Ellipse = (90 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(X / Y));
                else if (fi > 180 && fi < 270)
                    Fi4Ellipse = (180 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(Y / X));
                else if (fi > 270 && fi < 360)
                    Fi4Ellipse = (270 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(X / Y));
            }
            double _Fi4Ellipse = Fi4Ellipse * CMGeoBase.RadToDeg + AzimutOffset;
            return (_Fi4Ellipse > 360 ? _Fi4Ellipse - 360 : _Fi4Ellipse);
        }


        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input)
        {

            double height = 100;
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = SurfaceName;//string.Format("Поверхность R-{0}", Radius);
            height += APInf.AirfieldHeight;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                    CheckedResult.ResultText = string.Format("Попадает в {0}", SurfaceName);

            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }

        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, CMBAPSurfaceNoise APSurfaceNoiseInner)
        {

            double height = 100;
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = SurfaceName;//string.Format("Поверхность R-{0}", Radius);
            height += APInf.AirfieldHeight;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                if (CMGeoBase.IsPointInPolygon(APSurfaceNoiseInner.PointsList, Input.Coordinates) == false)
                {
                    CheckedResult.ResultText = string.Format("Попадает в {0}", SurfaceName);
                }
            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }


    }
}
