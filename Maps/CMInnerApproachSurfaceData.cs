using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;


namespace Maps
{
    public class CMInnerApproachSurfaceData : CMBaseAPSurface
    {              

        public CMInnerApproachSurfaceData()
        {
            initvalue();
        }

        public CMInnerApproachSurfaceData(CMAirPortInfo apinf)
        {
            initvalue();
            mAPInf = apinf;
        }

        void initvalue()
        {
            Width = 156; // 120 - обычно, 156 - для ВПП класса а, с размахом крыла 65-75 метров
            Offset = 60;
            Length = 900;
            HeightAngle = 0.02;
        }

        public void LoadPointsList()
        {
            mPointsList.Clear();
 
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -Offset), APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -Offset), APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(Offset + Length)), APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(Offset + Length)), APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2));
        }


        public void LoadPointsList_()
        {
            mPointsList.Clear();

            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(Offset + Length, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(Offset + Length, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
        }


        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input)
        {

            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Внутренняя поверхность захода на посадку";
            CheckedResult.PointHeight = Input.Height;


            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                double length = CMGeoBase.GetDistance(Input.Coordinates, APInf.CRW.RunwayBegin.Coordinates) - Offset;
                double angle = Math.Abs(CMGeoBase.Azimut(CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates), Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayBegin.Coordinates));
                length *= Math.Cos(angle * CMGeoBase.DegToRad);
                CheckedResult.SurfaceHeight = length * HeightAngle;
                //
                CheckedResult.SurfaceHeight += APInf.CRW.RunwayBegin.Height;
                //
                //TextResult.Text += CheckedResult.SurfaceHeight <= Input.Height ? "Попадает во внутренюю поверхность захода на посадку и превышает высоту на :" + (Input.Height - height).ToString() + " / " + height.ToString() + Environment.NewLine : " Попадает во внутренюю поверхность захода на посадку и не превышает высоту" + Environment.NewLine;

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle  = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }
                CheckedResult.ResultText = string.Format("Попадает во внутренюю поверхность захода на посадку и {0} превышает высоту{1}", (CheckedResult.IsObstacle ? "" : " не "), (CheckedResult.IsObstacle ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));
            }
            else
                Visible = false;

            
            return CheckedResult;
        }
    }
}
