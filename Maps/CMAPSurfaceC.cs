using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;
using GMap.NET;

namespace Maps
{
    public class CMAPSurfaceC : CMBaseAPSurface
    {
        public double Radius = 0;
        public CMAPSurfaceC()
        { 
        
        
        }

        public CMAPSurfaceC(double radius, string surfaceName)
        {
            Radius = radius;
            SurfaceName = surfaceName;
        }

        public CMAPSurfaceC(CMAirPortInfo apinf)
        {
            mAPInf = apinf;
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
            CheckedResult.SurfaceName = SurfaceName;// string.Format("Поверхность R-{0}", Radius);
            height += APInf.AirfieldHeight;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                if (CMGeoBase.IsPointInPolygon(SurfaceC.PointsList, Input.Coordinates) == false)
                {
                    CheckedResult.ResultText = string.Format("Попадает в {0} ", SurfaceName);
                }
            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }

        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input)
        {

            double height = Height + APInf.AirfieldHeight;
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = SurfaceName;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {

                    if (height <= Input.Height)
                    {
                        IsObstacle = true;
                        dif = Input.Height - height;
                    }
                    CheckedResult.ResultText = string.Format("Попадает в {0} и {1} превышает допустимую высоту {2} ", SurfaceName, ((IsObstacle == true) ? "" : "не"), ((IsObstacle == true) ? (string.Format(" на {0} / {1}", Math.Round((Input.Height - height), 2), Math.Round(height, 2))) : ""));
            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }


        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, CMAPSurfaceB SurfaceB)
        {

            double height = Height + APInf.AirfieldHeight;
            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = SurfaceName;


            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                if (CMGeoBase.IsPointInPolygon(SurfaceB.PointsList, Input.Coordinates) == false)
                {

                    CheckedResult.ResultText = string.Format("Попадает в {0}" , SurfaceName);
                    if (Height > 0)
                    {
                        if (height <= Input.Height)
                        {
                            IsObstacle = true;
                            dif = Input.Height - height;
                        }
                        CheckedResult.ResultText += string.Format(" и {0} превышает допустимую высоту {1} ", ((IsObstacle == true) ? "" : "не"), ((IsObstacle == true) ? (string.Format(" на {0} / {1}", Math.Round((Input.Height - height), 2), Math.Round(height, 2))) : ""));
                    }

                }
            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }

        

        public void LoadPointsList()
        {
            mPointsList.Clear();

            for (int i = 0; i < 360; i++)
                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, i, Radius).Coordinates);
        }

    }
}

