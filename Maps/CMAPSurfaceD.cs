using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;
using GMap.NET;

namespace Maps
{
    public class CMAPSurfaceD : CMBaseAPSurface
    {
        public double Radius = 0;
        public double AzimutP = 0.0;

        public CMAPSurfaceD()
        {

            AzimutP = 23.694;
        }

        public CMAPSurfaceD(CMAirPortInfo apinf)
        {
            mAPInf = apinf;
            AzimutP = 23.694;
            //AzimutM = 360 - AzimutP;
        }

        /// <summary>
        /// Проверка конической поверхности
        /// </summary>
        /// <param name="Input"></param>
        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, CMAPSurfaceC SurfaceC)
        {

            double height = 100;
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Граница района аэродрома";
            height += APInf.AirfieldHeight;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                if (CMGeoBase.IsPointInPolygon(SurfaceC.PointsList, Input.Coordinates) == false)
                {
                    CheckedResult.ResultText = "Попадает в границы района аэродрома";
                }
            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }

        public void LoadPointsListO()
        {
            mPointsList.Clear();
            for (int i = 0; i < 360; i++)
                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, i, Radius).Coordinates);
        }


        public void LoadPointsList()
        {
            mPointsList.Clear();
            mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, APInf.CRW.RunwayThreshold1.TrueCourse +180 + AzimutP, Radius).Coordinates);
            mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, APInf.CRW.RunwayThreshold1.TrueCourse - AzimutP, Radius).Coordinates);


            for (int i = (int)Math.Round(APInf.CRW.RunwayThreshold1.TrueCourse - AzimutP)+1; i < (int)Math.Round(APInf.CRW.RunwayThreshold1.TrueCourse + 180 + AzimutP); i++)
                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, i, Radius).Coordinates);
        }


    }
}
