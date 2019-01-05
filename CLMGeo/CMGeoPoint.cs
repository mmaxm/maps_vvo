using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET;
using System.Device.Location;

namespace CLMGeo
{

    public class CMGeoPoint : CMBaseGeoPoint
    {

        public double Height = 0;
        public string Text = "";
        public PointLatLng Coordinates = new PointLatLng();

        public CMGeoPoint()
        {

        }

        public CMGeoPoint(enumGeoPointType geodottype)
        {
            GeoDotType = geodottype;
        }

        public CMGeoPoint(enumGeoPointType geodottype, double lat, double lng, double height=0)
        {
            GeoDotType = geodottype;
            Height = height;
            if (Coordinates == null)
                Coordinates = new PointLatLng();
            
            Coordinates.Lat = lat;
            Coordinates.Lng = lng;
            //Coordinates.
        }

        public string GetCaption()
        {
            return  string.Format("[{0}] [{1}]{2}", ShowDegMiSec(Coordinates.Lat), ShowDegMiSec(Coordinates.Lng),(Text.Length>0 ? ", " + Text : ""));
        }

        public CMGeoPoint(enumGeoPointType geodottype, double lat, double lng, double height = 0,string text="")
        {
            GeoDotType = geodottype;
            Height = height;
            Text = text;
            if (Coordinates == null)
                Coordinates = new PointLatLng();

            Coordinates.Lat = lat;
            Coordinates.Lng = lng;
        }

        public CMGeoPoint(enumGeoPointType geodottype, PointLatLng coordinates, double height=0)
        {
            GeoDotType = geodottype;
            Height = height;
            Coordinates = coordinates;
        }

        public double Lat 
        {
            get
            {
                if (Coordinates == null)
                    return 0;
                else
                    return Coordinates.Lat; 
            }
            set 
            {
                if (Coordinates == null)
                    Coordinates = new PointLatLng();

                Coordinates.Lat = value;
            }
        }

        public double Lng 
        { 
            get
            {
                if (Coordinates == null)
                    return 0;
                else
                    return Coordinates.Lng;
            } 
            set
            {
                if (Coordinates == null)
                    Coordinates = new PointLatLng();
                Coordinates.Lng = value;
            } 
        }

        public double LngRad
        {
            get
            {
                return Math.PI * Lng / 180;
            }
        }

        public double LatRad
        {
            get
            {
                return Math.PI * Lat / 180;
            }
        }

        public double CosLat()
        {
            return Math.Cos(LatRad);
        }

        public double SinLat()
        {
            return Math.Sin(LatRad);
        }

        public GMap.NET.WindowsForms.Markers.GMarkerGoogle GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType MarkerGoogleType)
        {

            GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(Coordinates, MarkerGoogleType);
           // marker.ToolTipText = string.Format("{0}, {1} {2}", Text, ShowDegMiSec(Lat), ShowDegMiSec(Lng));
            marker.Tag = new CMBaseGeoPoint(GeoDotType);

            return marker;
        }

        public GMap.NET.WindowsForms.Markers.GMarkerGoogle GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType MarkerGoogleType, enumGeoPointType geodottype)
        {

            GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(Coordinates, MarkerGoogleType);
            //marker.ToolTipText = string.Format("{0}, {1} {2}", Text, ShowDegMiSec(Lat), ShowDegMiSec(Lng));
            marker.Tag = new CMBaseGeoPoint(geodottype);
            return marker;
        }



        public static GMap.NET.WindowsForms.Markers.GMarkerGoogle GetGMarkerGoogle(string markername, PointLatLng pointlatlng,
            enumGeoPointType GeoDotType,
            GMap.NET.WindowsForms.Markers.GMarkerGoogleType MarkerGoogleType)
        {
            GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(pointlatlng, MarkerGoogleType);
            //marker.ToolTipText = string.Format("{0}, {1} {2}", markername, ShowDegMiSec(pointlatlng.Lat), ShowDegMiSec(pointlatlng.Lng));
            marker.Tag = new CMBaseGeoPoint(GeoDotType);

            return marker;
        }


        public GeoCoordinate ToGeoCoordinate()
        {
            return new GeoCoordinate(Lat,Lng);
        }

        public double GetDistance(PointLatLng Point)
        {
            return new GeoCoordinate(Point.Lat, Point.Lng).GetDistanceTo(this.ToGeoCoordinate());
        }

    }
}


//СheckedPoint