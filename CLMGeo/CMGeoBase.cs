using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Device.Location;
using GMap.NET;

namespace CLMGeo
{

    public enum enumGeoPointType : int { geoptNull = 0, geoptControlDot = 1, geoptObstacleDot = 2, geoptPolyDot = 3, geoptRunwayThreshold = 4 }

    public class CMGeoBase
    {
        //TODO текущее значение AverageRadiusEarth взято от сюда gis-lab.info
//        public static double LengthOneDegree = 111.197;
       // public static double EquatorLength = 40031.49306;//40075.696;
        public static double AverageRadiusEarth = 6372.795;//6371.210; // 6373.795;//

        public static double DegToRad = Math.PI / 180.0;
        public static double RadToDeg = 180.0 / Math.PI;
        /// <summary>
        /// большая полуось элипсоида
        /// </summary>
        public static double a = 6378137; 

        /// <summary>
        /// Число угловых секунд в 1 радиане
        /// </summary>
        public static double ro = 206264.80624;

        /*
                    double a = 6378137; //большая полуось элипсоида
            double alfa = 1 / 298.257223563;//коэфициент сжатия элипсоида или f
            //double ro = 206264.80624;
            double b = a * (1 - alfa); //малая полуось элипсоида, ~ 6356752.314245179497 
            //double e = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2));// первый эксцентритет
            double e2 = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2));
        */

        public CMGeoBase()
        { 
        }

        /// <summary>
        /// коэфициент сжатия элипсоида или f
        /// </summary>
        public static double alfa
        {
            get
            {
                return 1 / 298.257223563;
            }
        }

        /// <summary>
        /// малая полуось элипсоида, ~ 6356752.314245179497
        /// </summary>
        public static double b
        {
            get { return a * (1 - alfa); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static double SecAlfa
        {
            get { return a * (1 / Math.Cos(alfa)); }
        }


        /// <summary>
        /// первый эксцентритет
        /// </summary>
        public static double e1
        {
            get { return Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2)); }
        }

