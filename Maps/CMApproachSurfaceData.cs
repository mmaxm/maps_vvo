using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;


namespace Maps
{
    public class CMApproachSurfaceData : CMBaseAPSurface
    {

        public double HeightAngle1;// = 0.02;
        public double HeightAngle2 ;//= 0.025;
        public double Length1 ;//= 3000;
        public double Length2 ;//= 3600;
        public double Length3 ;//= 8400;
        //public bool visible = true;


        public CMApproachSurfaceData() 
        {
            initvalue();
        }

        public CMApproachSurfaceData(CMAirPortInfo apinf)
        {
            initvalue();
            mAPInf = apinf;
        }

        void initvalue()
        {
            Width = 300;
            WidthAngle = 0.15;
            Offset = 60;
            HeightAngle1 = 0.02;
            HeightAngle2 = 0.025;
            Length1 = 3000;
            Length2 = 3600;
            Length3 = 8400;
        }

        public void LoadPointsList()//CMAirPortInfo APInf)
        {
            mPointsList.Clear();

            PointLatLng _n = CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, -Offset);
            //mPointsList.Add(_n);
            mPointsList.Add(CMGeoBase.GetCoordinate(_n, APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(_n, APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2));

            PointLatLng _m1 = CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, (Offset + Length1) * -1);
            mPointsList.Add(CMGeoBase.GetCoordinate(_m1, APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2 + WidthAngle * (Length1)));

            PointLatLng _m2 = CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, (Offset + Length1 + Length2) * -1);
            mPointsList.Add(CMGeoBase.GetCoordinate(_m2, APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2 + WidthAngle * (Length1 + Length2)));

            PointLatLng _m3 = CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.TrueCourse, (Offset + Length1 + Length2 + Length3) * -1);
            mPointsList.Add(CMGeoBase.GetCoordinate(_m3, APInf.CRW.RunwayBegin.TrueCourse - 90, Width / 2 + WidthAngle * (Length1 + Length2 + Length3)));

            mPointsList.Add(CMGeoBase.GetCoordinate(_m3, APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2 + WidthAngle * (Length1 + Length2 + Length3)));
            mPointsList.Add(CMGeoBase.GetCoordinate(_m2, APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2 + WidthAngle * (Length1 + Length2)));
            mPointsList.Add(CMGeoBase.GetCoordinate(_m1, APInf.CRW.RunwayBegin.TrueCourse + 90, Width / 2 + WidthAngle * (Length1)));

        }


        public void LoadPointsList_()//CMAirPortInfo APInf)
        {
            mPointsList.Clear();

            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 1, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2, 3, CMGeoBase.OtstupVPP(Offset, APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + WidthAngle * (Length1), 3, CMGeoBase.OtstupVPP((Offset + Length1), APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + WidthAngle * (Length1 + Length2), 3, CMGeoBase.OtstupVPP((Offset + Length1 + Length2), APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + WidthAngle * (Length1 + Length2 + Length3), 3, CMGeoBase.OtstupVPP((Offset + Length1 + Length2 + Length3), APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + WidthAngle * (Length1 + Length2 + Length3), 1, CMGeoBase.OtstupVPP((Offset + Length1 + Length2 + Length3), APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + WidthAngle * (Length1 + Length2), 1, CMGeoBase.OtstupVPP((Offset + Length1 + Length2), APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));
            mPointsList.Add(CMGeoBase.OtstupVPP(Width / 2 + WidthAngle * (Length1), 1, CMGeoBase.OtstupVPP((Offset + Length1), APInf.CRW.TransitionSurfaceDirection, APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.GetCalcTrueCourse()), APInf.CRW.GetCalcTrueCourse()));

        }



        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input)
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Поверхность захода на посадку";
            CheckedResult.PointHeight = Input.Height;

            if (CMGeoBase.IsPointInPolygon(PointsList, Input.Coordinates))
            {
                Visible = true;

                double angle = Math.Abs(CMGeoBase.Azimut(APInf.CRW.RunwayBegin.Coordinates, Input.Coordinates) - CMGeoBase.Azimut(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayBegin.Coordinates));
                double length = CMGeoBase.GetDistance(Input.Coordinates, APInf.CRW.RunwayBegin.Coordinates) * Math.Cos(angle * CMGeoBase.DegToRad);


                if (length <= Length1)
                    CheckedResult.SurfaceHeight = (length - Offset) * HeightAngle1;
                else
                    if (length > Length1 && length <= (Length1 + Length2))
                        CheckedResult.SurfaceHeight = (length - (Length1 + Offset)) * HeightAngle2 + Length1 * HeightAngle1;
                    else
                    {
                        CheckedResult.SurfaceHeight = Length2 * HeightAngle2 + Length1 * HeightAngle1;
                    }

                CheckedResult.SurfaceHeight += APInf.CRW.RunwayBegin.Height; ;

                if (CheckedResult.SurfaceHeight <= Input.Height)
                {
                    CheckedResult.IsObstacle = true;
                    CheckedResult.Exceeding = Input.Height - CheckedResult.SurfaceHeight;
                }
                //TextResult.Text += height <= DataStorage.Height ? "Попадает в область захода на посадку и превышает допустимую высоту на "
                //                     + (DataStorage.Height - height).ToString() + " / " + height.ToString() + Environment.NewLine : "Попадает в область захода на посадку и не превышает допустимую высоту" + Environment.NewLine;
                CheckedResult.ResultText = string.Format("Попадает в область захода на посадку и {0} превышает допустимую высоту{1}", (CheckedResult.IsObstacle == true ? "" : " не "), (CheckedResult.IsObstacle == true ? string.Format(" на {0} / {1}", CheckedResult.Exceeding, CheckedResult.SurfaceHeight) : ""));

             //   CheckedResult.ResultText = string.Format("Попадает во внутреннюю горизонтальную поверхность и {0} превышает допустимую высоту{1}", (IsObstacle == true ? "" : " не "), (IsObstacle == true ? string.Format(" на {0} / {1}", dif, height) : ""));
            }
            else
                Visible = false;


            return CheckedResult;
        }

    }

}
