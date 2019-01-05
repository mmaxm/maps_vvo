using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.Device.Location;


namespace CLMGeo
{
    public class CMGeoCoordinate : GeoCoordinate
    {

        public enumGeoPointType GeoDotType = enumGeoPointType.geoptNull;
        public string Description = "";
        public string Text = "";

        public CMGeoCoordinate()
        { 
            
        }

        public CMGeoCoordinate(enumGeoPointType geodottype)
        {
            GeoDotType = geodottype;
        }


        public CMGeoCoordinate(enumGeoPointType geodottype, double latitude, double longitude, double altitude = 0)
        {
            GeoDotType = geodottype;
            this.Altitude = altitude;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public CMGeoCoordinate(enumGeoPointType geodottype, double latitude, double longitude, double altitude = 0, string text = "")
        {
            GeoDotType = geodottype;
            this.Altitude = altitude;
            Text = text;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public CMGeoCoordinate(enumGeoPointType geodottype, PointLatLng coordinates, double altitude = 0)
        {
            GeoDotType = geodottype;
            this.Altitude = altitude;
            this.Latitude = coordinates.Lat;
            this.Longitude = coordinates.Lng;
        }

        public double Lat
        {
            get { return this.Latitude; }
            set { this.Latitude = value; }
        }

        public double Lng
        {
            get { return this.Longitude; }
            set { this.Longitude = value; }
        }

        //public GMap.NET.WindowsForms.Markers.GMarkerGoogle GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType MarkerGoogleType)
        //{

        //    GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(Coordinates, MarkerGoogleType);
        //    marker.ToolTipText = string.Format("{0}, {1} {2}", Text, CMBaseGeoPoint.ShowDegMiSec(Lat), CMBaseGeoPoint.ShowDegMiSec(Lng));
        //    marker.Tag = new CMBaseGeoPoint(GeoDotType);

        //    return marker;
        //}

        //public static GMap.NET.WindowsForms.Markers.GMarkerGoogle GetGMarkerGoogle(string markername, GeoCoordinate pointlatlng,
        //    enumGeoPointType GeoDotType,
        //    GMap.NET.WindowsForms.Markers.GMarkerGoogleType MarkerGoogleType)
        //{

        //    //GeoCoordinate
        //    //PointLatLng 
        //    GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle((PointLatLng)pointlatlng, MarkerGoogleType);
        //    marker.ToolTipText = string.Format("{0}, {1} {2}", markername, CMBaseGeoPoint.ShowDegMiSec(pointlatlng.Lat), CMBaseGeoPoint.ShowDegMiSec(pointlatlng.Lng));
        //    marker.Tag = new CMBaseGeoPoint(GeoDotType);

        //    return marker;
        //}



    }
}
