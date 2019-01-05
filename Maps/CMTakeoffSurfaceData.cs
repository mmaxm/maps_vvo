using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using CLMGeo;

namespace Maps
{
    public class CMTakeoffSurfaceData : CMBaseAPSurface
    {
        
        public double Width1  =0;
        public double Width2 =0;
        public double Length1 =0;
        public double Length2 =0;
        

        public CMTakeoffSurfaceData()
        { 
            Width1 = 180;
            Width2 = 2000;
            WidthAngle = 0.125;
            HeightAngle = 0.016;
            Height = 2000;
            Length1 = 2000;
            Length2 = 15000;
            this.Offset = 150;
        }

        public CMTakeoffSurfaceData(double heightAngle, string surfaceName)
        {
            InitSurface();
            HeightAngle = heightAngle;
            SurfaceName = surfaceName;
        }

        void InitSurface()
        {
            Width1 = 180;
            Width2 = 2000;
            WidthAngle = 0.125;

            Height = 2000;
            Length1 = 2000;
            Length2 = 15000;
            this.Offset = 150;        
        
        }


        public CMTakeoffSurfaceData(CMAirPortInfo APInf)
        {
            LoadPointsList();
        }

        public void LoadPointsList()//CMAirPortInfo APInf)
        {
            //зеленая область
            PointLatLng RunwayEndWithOffset = CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.TrueCourse, -Offset);

            mPointsList.Clear();
            mPointsList.Add(CMGeoBase.GetCoordinate(RunwayEndWithOffset,APInf.CRW.RunwayEnd.TrueCourse - 90,Width1 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(RunwayEndWithOffset, APInf.CRW.RunwayEnd.TrueCourse - 180, (Width2 - Width1) / 2 / WidthAngle), APInf.CRW.RunwayEnd.TrueCourse - 90, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(RunwayEndWithOffset, APInf.CRW.RunwayEnd.TrueCourse - 180, Length2), APInf.CRW.RunwayEnd.TrueCourse - 90, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(RunwayEndWithOffset,  APInf.CRW.RunwayEnd.TrueCourse - 180, Length2), APInf.CRW.RunwayEnd.TrueCourse - 270, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(RunwayEndWithOffset, APInf.CRW.RunwayEnd.TrueCourse - 180, (Width2 - Width1) / 2 / WidthAngle), APInf.CRW.RunwayEnd.TrueCourse - 270, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(RunwayEndWithOffset, APInf.CRW.RunwayEnd.TrueCourse + 90, Width1 / 2));
        }

        public void LoadPointsList_()//CMAirPortInfo APInf)
        {
            //зеленая область

            PointLatLng RunwayEndWithOffset = CMGeoBase.OtstupVPP(-Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.GetCalcTrueCourse());

            mPointsList.Clear();
            mPointsList.Add(CMGeoBase.OtstupVPP(Width1 / 2, 1, RunwayEndWithOffset, APInf.CRW.GetCalcTrueCourse()));

            mPointsList.Add(CMGeoBase.OtstupVPP(Width2 / 2, 1, CMGeoBase.OtstupVPP((Width2 - Width1) / 2 / WidthAngle, APInf.CRW.TakeoffSurfaceDirection, RunwayEndWithOffset, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));


            mPointsList.Add(CMGeoBase.OtstupVPP(Width2 / 2, 1, CMGeoBase.OtstupVPP(Length2, APInf.CRW.TakeoffSurfaceDirection, RunwayEndWithOffset, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width2 / 2, 3, CMGeoBase.OtstupVPP(Length2, APInf.CRW.TakeoffSurfaceDirection, RunwayEndWithOffset, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));

            mPointsList.Add(CMGeoBase.OtstupVPP(Width2 / 2, 3, CMGeoBase.OtstupVPP((Width2 - Width1) / 2 / WidthAngle, APInf.CRW.TakeoffSurfaceDirection, RunwayEndWithOffset, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));

            mPointsList.Add(CMGeoBase.OtstupVPP(Width1 / 2, 3, RunwayEndWithOffset, APInf.CRW.GetCalcTrueCourse()));

        }

        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input)
        {

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = SurfaceName;//"Поверхность взлета";
            CheckedResult.PointHeight = Input.Height;

            if (CMGeoBase.IsPointInPolygon(PointsList, Input.Coordinates))
            {
                Visible = true;

                double Angle = Math.Abs(CMGeoBase.Azimut(APInf.CRW.RunwayEnd.Coordinates, Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayEnd.Coordinates));
                double _length = CMGeoBase.GetDistance(Input.Coordinates, APInf.CRW.RunwayEnd.Coordinates) * Math.Cos(Angle * CMGeoBase.DegToRad);
                CheckedResult.SurfaceHeight = _length * HeightAngle;

                CheckedResult.SurfaceHeight += APInf.CRW.RunwayBegin.Height;

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }
                CheckedResult.ResultText = string.Format("Попадает в {0} и {1} превышает допустимую высоту{2}", SurfaceName, (CheckedResult.IsObstacle == true ? "" : " не "), (CheckedResult.IsObstacle == true ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));
            }
            else
                Visible = false;
           
            return CheckedResult;
        }

    }
}
