using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;
using System.Device.Location;

namespace Maps
{
    public class CMInnerTransitionSurfaceData : CMBaseAPSurface
    {
        public double offset2 = 60;
        public double Length2 = 900;
        public double Width2 = 156;
        public double offset3 = 1800;
        public double offset4 = 1801.8;
        /// <summary>
        ///HeightAngle InnerApproachSurface
        /// </summary>
        public double HeightAngle2 = 0.02;
        /// <summary>
        /// HeightAngle CancelledApproachSurface
        /// </summary>
        public double HeightAngle3 = 0.0333;
        
        List<PointLatLng> mPointsList2 = new List<PointLatLng>();

        public CMInnerTransitionSurfaceData()
        {
            initvalue();
        }

        public CMInnerTransitionSurfaceData(CMAirPortInfo apinf)
        {
            initvalue();
            mAPInf = apinf;
        }

        void initvalue()
        {
            Height = 60;
            HeightAngle = 0.333;
            Width = 156;
        }

        double HeightAngle2Calc()
        {
            double at = CMBaseGeoPoint.RadToDegree(Math.Atan(HeightAngle));
            return Math.Tan(CMBaseGeoPoint.DegreeToRad(at - ((at / 90) * CMBaseGeoPoint.RadToDegree(Math.Atan(HeightAngle2)))));
        }

        double HeightAngle3Calc()
        {
            double at = CMBaseGeoPoint.RadToDegree(Math.Atan(HeightAngle));
            double result = Math.Tan(CMBaseGeoPoint.DegreeToRad(at - ((at / 90) * CMBaseGeoPoint.RadToDegree(Math.Atan(HeightAngle3)))));
            return result;
        }

