using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMGeo;
using GMap.NET;

namespace Maps
{
    public class CMAPSurfaceA : CMBaseAPSurface
    {
        public double Radius = 0;
        public CMAPSurfaceA()
        { 
        
        
        }

        public CMAPSurfaceA(CMAirPortInfo apinf)
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

        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, CMAPSurfaceB SurfaceB)
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Горизонтальная поверхность";
            bool IsObstacle = false;
            double dif = 0;
            double height = Height + APInf.AirfieldHeight;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                if (height <= Input.Height)
                {
                    IsObstacle = true;
                    dif = Input.Height - height;
                }

                CheckedResult.ResultText = string.Format("Попадает во внутреннюю горизонтальную поверхность и {0} превышает допустимую высоту{1}", (IsObstacle == true ? "" : " не "), (IsObstacle == true ? string.Format(" на {0} / {1}", dif, height) : ""));
            }

            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;

            return CheckedResult;
        }

        public CMCGPCheckedResult CheckHorizSurface_(CMCheckedGeoPoint Input, CMAPSurfaceB SurfaceB)
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Горизонтальная поверхность";
            bool inZone = false;
            bool IsObstacle = false;
            double dif = 0;
            double height = Height + APInf.AirfieldHeight;
            int Zone = 0;



            List<PointLatLng> Plane1 = new List<PointLatLng>();
            Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse - 90, Radius).Coordinates);
            Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse + 90, Radius).Coordinates);
            Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse - 90, Radius).Coordinates);
            Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse + 90, Radius).Coordinates);

            if (CMGeoBase.IsPointInPolygon(Plane1, Input.Coordinates))
            {
                Zone = 1;
            }
            else
            {
                Plane1.Clear();
                Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse + 90, Radius).Coordinates);
                Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse - 90, Radius).Coordinates);
                Plane1.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse, Radius), APInf.CRW.RunwayEnd.BackTrueCourse - 90, Radius).Coordinates);
                Plane1.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, APInf.CRW.RunwayEnd.BackTrueCourse, Radius), APInf.CRW.RunwayEnd.BackTrueCourse + 90, Radius).Coordinates);
                
                if (CMGeoBase.IsPointInPolygon(Plane1, Input.Coordinates))
                {
                    Zone = 2;
                }
                else
                {
                    Plane1.Clear();
                    Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse + 90, Radius).Coordinates);
                    Plane1.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse + 90, Radius), APInf.CRW.RunwayBegin.BackTrueCourse, Radius).Coordinates);
                    Plane1.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse - 90, Radius), APInf.CRW.RunwayBegin.BackTrueCourse, Radius).Coordinates);
                    Plane1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, APInf.CRW.RunwayBegin.BackTrueCourse - 90, Radius).Coordinates);
                    if (CMGeoBase.IsPointInPolygon(Plane1, Input.Coordinates))
                    {
                        Zone = 3;
                    }
                }

            }

            switch (Zone)
            {
                case 1:
                    inZone = true;
                    break;
                case 2:
                    if (CMGeoBase.GetDistance(APInf.CRW.RunwayEnd, Input) <= Radius)
                        inZone = true;

                    break;
                case 3:
                    if (CMGeoBase.GetDistance(APInf.CRW.RunwayBegin, Input) <= Radius)
                        inZone = true;
                    break;
                default:
                    break;
            }

            if (inZone)
            {
                if (height <= Input.Height)
                {
                    IsObstacle = true;
                    dif = Input.Height - height;
                }

                CheckedResult.ResultText = string.Format("Попадает во внутреннюю горизонтальную поверхность и {0} превышает допустимую высоту{1}", (IsObstacle == true ? "" : " не "), (IsObstacle == true ? string.Format(" на {0} / {1}", dif, height) : ""));
            }



            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;

            return CheckedResult;
        }


        /// <summary>
        /// Проверка на вхождение точки во внутреннюю горизонтальную поверхность
        /// </summary>
        /// <param name="Input">Проверяемая точка</param>
        public CMCGPCheckedResult CheckHorizSurface___(CMCheckedGeoPoint Input, CMAPSurfaceB SurfaceB)
        {
            double distance = CMGeoBase.GetDistance(Input, CMGeoBase.GetZone(Input, SurfaceB.Radius, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd), APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd);

            double height = Height + APInf.AirfieldHeight; //HeightA + APInf.AirfieldHeight; //абсолютная высота внутренней горизонтальной поверхности

            bool IsObstacle = false;
            double dif = 0;

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Горизонтальная поверхность";

            if (distance <= Radius)
            {
                if (height <= Input.Height)
                {
                    IsObstacle = true;
                    dif = Input.Height - height;
                }

                CheckedResult.ResultText = string.Format("Попадает во внутреннюю горизонтальную поверхность и {0} превышает допустимую высоту{1}", (IsObstacle == true ? "" : " не "), (IsObstacle == true ? string.Format(" на {0} / {1}", dif, height) : ""));

            }


            CheckedResult.IsObstacle = IsObstacle;
            CheckedResult.PointHeight = Input.Height;
            CheckedResult.SurfaceHeight = height;
            CheckedResult.Exceeding = dif;

            return CheckedResult;
        }


    }
}
