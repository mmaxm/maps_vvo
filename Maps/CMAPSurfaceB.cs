using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;
using GMap.NET;

namespace Maps
{
    public class CMAPSurfaceB : CMBaseAPSurface
    {
        public double Angle;
        public double Radius = 0;

        public CMAPSurfaceB()
        { 
        
        }

        public CMAPSurfaceB(CMAirPortInfo apinf)
        {
            mAPInf = apinf;
        }

        public void LoadPointsList()
        {
            //зеленая область
            mPointsList.Clear();

            for (int i = 90; i >= -90; i--)
                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse + i, Radius).Coordinates);

            for (int i = 90; i >= -90; i--)
                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse + i, Radius).Coordinates);
        }

        public double HeigthPlaneB(double DistanceToPlaneB,  CMAPSurfaceA SurfaceA)
        {
            double HeightB = 0;
            if (DistanceToPlaneB >= SurfaceA.Radius) ////высота конической плоскости, рамки - от горизонтальной плоскости до 6 км, если меньше - указываем высоту горизонтальной плоскости
                if (DistanceToPlaneB <= Radius)
                    HeightB = Angle * (DistanceToPlaneB - SurfaceA.Radius); 

            return SurfaceA.Height + HeightB;  
        }


        /// <summary>
        /// Проверка конической поверхности
        /// </summary>
        /// <param name="Input"></param>
        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, CMAPSurfaceA SurfaceA)
        {
            double distance = CMGeoBase.GetDistance(Input, CMGeoBase.GetZone(Input, Radius, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd), APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd);
            double height = HeigthPlaneB(distance, SurfaceA);
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Коническая поверхность";
            height += APInf.AirfieldHeight;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                if (CMGeoBase.IsPointInPolygon(SurfaceA.PointsList, Input.Coordinates)==false)
                {
                    if (height <= Input.Height)
                    {
                        IsObstacle = true;
                        dif = Input.Height - height;
                    }
                    CheckedResult.ResultText = string.Format("Попадает в коническую поверхность и {0} превышает допустимую высоту {1} ", ((IsObstacle == true) ? "" : "не"), ((IsObstacle == true) ? (string.Format(" на {0} / {1}", Math.Round((Input.Height - height), 2), Math.Round(height, 2))) : ""));
                }
            }
          
            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }

        /// <summary>
        /// Проверка конической поверхности
        /// </summary>
        /// <param name="Input"></param>
        public CMCGPCheckedResult CheckConeSurface__(CMCheckedGeoPoint Input, CMAPSurfaceA SurfaceA)
        {
            double distance = CMGeoBase.GetDistance(Input, CMGeoBase.GetZone(Input, Radius, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd), APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd);
            double height = HeigthPlaneB(distance, SurfaceA);
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Коническая поверхность";

            ////если попал, считаем высоту
            height += APInf.AirfieldHeight;

            if (distance <= Radius)
                if (distance > SurfaceA.Radius)
                {
                    if (height <= Input.Height)
                    {
                        IsObstacle = true;
                        dif = Input.Height - height;
                    }
                    CheckedResult.ResultText = string.Format("Попадает в коническую поверхность и {0} превышает допустимую высоту {1} ", ((IsObstacle == true) ? "" : "не"), ((IsObstacle == true) ? (string.Format(" на {0} / {1}", Math.Round((Input.Height - height), 2), Math.Round(height, 2))) : ""));

                }
            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }


    }
}
