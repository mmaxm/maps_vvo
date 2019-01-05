using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;
using System.Device.Location;

namespace Maps
{
    public class CMCancelledApproachSurfaceData : CMBaseAPSurface
    {
        

        public CMCancelledApproachSurfaceData()
        {
            initvalue();
        }
        
        void initvalue()
        {
            Width = 156;//120;
            Offset = 1800;
            WidthAngle = 0.1;
            HeightAngle = 0.0333;
            Height = 60;
        }

        public CMCancelledApproachSurfaceData(CMAirPortInfo apinf)
        {
            initvalue();
            mAPInf = apinf;
        }

        public void LoadPointsList()//CMAirPortInfo APInf)
        {
            mPointsList.Clear();
            //красная трапеция рядом с зеленой
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, Offset), APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, Offset + Height / HeightAngle), APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2 + Height / HeightAngle * WidthAngle));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, Offset + Height / HeightAngle), APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2 + Height / HeightAngle * WidthAngle));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, Offset), APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2));
        }

        public void LoadPointsList_()//CMAirPortInfo APInf)
        {
            mPointsList.Clear();
            //красная трапеция рядом с зеленой
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + Height / HeightAngle * WidthAngle, 1, CMGeoBase.OtstupVPP(Offset + Height / HeightAngle, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + Height / HeightAngle * WidthAngle, 3, CMGeoBase.OtstupVPP(Offset + Height / HeightAngle, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
        }



        public CMCGPCheckedResult CheckCancelledApproachSurface(CMCheckedGeoPoint Input)
        {

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Поверхность прерванной посадки";
            CheckedResult.PointHeight = Input.Height;


            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {

                PointLatLng temp = CMGeoBase.OtstupVPP(Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates);

                GeoCoordinate sCoord = new GeoCoordinate(temp.Lat, temp.Lng);
                GeoCoordinate eCoord = new GeoCoordinate(Input.Lat, Input.Lng);
                double length = sCoord.GetDistanceTo(eCoord);
                double angle = CMGeoBase.Azimut(temp, Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayEnd.Coordinates);

                length *= Math.Cos(Math.Abs(angle) * CMGeoBase.DegToRad);
                CheckedResult.SurfaceHeight = length * HeightAngle;
                CheckedResult.SurfaceHeight  += APInf.CRW.RunwayBegin.Height;

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }

                CheckedResult.ResultText = string.Format("Попадает в область прерванной посадки и {0} превышает допустимую высоту{1}", (CheckedResult.IsObstacle == true ? "" : " не "), (CheckedResult.IsObstacle == true ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));
            }
            else
                Visible = false;

            return CheckedResult;
        }


        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, double X)
        {

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Поверхность прерванной посадки";
            CheckedResult.PointHeight = Input.Height;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {

                PointLatLng temp = CMGeoBase.OtstupVPP(Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates);

                GeoCoordinate sCoord = new GeoCoordinate(temp.Lat, temp.Lng);
                GeoCoordinate eCoord = new GeoCoordinate(Input.Lat, Input.Lng);
                double length = sCoord.GetDistanceTo(eCoord);
                double angle = CMGeoBase.Azimut(temp, Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayEnd.Coordinates);

                length *= Math.Cos(Math.Abs(angle) * CMGeoBase.DegToRad);
                CheckedResult.SurfaceHeight = HeightAngle * (Math.Abs(X) - 1800) + APInf.AirfieldHeight;

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }

                CheckedResult.ResultText = string.Format("Попадает в область прерванной посадки и {0} превышает допустимую высоту{1}", (CheckedResult.IsObstacle == true ? "" : " не "), (CheckedResult.IsObstacle == true ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));
            }
            else
                Visible = false;
            return CheckedResult;
        }

    }
}