        /// <summary>
        /// второй эксцентритет
        /// </summary>
        public static double e2
        {
            get { return Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2)); }
        }

        public static double EquatorLength
        {
            get
            {
                //return 40075.696;
                return ((2 * Math.PI) * AverageRadiusEarth);
            }
        }

        public static double LengthOneDegree
        {
            get
            {
                 //return 111.1;
                 //return 111.197;
                return EquatorLength / 360;
            }
        }

        /// <summary>
        /// Радиус кривезны
        /// </summary>
        /// <param name="B">Геодезическая широта</param>
        /// <returns></returns>
        public static double N(double B)
        {
            return (a / Math.Sqrt(1 - Math.Pow(e1, 2) * Math.Pow(Math.Sin(CMBaseGeoPoint.DegreeToRad(B)), 2)));
        }

        public static double GetU(double LatRad)
        {
            return Math.Atan((1 - alfa) * Math.Tan(LatRad));
        }


        public static PointLatLng Mercator(PointLatLng pot)
        {
            double[] xz = MercatorProjection.toPixel(pot.Lng, pot.Lat);
            //point outpot = new point(Math.Round(xz[0]), Math.Round(xz[1]));
            PointLatLng outpot = new PointLatLng(Math.Round(xz[1]), Math.Round(xz[0]));
            

            return outpot;
        }

        /// <summary>
        /// Получение новой точки на основании начальной координаты, азимута и растояния
        /// </summary>
        /// <param name="StartPoint">Начальная точка</param>
        /// <param name="asimut">азимут</param>
        /// <param name="distance">растояние от начальной точки</param>
        /// <returns></returns>
        public static CMGeoPoint GetCoordinate(CMGeoPoint StartPoint, double asimut, double distance)
        {
            /*
             *S -  distance , растояние
             */
            CMGeoPoint FinishPoint = new CMGeoPoint();

            //double a = 6378137; //большая полуось элипсоида
            //double alfa = 1 / 298.257223563;//коэфициент сжатия элипсоида
            //double ro = 206264.80624;
            //double b = a * (1 - alfa); //малая полуось элипсоида, ~ 6356752.314245179497 

            //double B = StartPoint.Coordinates.Lat; //геодезическая широта // убрать переменную использовать  StartPoint.Coordinates.Lat
            //double e = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2));// первый эксцентритет
            //double e2 = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2));

            //double N = (a / Math.Sqrt(1 - Math.Pow(e1, 2) * Math.Pow(Math.Sin(CMBaseGeoPoint.DegreeToRad(B)), 2))); //6387718.2645889279 //6387825.05301                                    

            double u = ((distance / N(StartPoint.Coordinates.Lat)) * Math.Cos(CMBaseGeoPoint.DegreeToRad(asimut)));
            double v = ((distance / N(StartPoint.Coordinates.Lat)) * Math.Sin(CMBaseGeoPoint.DegreeToRad(asimut)));

            double pb = u * (1 + Math.Pow(v, 2) / 3);
            double pc = v * (1 - Math.Pow(u, 2) / 6);
            double fi = (StartPoint.Coordinates.Lat + CMBaseGeoPoint.ShowDegreeFraction(0, 0, pb * ro));
            double tau = pc * Math.Tan(CMBaseGeoPoint.DegreeToRad(fi));
            double lambda = pc * (1 / Math.Cos(CMBaseGeoPoint.DegreeToRad(fi)));
            double t = tau * (1 - Math.Pow(lambda, 2) / 6 - Math.Pow(tau, 2) / 6) * ro;
            double l = lambda * (1 - Math.Pow(tau, 2)) * ro;
            double d = pc * tau / 2 * (1 - Math.Pow(lambda, 2) / 12 - Math.Pow(tau, 2) / 6);
            double deltafi = pb - d;
            double V = 1 + Math.Pow(e2, 2) * Math.Pow(Math.Cos(CMBaseGeoPoint.DegreeToRad(StartPoint.Coordinates.Lat)), 2);
            double deltaB = V * deltafi * (1 - 3.0 / 4.0 * Math.Pow(e2, 2) * Math.Sin(StartPoint.Coordinates.Lat * 2) * deltafi - Math.Pow(e2, 2) / 2 * Math.Cos(StartPoint.Coordinates.Lat * 2) * Math.Pow(deltafi, 2)) * ro;

            FinishPoint.Coordinates.Lat = StartPoint.Coordinates.Lat + CMBaseGeoPoint.ShowDegreeFraction(0, 0, deltaB);
            FinishPoint.Coordinates.Lng = StartPoint.Coordinates.Lng + CMBaseGeoPoint.ShowDegreeFraction(0, 0, l);

            return FinishPoint;
        }

        /// <summary>
        /// Получение новой точки на основании начальной координаты, азимута и растояния
        /// </summary>
        /// <param name="StartPoint">Начальная точка</param>
        /// <param name="asimut">азимут</param>
        /// <param name="distance">растояние от начальной точки</param>
        /// <returns></returns>
        public static CMGeoPoint GetCoordinateRad(CMGeoPoint StartPoint, double asimut, double distance)
        {
            /*
             *S -  distance , растояние
             */
            CMGeoPoint FinishPoint = new CMGeoPoint();

            //double a = 6378137; //большая полуось элипсоида
            //double alfa = 1 / 298.257223563;//коэфициент сжатия элипсоида
            //double ro = 206264.80624;
            //double b = a * (1 - alfa); //малая полуось элипсоида, ~ 6356752.314245179497 

            //double B = StartPoint.Coordinates.Lat; //геодезическая широта // убрать переменную использовать  StartPoint.Coordinates.Lat
            //double e = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2));// первый эксцентритет
            //double e2 = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2));

            //double N = (a / Math.Sqrt(1 - Math.Pow(e1, 2) * Math.Pow(Math.Sin(CMBaseGeoPoint.DegreeToRad(B)), 2))); //6387718.2645889279 //6387825.05301                                    

            double u = ((distance / N(StartPoint.Coordinates.Lat)) * Math.Cos(asimut));
            double v = ((distance / N(StartPoint.Coordinates.Lat)) * Math.Sin(asimut));

            double pb = u * (1 + Math.Pow(v, 2) / 3);
            double pc = v * (1 - Math.Pow(u, 2) / 6);
            double fi = (StartPoint.Coordinates.Lat + CMBaseGeoPoint.ShowDegreeFraction(0, 0, pb * ro));
            double tau = pc * Math.Tan(CMBaseGeoPoint.DegreeToRad(fi));
            double lambda = pc * (1 / Math.Cos(CMBaseGeoPoint.DegreeToRad(fi)));
            double t = tau * (1 - Math.Pow(lambda, 2) / 6 - Math.Pow(tau, 2) / 6) * ro;
            double l = lambda * (1 - Math.Pow(tau, 2)) * ro;
            double d = pc * tau / 2 * (1 - Math.Pow(lambda, 2) / 12 - Math.Pow(tau, 2) / 6);
            double deltafi = pb - d;
            double V = 1 + Math.Pow(e2, 2) * Math.Pow(Math.Cos(CMBaseGeoPoint.DegreeToRad(StartPoint.Coordinates.Lat)), 2);
            double deltaB = V * deltafi * (1 - 3.0 / 4.0 * Math.Pow(e2, 2) * Math.Sin(StartPoint.Coordinates.Lat * 2) * deltafi - Math.Pow(e2, 2) / 2 * Math.Cos(StartPoint.Coordinates.Lat * 2) * Math.Pow(deltafi, 2)) * ro;

            FinishPoint.Coordinates.Lat = StartPoint.Coordinates.Lat + CMBaseGeoPoint.ShowDegreeFraction(0, 0, deltaB);
            FinishPoint.Coordinates.Lng = StartPoint.Coordinates.Lng + CMBaseGeoPoint.ShowDegreeFraction(0, 0, l);

            return FinishPoint;
        }

        /// <summary>
        /// Получение новой точки на основании начальной координаты, азимута и растояния
        /// </summary>
        /// <param name="StartPoint">Начальная точка</param>
        /// <param name="asimut">азимут</param>
        /// <param name="distance">растояние от начальной точки</param>
        /// <returns></returns>
        public static PointLatLng GetCoordinate(PointLatLng StartPoint, double asimut, double distance)
        {
            /*
             *S -  distance , растояние
             */
            PointLatLng FinishPoint = new PointLatLng();

            //double a = 6378137; //большая полуось элипсоида
            //double alfa = 1 / 298.257223563;//коэфициент сжатия элипсоида
            //double ro = 206264.80624;
            //double b = a * (1 - alfa); //малая полуось элипсоида, ~ 6356752.314245179497 

            //double B = StartPoint.Coordinates.Lat; //геодезическая широта // убрать переменную использовать  StartPoint.Coordinates.Lat
            //double e = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2));// первый эксцентритет
            //double e2 = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2));

            //double N = (a / Math.Sqrt(1 - Math.Pow(e1, 2) * Math.Pow(Math.Sin(CMBaseGeoPoint.DegreeToRad(B)), 2))); //6387718.2645889279 //6387825.05301                                    

            double u = ((distance / N(StartPoint.Lat)) * Math.Cos(CMBaseGeoPoint.DegreeToRad(asimut)));
            double v = ((distance / N(StartPoint.Lat)) * Math.Sin(CMBaseGeoPoint.DegreeToRad(asimut)));

            double pb = u * (1 + Math.Pow(v, 2) / 3);
            double pc = v * (1 - Math.Pow(u, 2) / 6);
            double fi = (StartPoint.Lat + CMBaseGeoPoint.ShowDegreeFraction(0, 0, pb * ro));
            double tau = pc * Math.Tan(CMBaseGeoPoint.DegreeToRad(fi));
            double lambda = pc * (1 / Math.Cos(CMBaseGeoPoint.DegreeToRad(fi)));
            double t = tau * (1 - Math.Pow(lambda, 2) / 6 - Math.Pow(tau, 2) / 6) * ro;
            double l = lambda * (1 - Math.Pow(tau, 2)) * ro;
            double d = pc * tau / 2 * (1 - Math.Pow(lambda, 2) / 12 - Math.Pow(tau, 2) / 6);
            double deltafi = pb - d;
            double V = 1 + Math.Pow(e2, 2) * Math.Pow(Math.Cos(CMBaseGeoPoint.DegreeToRad(StartPoint.Lat)), 2);
            double deltaB = V * deltafi * (1 - 3.0 / 4.0 * Math.Pow(e2, 2) * Math.Sin(StartPoint.Lat * 2) * deltafi - Math.Pow(e2, 2) / 2 * Math.Cos(StartPoint.Lat * 2) * Math.Pow(deltafi, 2)) * ro;

            FinishPoint.Lat = StartPoint.Lat + CMBaseGeoPoint.ShowDegreeFraction(0, 0, deltaB);
            FinishPoint.Lng = StartPoint.Lng + CMBaseGeoPoint.ShowDegreeFraction(0, 0, l);

            return FinishPoint;
        }


        public static double GetAsimut(CMGeoPoint StartPoint, CMGeoPoint FinishPoint,int AsimutType = 0)
        {

            //double U1 = Math.Atan((1 - alfa) * Math.Tan(StartPoint.LatRad)); // запихнуть в функцию
            //double U2 = Math.Atan((1 - alfa) * Math.Tan(FinishPoint.LatRad)); // запихнуть в функцию

            double U1 = GetU(StartPoint.LatRad); 
            double U2 = GetU(FinishPoint.LatRad); 


            double L = FinishPoint.LngRad - StartPoint.LngRad;

            double lambda = L;

            double sigma = 0; // растояние между двумя точками на дополнительной сфере
            double sin_sigma = 0;
            double cos_sigma = 0;
            double cos_asimutEq_quad = 0;
            double cos_2_sigmaM = 0;

            for (int i = 0; i < 20; i++)
            {
                sin_sigma = Math.Sqrt(Math.Pow(Math.Cos(U2) * Math.Sin(lambda), 2) + Math.Pow(Math.Cos(U1) * Math.Sin(U2) - Math.Sin(U1) * Math.Cos(U2) * Math.Cos(lambda), 2));
                //sin_sigma = Math.Sqrt((Math.Pow((Math.Cos(U2) * Math.Sin(lambda)), 2)) + Math.Pow((Math.Cos(U1) * Math.Sin(U2) - Math.Sin(U2) * Math.Cos(U1) * Math.Cos(lambda)), 2));
                cos_sigma = Math.Sin(U1) * Math.Sin(U2) + Math.Cos(U1) * Math.Cos(U2) * Math.Cos(lambda);
                sigma = Math.Atan(sin_sigma / cos_sigma);
                double sin_asimutEq = Math.Cos(U1) * Math.Cos(U2) * Math.Sin(lambda) / sin_sigma;
                cos_asimutEq_quad = 1.0 - Math.Pow(sin_asimutEq, 2);
                cos_2_sigmaM = cos_sigma - 2.0 * Math.Sin(U1) * Math.Sin(U2) / cos_asimutEq_quad;
                double C = alfa / 16.0 * cos_asimutEq_quad * (4.0 + alfa * (4.0 - 3.0 * cos_asimutEq_quad));
                lambda = L + (1.0 - C) * alfa * sin_asimutEq * (sigma + C * sin_sigma * (cos_2_sigmaM + C * cos_sigma * (-1.0 + 2.0 * Math.Pow(cos_2_sigmaM, 2))));
            }

            double asimut12 = Math.Atan2((Math.Cos(U2) * Math.Sin(lambda)), (Math.Cos(U1) * Math.Sin(U2) - Math.Sin(U1) * Math.Cos(U2) * Math.Cos(lambda))); //азимут прямой с 1 на 2
            double asimut21 = Math.Atan2((Math.Cos(U1) * Math.Sin(lambda)), (-Math.Sin(U1) * Math.Cos(U2) + Math.Cos(U1) * Math.Sin(U2) * Math.Cos(lambda))); //азимут обратный с 2 на 1
            

            //asimut12 = Math.Atan((Math.Cos(U2) * Math.Sin(lambda)) / (Math.Cos(U1) * Math.Sin(U2) - Math.Sin(U1) * Math.Cos(U2) * Math.Cos(lambda)));
            //asimut21 = Math.Atan((Math.Cos(U1) * Math.Sin(lambda)) / (-Math.Sin(U1) * Math.Cos(U2) + Math.Cos(U1) * Math.Sin(U2) * Math.Cos(lambda)));

            //double a12 = asimut12 * 180 / Math.PI * 2;
//            double a21 = asimut21 * 180 / Math.PI;


            //return asimut12 * 180 / Math.PI * 2;

            double Asimut = 0.0;

            if (AsimutType == 0)
                Asimut = asimut12 * 180 / Math.PI;
            else
            {
                Asimut = asimut21 * 180 / Math.PI;
                Asimut += (Asimut >= 180) ? -180 : 180;
            }
            

            if (Asimut < 0)
                Asimut = Asimut + 360;

            return Asimut;

        }


        public static double GetDistance(CMGeoPoint StartPoint, CMGeoPoint FinishPoint)
        {

            //double U1 = Math.Atan((1 - alfa) * Math.Tan(StartPoint.LatRad)); // запихнуть в функцию
            //double U2 = Math.Atan((1 - alfa) * Math.Tan(FinishPoint.LatRad)); // запихнуть в функцию

            double U1 = GetU(StartPoint.LatRad); // запихнуть в функцию
            double U2 = GetU(FinishPoint.LatRad); // запихнуть в функцию


            double L = FinishPoint.LngRad - StartPoint.LngRad;

            double lambda = L;

            double sigma = 0; // растояние между двумя точками на дополнительной сфере
            double sin_sigma = 0;
            double cos_sigma = 0;
            double cos_asimutEq_quad = 0;
            double cos_2_sigmaM = 0;

            for (int i = 0; i < 20; i++)
            {
                sin_sigma = Math.Sqrt(Math.Pow(Math.Cos(U2) * Math.Sin(lambda), 2) + Math.Pow(Math.Cos(U1) * Math.Sin(U2) - Math.Sin(U1) * Math.Cos(U2) * Math.Cos(lambda), 2));
                //sin_sigma = Math.Sqrt((Math.Pow((Math.Cos(U2) * Math.Sin(lambda)), 2)) + Math.Pow((Math.Cos(U1) * Math.Sin(U2) - Math.Sin(U2) * Math.Cos(U1) * Math.Cos(lambda)), 2));
                cos_sigma = Math.Sin(U1) * Math.Sin(U2) + Math.Cos(U1) * Math.Cos(U2) * Math.Cos(lambda);
                sigma = Math.Atan(sin_sigma / cos_sigma);
                double sin_asimutEq = Math.Cos(U1) * Math.Cos(U2) * Math.Sin(lambda) / sin_sigma;
                cos_asimutEq_quad = 1.0 - Math.Pow(sin_asimutEq, 2);
                cos_2_sigmaM = cos_sigma - 2.0 * Math.Sin(U1) * Math.Sin(U2) / cos_asimutEq_quad;
                double C = alfa / 16.0 * cos_asimutEq_quad * (4.0 + alfa * (4.0 - 3.0 * cos_asimutEq_quad));
                lambda = L + (1.0 - C) * alfa * sin_asimutEq * (sigma + C * sin_sigma * (cos_2_sigmaM + C * cos_sigma * (-1.0 + 2.0 * Math.Pow(cos_2_sigmaM, 2))));
            }

            double u_quad = cos_asimutEq_quad * Math.Pow(e2, 2);
            //double A = 1 + u_quad / 16384 * (4096 + u_quad * (-768 + u_quad * (320 - 175 * u_quad)));
            double B = u_quad / 1024 * (256 + u_quad * (-128 + u_quad * (74 - 47 * u_quad)));
            
            double delta_sigma = B * sin_sigma * (cos_2_sigmaM + 1.0 / 4.0 * B * (cos_sigma * (-1 + 2 * Math.Pow(cos_2_sigmaM, 2) - 1 / 6 * B * cos_2_sigmaM * (-3 + 4 * Math.Pow(sin_sigma, 2) * (-3 + 4 * Math.Pow(cos_2_sigmaM, 2))))));

            return b * (1 + u_quad / 16384 * (4096 + u_quad * (-768 + u_quad * (320 - 175 * u_quad)))) * (sigma - delta_sigma);

            //return b * A * (sigma - delta_sigma);


        }

        

        //


        /// <summary>
        /// Нахождение азимута
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="vector1"></param>
        /// <returns></returns>
        public static double Azimut(PointLatLng vector2, PointLatLng vector1)
        {
            //kta
            vector2 = Mercator(vector2);
            double x1 = vector2.Lng;
            double y1 = vector2.Lat;

            //north point of airground
            vector1 = Mercator(vector1);
            double x2 = vector1.Lng;//132.16693;
            double y2 = vector1.Lat;//43.40660;

            double dx = x2 - x1;
            double dy = y2 - y1;
            double AzimutAngle = 0; 

            AzimutAngle = Math.Atan2(dy, dx);
            AzimutAngle = AzimutAngle * (180 / Math.PI);
            if (dy < 0) 
                AzimutAngle = AzimutAngle * (-1) + 90;
            else
                if (dx < 0)
                    AzimutAngle = 180 - AzimutAngle + 270;
                else
                    AzimutAngle = 90 - AzimutAngle;

            return AzimutAngle;
            
        }

        public static double Azimut_m(PointLatLng Point1, PointLatLng Point2)
        {

            double lat1 = Point1.Lat * Math.PI / 180;
            double lat2 = Point2.Lat * Math.PI / 180;
            double long1 = Point1.Lng * Math.PI / 180;
            double long2 = Point2.Lng * Math.PI / 180;

            double cl1 = Math.Cos(lat1);
            double cl2 = Math.Cos(lat2);

            double sl1 = Math.Sin(lat1);
            double sl2 = Math.Sin(lat2);

            double delta = long2 - long1;
            double cdelta = Math.Cos(delta);
            double sdelta = Math.Sin(delta);

            double x = (cl1 * sl2) - (sl1 * cl2 * cdelta);
            double y = sdelta * cl2;
            double z = Math.Atan(-y / x);
            z = z * RadToDeg;// (180 / Math.PI);
            

            if (x < 0)
                z += 180;

            z = (- ((z + 180) / (360 - 180))) * DegToRad;
            
            double an1 = z -((2*Math.PI ) *(z/(2*Math.PI)));
            double an2=(an1 * 180)/Math.PI;
            return an2 * RadToDeg;
        }


        public static PointLatLng OtstupVPP(double distOtst, int mode, PointLatLng FRMPoint)
        {
            //return OtstupVPP(distOtst, mode, FRMPoint, 61.025);
            //return OtstupVPP(distOtst, mode, FRMPoint, 61);
            return OtstupVPP(distOtst, mode, FRMPoint, 61.017087858055064);
            
        }

        /// <summary>
        /// Получение новой точки координат, удаленной от исходной точки на указаную дистанцию и в указанном направлении
        /// </summary>
        /// <param name="distOtst"></param>
        /// <param name="mode"></param>
        /// <param name="FRMPoint"></param>
        /// <returns></returns>
        /// MagneticCourse
        public static PointLatLng OtstupVPP(double distOtst, int mode, PointLatLng FRMPoint, double tCourse)
        {
            double len = Math.Cos(FRMPoint.Lat * CMGeoBase.DegToRad) * CMGeoBase.LengthOneDegree;//(CMGeoBase.GetEquatorLength() / 360);
            //double len = Math.Cos(FRMPoint.Lat * Math.PI / 180) * 40075 / 360;

        //    public static double DegToRad = Math.PI / 180.0;
        //public static double RadToDeg = 180.0 / Math.PI;

            double lng = 0;
            double lat = 0;
            distOtst /= 1000; //Переводим метры в километры
            //TODO заменить 61 на какуюнибудь переменную
            switch (mode)
            {
                case 1: //-90
                    {
                        lng = FRMPoint.Lng + distOtst * Math.Cos((180 - tCourse) * CMGeoBase.DegToRad) / len; // 90 - 61 + 270 + 180
                        lat = FRMPoint.Lat + distOtst * Math.Sin((180 - tCourse) * CMGeoBase.DegToRad) / CMGeoBase.LengthOneDegree;//111.1;
                        break;
                    }
                case 2: //0
                    {
                        lng = FRMPoint.Lng + distOtst * Math.Cos((90 - tCourse) * CMGeoBase.DegToRad) / len;
                        lat = FRMPoint.Lat + distOtst * Math.Sin((90 - tCourse) * CMGeoBase.DegToRad) / CMGeoBase.LengthOneDegree;//111.1;
                        //y = FRMPoint.Lat + distOtst * Math.Sin((90 - tCourse) * CMGeoBase.DegToRad) / CMGeoBase.LengthOneDegree;//111.1;
                        break;
                    }
                case 3:  // 90
                    {
                        lng = FRMPoint.Lng + distOtst * Math.Cos((360 - tCourse) * CMGeoBase.DegToRad) / len;//90-61+270
                        lat = FRMPoint.Lat + distOtst * Math.Sin((360 - tCourse) * CMGeoBase.DegToRad) / CMGeoBase.LengthOneDegree;//111.1;
                        break;
                    }
                case 4:      //180             
                    {
                        lng = FRMPoint.Lng + distOtst * Math.Cos((270 - tCourse) * CMGeoBase.DegToRad) / len; //90-61+180
                        lat = FRMPoint.Lat + distOtst * Math.Sin((270 - tCourse) * CMGeoBase.DegToRad) / CMGeoBase.LengthOneDegree;//111.1;
                        break;
                    }
                default: break;

            }
            return new PointLatLng(lat, lng);
        }

        public static bool IsPointInPolygon(List<PointLatLng> poly, PointLatLng point)
        {
            int i, j;
            bool c = false;

            for (i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                if ((((poly[i].Lat <= point.Lat) && (point.Lat < poly[j].Lat)) ||
                    ((poly[j].Lat <= point.Lat) && (point.Lat < poly[i].Lat))) &&
                    (point.Lng < (poly[j].Lng - poly[i].Lng) * (point.Lat - poly[i].Lat) / (poly[j].Lat - poly[i].Lat) + poly[i].Lng))

                    c = !c;
            }
            return c;
        }

        public static bool CheckIntegrity(PointLatLng SideBegin, PointLatLng SideEnd, PointLatLng CheckPoint)
        {
            double d = (SideEnd.Lng - SideBegin.Lng) * (CheckPoint.Lat - SideBegin.Lat) - (SideEnd.Lat - SideBegin.Lat) * (CheckPoint.Lng - SideBegin.Lng);
            return (d <= 0) ? true : false; // учитываем положение справа от линии + на линии
        }

        public static double GetDistance(PointLatLng PointA, PointLatLng PointB)
        {
            GeoCoordinate ACoord = new GeoCoordinate(PointA.Lat, PointA.Lng);
            GeoCoordinate BCoord = new GeoCoordinate(PointB.Lat, PointB.Lng);
            return BCoord.GetDistanceTo(ACoord);
        }

        public static double GetDistance(CMGeoPoint RPoint, int zone, CMGeoPoint RunwayBegin, CMGeoPoint RunwayEnd)
        {
            //TODO убрать в класс точки
            //GeoCoordinate test = new GeoCoordinate();
            GeoCoordinate sCoord = new GeoCoordinate(RunwayBegin.Lat, RunwayBegin.Lng);
            GeoCoordinate eCoord = new GeoCoordinate(RunwayEnd.Lat, RunwayEnd.Lng);
            double Fdistance = 0;
            GeoCoordinate randCoord = new GeoCoordinate(RPoint.Lat, RPoint.Lng);

            //var KTACoord = new GeoCoordinate(GPKTA.Lat, GPKTA.Lng);

            switch (zone)
            {
                case 3:
                    Fdistance = GetDistance(RunwayEnd.Coordinates,RPoint.Coordinates); //= eCoord.GetDistanceTo(randCoord);
                    //HeigthPlaneB(Fdistance);
                    break;
                case 1:
                    Fdistance = GetDistance(RunwayBegin.Coordinates, RPoint.Coordinates);
                    //Fdistance = sCoord.GetDistanceTo(randCoord);
                    
                    //HeigthPlaneB(Fdistance);
                    break;
                case 2:
                    {
                        Fdistance = LineToPointDistance(RPoint,RunwayBegin,RunwayEnd);
                        //HeigthPlaneB(Fdistance);
                        break;
                    }
                default:
                    break;
            }

            return Fdistance;

        }

        public static double LineToPointDistance(CMGeoPoint point, CMGeoPoint RunwayBegin, CMGeoPoint RunwayEnd)
        {
            //поиск растояния от полосы до точки через высоту треугольника
            double a = CMGeoBase.GetDistance(RunwayEnd, point);
            double b = CMGeoBase.GetDistance(point, RunwayBegin);
            double c = CMGeoBase.GetDistance(RunwayBegin, RunwayEnd);
            double p = (a + b + c) / 2;

            double h = Math.Sqrt(p * (p - a) * (p - b) * (p - c)) * 2 / c;
            return h;
        }

        public static double LineToPointDistance_(PointLatLng point, PointLatLng RunwayBegin, PointLatLng RunwayEnd)
        {
            //поиск растояния от полосы до точки через высоту треугольника
            double a = CMGeoBase.GetDistance(RunwayEnd, point);
            double b = CMGeoBase.GetDistance(point, RunwayBegin);
            double c = CMGeoBase.GetDistance(RunwayBegin, RunwayEnd);
            double p = (a + b + c) / 2;

            double h = Math.Sqrt(p * (p - a) * (p - b) * (p - c)) * 2 / c;
            return h;
        }


        public static Vector LineToPointDistance(Vector linePoint1, Vector linePoint2, Vector point)
        {

            double u =
                    ((point.X - linePoint1.X) * (linePoint2.X - linePoint1.X) +
                    (point.Y - linePoint1.Y) * (linePoint2.Y - linePoint1.Y)) / (linePoint2 - linePoint1).LengthSquared;
                        Vector intersectionPoint = new Vector(
                linePoint1.X + u * (linePoint2.X - linePoint1.X),
                linePoint1.Y + u * (linePoint2.Y - linePoint1.Y)
            );

            Vector distance = intersectionPoint - point;

            distance.X = Math.Abs(distance.X);
            distance.Y = Math.Abs(distance.Y);

            return distance;
        }

        public static double LineSide(PointLatLng point, PointLatLng LineBegin, PointLatLng LineEnd)
        {
            return ((LineEnd.Lat - LineBegin.Lat) * (point.Lng - LineBegin.Lng) - (point.Lat - LineBegin.Lat) * (LineEnd.Lng - LineBegin.Lng));
        }


        public static int GetZone(CMGeoPoint point, double Radius, int TakeoffSurfaceDirection, CMRunwayThreshold RunwayBegin, CMRunwayThreshold RunwayEnd)
        {
            double RunwayLength = GetDistance(RunwayBegin,RunwayEnd);

            

            List<PointLatLng> Plane1 = new List<PointLatLng>();
            Plane1.Add(CMGeoBase.GetCoordinate(RunwayBegin, RunwayBegin.BackTrueCourse - 90,Radius).Coordinates);
            Plane1.Add(CMGeoBase.GetCoordinate(RunwayEnd, RunwayEnd.BackTrueCourse + 90, Radius).Coordinates);
            Plane1.Add(CMGeoBase.GetCoordinate(RunwayEnd, RunwayEnd.BackTrueCourse - 90, Radius).Coordinates);
            Plane1.Add(CMGeoBase.GetCoordinate(RunwayBegin, RunwayBegin.BackTrueCourse + 90, Radius).Coordinates);

            //Plane1.Add(CMGeoBase.OtstupVPP(Radius, 1, RunwayBegin.Coordinates));
            //Plane1.Add(CMGeoBase.OtstupVPP(Radius, 1, RunwayEnd.Coordinates));
            //Plane1.Add(CMGeoBase.OtstupVPP(Radius, 3, RunwayEnd.Coordinates));
            //Plane1.Add(CMGeoBase.OtstupVPP(Radius, 3, RunwayBegin.Coordinates));

            // ShowPolygonPoints(Plane1);
            if (CMGeoBase.IsPointInPolygon(Plane1, point.Coordinates))
            {
                return 2;
            }
            else
            {
                //CMGeoPoint pointx = CMGeoBase.GetCoordinate(RunwayBegin,RunwayBegin.
                PointLatLng pointx = CMGeoBase.GetCoordinate(RunwayBegin.Coordinates,RunwayBegin.TrueCourse,RunwayLength / 2);

                PointLatLng LineBegin = CMGeoBase.GetCoordinate(pointx, RunwayBegin.TrueCourse + 90, Radius);
                PointLatLng LineEnd = CMGeoBase.GetCoordinate(pointx, RunwayBegin.TrueCourse - 90, Radius);

                //PointLatLng pointx_ = CMGeoBase.OtstupVPP(RunwayLength / 2, TakeoffSurfaceDirection, RunwayBegin.Coordinates);
                //PointLatLng LineBegin_ = CMGeoBase.OtstupVPP(Radius, 1, pointx);
                //PointLatLng LineEnd_ = CMGeoBase.OtstupVPP(Radius, 3, pointx);

                double dec = LineSide(point.Coordinates, LineBegin, LineEnd);

                if (dec <= 0)
                    return TakeoffSurfaceDirection == 2 ? 3 : 1;
                else
                    return TakeoffSurfaceDirection == 2 ? 1 : 3;
            }
        }

        public static int GetZone_(PointLatLng point, double Radius, int TakeoffSurfaceDirection, PointLatLng RunwayBegin, PointLatLng RunwayEnd)
        {
            double RunwayLength = GetDistance(RunwayBegin, RunwayEnd);

            List<PointLatLng> Plane1 = new List<PointLatLng>();
            Plane1.Add(CMGeoBase.OtstupVPP(Radius, 1, RunwayBegin));
            Plane1.Add(CMGeoBase.OtstupVPP(Radius, 1, RunwayEnd));
            Plane1.Add(CMGeoBase.OtstupVPP(Radius, 3, RunwayEnd));
            Plane1.Add(CMGeoBase.OtstupVPP(Radius, 3, RunwayBegin));
            // ShowPolygonPoints(Plane1);
            if (CMGeoBase.IsPointInPolygon(Plane1, point))
            {
                return 2;
            }
            else
            {
                PointLatLng pointx = CMGeoBase.OtstupVPP(RunwayLength / 2, TakeoffSurfaceDirection, RunwayBegin);
                PointLatLng LineBegin = CMGeoBase.OtstupVPP(Radius, 1, pointx);
                PointLatLng LineEnd = CMGeoBase.OtstupVPP(Radius, 3, pointx);

                double dec = LineSide(point, LineBegin, LineEnd);

                if (dec <= 0)
                    return TakeoffSurfaceDirection == 2 ? 3 : 1;
                else
                    return TakeoffSurfaceDirection == 2 ? 1 : 3;
            }
        }


        public static byte ZoneChecker(PointLatLng Input, List<PointLatLng> Poligon)
        {
            byte dec = 1;
            List<PointLatLng> TempPoligon = new List<PointLatLng>(5);
            TempPoligon = TempPoligon.Concat(Poligon.GetRange(0, 2).ToList()).Concat(Poligon.GetRange(Poligon.Count - 2, 2).ToList()).ToList();
            //ShowPolygonPoints(TempPoligon);
            if (CMGeoBase.IsPointInPolygon(TempPoligon, Input))
            {
                return dec;
            }
            else
            {
                TempPoligon.Clear();
                TempPoligon = TempPoligon.Concat(Poligon.GetRange(1, Poligon.Count - 2).ToList()).ToList();
                dec += ZoneChecker(Input, TempPoligon);
            }

            TempPoligon.Clear();
            return dec;
        }


    }
}
