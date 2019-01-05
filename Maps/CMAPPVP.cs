using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using CLMGeo;


namespace Maps
{
    public class CMAPPVP : CMBaseAPSurface
    {
        public CMAPPVP()
        {

            initvalue();

        }
        List<PointLatLng> mPointsList2 = new List<PointLatLng>();

        public CMAPPVP(CMAirPortInfo apinf)
        {
            initvalue();
            mAPInf = apinf;
        }

        void initvalue()
        {
            Width = 300;
            Offset = 150;
            WidthAngle = 0.15;
            Length = 30000;
        }

        public List<PointLatLng> PointsList2
        {
            get { return mPointsList2; }
        }

        public void LoadPointsList()//CMAirPortInfo APInf)
        {
            mPointsList.Clear();
            //красная трапеция рядом с зеленой
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, Offset), APInf.CRW.RunwayBegin.BackTrueCourse + 90, Width / 2));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, Offset + Length), APInf.CRW.RunwayBegin.BackTrueCourse+90, (Width / 2) + WidthAngle * Length));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, Offset+ Length), APInf.CRW.RunwayBegin.BackTrueCourse-90, (Width / 2) + WidthAngle * Length));
            mPointsList.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, Offset), APInf.CRW.RunwayBegin.BackTrueCourse - 90, Width / 2));


            mPointsList2.Clear();
            //красная трапеция рядом с зеленой
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, Offset), APInf.CRW.RunwayEnd.BackTrueCourse + 90, Width / 2));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, Offset + Length), APInf.CRW.RunwayEnd.BackTrueCourse + 90, (Width / 2) + WidthAngle * Length));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, Offset + Length), APInf.CRW.RunwayEnd.BackTrueCourse - 90, (Width / 2) + WidthAngle * Length));
            mPointsList2.Add(CMGeoBase.GetCoordinate(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, Offset), APInf.CRW.RunwayEnd.BackTrueCourse - 90, Width / 2));

        
        }

        public CMCGPCheckedResult CheckSurface(CMCheckedGeoPoint Input )
        {
            CMCGPCheckedResult CheckedResult = new CMCGPCheckedResult();
            CheckedResult.SurfaceName = "Полоса воздушных подходов";
            bool IsObstacle = false;
            bool Forward = false;
            double dif = 0;

            if (CMGeoBase.IsPointInPolygon(mPointsList, Input.Coordinates))
            {
                    IsObstacle = true;
                    Forward = true;
            }

            if (CMGeoBase.IsPointInPolygon(mPointsList2, Input.Coordinates))
            {
                IsObstacle = true;
                Forward = false;
            }

            if (IsObstacle)
            {
                CheckedResult.ResultText = string.Format("Попадает в {0}полосу воздушных подходов", (Forward == true ? "" : " обратную ") );
            }

            CheckedResult.PointHeight = Input.Height;
            CheckedResult.Exceeding = dif;
            return CheckedResult;
        }

    }
}
