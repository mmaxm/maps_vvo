using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;


namespace Maps
{
    public class CMTransitionSurfaceData : CMBaseAPSurface
    {

          double  Offset2 = 0;
        List<PointLatLng> mPointsList2 = new List<PointLatLng>();

        public CMTransitionSurfaceData()
        {
            initvalue();
        }

        public CMTransitionSurfaceData(CMAirPortInfo apinf)
        {
            initvalue();
            mAPInf = apinf;
        }

        void initvalue()
        {
            HeightAngle = 0.143;
            Offset = 60;
            Offset2 = -150;
            Width = 300;
        }

        public void LoadPointsList(CMApproachSurfaceData ASDLink, double HsurfaceA)
        {
            mPointsList.Clear();
            //Плоскость №1


            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -Offset), APInf.CRW.RunwayBegin.TrueCourse + (90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), HsurfaceA / ASDLink.HeightAngle1 + (ASDLink.Offset)), APInf.CRW.RunwayBegin.TrueCourse + 90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1), ASDLink.Width / 2 + (HsurfaceA / ASDLink.HeightAngle1 * ASDLink.WidthAngle)));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), -Offset2), APInf.CRW.RunwayBegin.TrueCourse + (90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), ASDLink.Width / 2 + HsurfaceA / HeightAngle));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), -Offset2), APInf.CRW.RunwayBegin.TrueCourse + (90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), Width / 2));
            
            //Плоскость №1

            mPointsList2.Clear();
            //Плоскость №2

            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), Offset), APInf.CRW.RunwayBegin.TrueCourse + (90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? 1 : -1)), Width / 2));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), HsurfaceA / ASDLink.HeightAngle1 + (ASDLink.Offset)), APInf.CRW.RunwayBegin.TrueCourse + 90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? 1 : -1), ASDLink.Width / 2 + (HsurfaceA / ASDLink.HeightAngle1 * ASDLink.WidthAngle)));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), -Offset2), APInf.CRW.RunwayBegin.TrueCourse + (90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? 1 : -1)), ASDLink.Width / 2 + HsurfaceA / HeightAngle));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.TrueCourse + (180 * (APInf.CRW.TransitionSurfaceDirection == 4 ? -1 : 1)), -Offset2), APInf.CRW.RunwayBegin.TrueCourse + (90 * (APInf.CRW.TransitionSurfaceDirection == 4 ? 1 : -1)), Width / 2));

            //Плоскость №2
        }


         public void LoadPointsList_(CMApproachSurfaceData ASDLink, double HsurfaceA)
        {
            mPointsList.Clear();
            //Плоскость №1
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(ASDLink.Width / 2 + (HsurfaceA / ASDLink.HeightAngle1 * ASDLink.WidthAngle), 1, CMGeoBase.OtstupVPP(HsurfaceA / ASDLink.HeightAngle1 + ASDLink.Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(ASDLink.Width / 2 + HsurfaceA / HeightAngle, 1, CMGeoBase.OtstupVPP(Offset2, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(Offset2, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            //Плоскость №1


            mPointsList2.Clear();
            //Плоскость №2
            mPointsList2.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList2.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(Offset2, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList2.Add(CMGeoBase.OtstupVPP(ASDLink.Width / 2 + HsurfaceA / HeightAngle, 3, CMGeoBase.OtstupVPP(Offset2, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList2.Add(CMGeoBase.OtstupVPP(ASDLink.Width / 2 + (HsurfaceA / ASDLink.HeightAngle1 * ASDLink.WidthAngle), 3, CMGeoBase.OtstupVPP(HsurfaceA / ASDLink.HeightAngle1 + ASDLink.Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            //Плоскость №2

        }



        public List<PointLatLng> PointsList2
        {
            get { return mPointsList2; }
        }


        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input)
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Переходная поверхность";
            CheckedResult.PointHeight = Input.Height;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates) || CMGeoBase.IsPointInPolygon(mPointsList2, Input.Coordinates))
            {
                CheckedResult.SurfaceHeight  = HeightAngle * (Math.Abs(Input.Offset.Lng) - 150) + APInf.AirfieldHeight;

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }
                CheckedResult.ResultText = string.Format("Попадание в переходную плоскость и {0} превышает допустимую высоту{1}", (CheckedResult.IsObstacle == true ? "" : " не "), (CheckedResult.IsObstacle == true ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));
            }
            else
                Visible = false;
            
            return CheckedResult;
        }

        

    }
}