        public void LoadPointsList(CMInnerApproachSurfaceData InnerApproachSurface, CMCancelledApproachSurfaceData CancelledApproachSurface)
        {
            mPointsList.Clear();

            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, offset3 + offset4), APInf.CRW.RunwayBegin.TrueCourse + 90, (Width / 2 + CancelledApproachSurface.Height / CancelledApproachSurface.HeightAngle * CancelledApproachSurface.WidthAngle)));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, offset3), APInf.CRW.RunwayBegin.TrueCourse + 90, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(offset2)), APInf.CRW.RunwayBegin.TrueCourse + 90, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(offset2 + Length2)), APInf.CRW.RunwayBegin.TrueCourse + 90, Width2 / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(InnerApproachSurface.Offset + InnerApproachSurface.Length)), APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2 + 42 / HeightAngle2Calc()));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(InnerApproachSurface.Offset)), APInf.CRW.RunwayBegin.TrueCourse + 90, Height / HeightAngle + Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, (CancelledApproachSurface.Offset)), APInf.CRW.RunwayBegin.TrueCourse + 90, (Height / HeightAngle + Width / 2)));

            mPointsList2.Clear();
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, offset3 + offset4), APInf.CRW.RunwayBegin.TrueCourse - 90, (Width / 2 + CancelledApproachSurface.Height / CancelledApproachSurface.HeightAngle * CancelledApproachSurface.WidthAngle)));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, (CancelledApproachSurface.Offset)), APInf.CRW.RunwayBegin.TrueCourse - 90, (Height / HeightAngle + Width / 2)));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(InnerApproachSurface.Offset)), APInf.CRW.RunwayBegin.TrueCourse - 90, Height / HeightAngle + Width / 2));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(InnerApproachSurface.Offset + InnerApproachSurface.Length)), APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2 + 42 / HeightAngle2Calc()));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(offset2 + Length2)), APInf.CRW.RunwayBegin.TrueCourse - 90, Width2 / 2));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -(offset2)), APInf.CRW.RunwayBegin.TrueCourse - 90, Width2 / 2));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, offset3), APInf.CRW.RunwayBegin.TrueCourse - 90, Width2 / 2));            
        
        }


        public void LoadPointsList_(CMInnerApproachSurfaceData InnerApproachSurface, CMCancelledApproachSurfaceData CancelledApproachSurface)
        {
            mPointsList.Clear();
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + 42 / HeightAngle, 1, CMGeoBase.OtstupVPP(InnerApproachSurface.Offset + InnerApproachSurface.Length, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse())); //1
            mPointsList.Add(CMGeoBase.OtstupVPP(Height / HeightAngle + Width / 2, 1, CMGeoBase.OtstupVPP(InnerApproachSurface.Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));//2
            mPointsList.Add(CMGeoBase.OtstupVPP(Height / HeightAngle + Width / 2, 1, CMGeoBase.OtstupVPP(CancelledApproachSurface.Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));//3
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + CancelledApproachSurface.Height / CancelledApproachSurface.HeightAngle * CancelledApproachSurface.WidthAngle, 1, CMGeoBase.OtstupVPP(3600, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));//4
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + CancelledApproachSurface.Height / CancelledApproachSurface.HeightAngle * CancelledApproachSurface.WidthAngle, 3, CMGeoBase.OtstupVPP(3600, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Height / HeightAngle + Width / 2, 3, CMGeoBase.OtstupVPP(CancelledApproachSurface.Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Height / HeightAngle + Width / 2, 3, CMGeoBase.OtstupVPP(InnerApproachSurface.Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + 42 / HeightAngle, 3, CMGeoBase.OtstupVPP(InnerApproachSurface.Offset + InnerApproachSurface.Length, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
        }

        public List<PointLatLng> PointsList2
        {
            get { return mPointsList2; }
        }

        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input, CMInnerApproachSurfaceData InnerApproachSurface, CMCancelledApproachSurfaceData CancelledApproachSurface)
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Внутренняя переходная поверхность";
            CheckedResult.PointHeight = Input.Height;

            List<PointLatLng> temp1 = new List<PointLatLng>();

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates) )
                temp1 = mPointsList; 
            else
                if (CMGeoBase.IsPointInPolygon(mPointsList2, Input.Coordinates))
                    temp1 = mPointsList2;

            if (temp1.Count > 0)
            {
                List<PointLatLng> temp2 = new List<PointLatLng>();

                temp2.Clear();
                temp2.Add(temp1[0]);
                temp2.Add(temp1[1]);
                temp2.Add(temp1[6]);

                if (CMGeoBase.IsPointInPolygon(temp2, Input.Coordinates))
                    CheckedResult.SurfaceHeight = HeightAngle3 * (Math.Abs(Input.Offset.Lat) - 1800.0) + HeightAngle3Calc() * (Math.Abs(Input.Offset.Lng) - Width2 / 2) + APInf.CRW.RunwayBegin.Height1800;
                else
                {
                    temp2.Clear();
                    temp2.Add(temp1[1]);
                    temp2.Add(temp1[2]);
                    temp2.Add(temp1[5]);
                    temp2.Add(temp1[6]);
                    if (CMGeoBase.IsPointInPolygon(temp2, Input.Coordinates))
                        CheckedResult.SurfaceHeight = HeightAngle * (Math.Abs(Input.Offset.Lng) - Width2 / 2) + APInf.CRW.RunwayBegin.AvgHeight1800;
                    else
                    {
                        temp2.Clear();
                        temp2.Add(temp1[2]);
                        temp2.Add(temp1[3]);
                        temp2.Add(temp1[4]);
                        temp2.Add(temp1[5]);
                        if (CMGeoBase.IsPointInPolygon(temp2, Input.Coordinates))
                            CheckedResult.SurfaceHeight = HeightAngle2 * (Math.Abs(Input.Offset.Lat) - offset2) + HeightAngle2Calc() * (Math.Abs(Input.Offset.Lng) - Width2 / 2) + APInf.CRW.RunwayBegin.Height;
                    }
                }

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }

                CheckedResult.ResultText = string.Format("Попадает во внутреннюю переходную поверхность и {0} превышает высоту{1}", (CheckedResult.IsObstacle ? "" : " не "), (CheckedResult.IsObstacle ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));
            }
            else
                Visible = false;
            
            return CheckedResult;
        }

        public CMCGPCheckedResult CheckInnerTransitionSurface_(CMCheckedGeoPoint Input, CMInnerApproachSurfaceData InnerApproachSurface,CMCancelledApproachSurfaceData CancelledApproachSurface)
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Внутренняя переходная поверхность";
            CheckedResult.PointHeight = Input.Height;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates) || CMGeoBase.IsPointInPolygon(mPointsList2, Input.Coordinates))
            {
                int misc = CMGeoBase.ZoneChecker(Input.Coordinates, mPointsList);
                double length = 0;
                double angle = 0;

                GeoCoordinate eCoord = new GeoCoordinate(Input.Lat, Input.Lng);
                PointLatLng temp1 = new PointLatLng();

                switch (misc)
                {
                    case 1:
                        {
                            temp1 = CMGeoBase.OtstupVPP(InnerApproachSurface.Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates);
                            GeoCoordinate sCoord = new GeoCoordinate(temp1.Lat, temp1.Lng);
                            length = eCoord.GetDistanceTo(sCoord);
                            angle = CMGeoBase.Azimut(temp1, Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayBegin.Coordinates);
                            //aa *= Math.Cos(Math.Abs(angle) * DegToRad);
                            double length2 = length * Math.Cos(Math.Abs(angle) * CMGeoBase.DegToRad);
                            double length3 = length * Math.Sin(Math.Abs(angle) * CMGeoBase.DegToRad);
                            CheckedResult.SurfaceHeight = length2 * InnerApproachSurface.HeightAngle;  //основание

                            double w1 = InnerApproachSurface.Width / 2 * 1000;
                            double w2 = Math.Abs(length3);
                            if (w2 > w1)
                                CheckedResult.SurfaceHeight += HeightAngle * (w2 - w1);
                            break;
                        }
                    case 2:
                        {
                            length = CMGeoBase.GetDistance(Input, 2, APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd);
                            if (length > Width / 2)
                                CheckedResult.SurfaceHeight = (length - Width / 2) * HeightAngle;

                            break;
                        }
                    case 3:
                        {
                            temp1 = CMGeoBase.OtstupVPP(CancelledApproachSurface.Offset, APInf.CRW.TakeoffSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates);
                            GeoCoordinate sCoord = new GeoCoordinate(temp1.Lat, temp1.Lng);
                            length = eCoord.GetDistanceTo(sCoord);
                            angle = CMGeoBase.Azimut(temp1, Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayEnd.Coordinates);
                            double length2 = length * Math.Cos(Math.Abs(angle) * CMGeoBase.DegToRad);
                            double length3 = length * Math.Sin(Math.Abs(angle) * CMGeoBase.DegToRad);
                            CheckedResult.SurfaceHeight = length2 * CancelledApproachSurface.HeightAngle; //основание

                            //new
                            double Width1 = 60 + length2 * 0.1;
                            double Width2 = Math.Abs(length3);

                            if (Width2 > Width1)
                                CheckedResult.SurfaceHeight += HeightAngle * (Width2 - Width1);
                            break;
                        }
                    default: break;
                }
                //
                CheckedResult.SurfaceHeight += APInf.CRW.RunwayBegin.Height;
                //
                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }

                CheckedResult.ResultText = string.Format("Попадает во внутреннюю переходную поверхность и {0} превышает высоту{1}", (CheckedResult.IsObstacle ? "" : " не "), (CheckedResult.IsObstacle ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));

            }
            else
                Visible = false;
            return CheckedResult;
        }




    }
}
