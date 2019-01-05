using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using CLMGeo;
using DataAirport;
using Demo.WindowsForms.CustomMarkers;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;


/*
 * TODO For Max
 *  Longtitude - Долгота ось OY
    Latitude - Широта ось OX
 * Надо разобратся что это за зверь -> DataStorage
 * 
 */
namespace Maps
{
    public partial class Form1 : Form
    {

        public GMapOverlay polyOverlay = new GMapOverlay("polygons");
        GmapsSave z;
        CMAirPortInfo APInf = new CMAirPortInfo();
        CMAPSurfaces APSurfaces = new CMAPSurfaces();
        List<CMCheckedGeoPoint> mCheckedGeoPoin = new List<CMCheckedGeoPoint>();

        bool showControlDot = false;
        bool ShowInterception = false;

        public Form1()
        {
            InitializeComponent();
            GMapProvider.Language = LanguageType.Russian;
            //GMapProvider.UserAgent = "";
#if DEBUG
#else
            //            toolStripButton1.Visible = false;
            cmdTest.Visible = false;
            button2.Visible = false;
#endif



            LoadAPSurfaces();
            LoadAPInf();

            gmaper.MapProvider = GMap.NET.MapProviders.YandexMapProvider.Instance;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gmaper.CacheLocation = Application.StartupPath + @"\maps";
            gmaper.MapScaleInfoEnabled = true;
            gmaper.Manager.Mode = AccessMode.ServerAndCache;
            
            //gmaper.ShowCenter = true;
            nudMapZoom.Value = (decimal)gmaper.Zoom;
            nudMapZoom.Maximum = gmaper.MaxZoom;
            nudMapZoom.Minimum = gmaper.MinZoom;

            comboBox1.ValueMember = "Name";
            comboBox1.DataSource = GMapProviders.List;
            comboBox1.SelectedItem = gmaper.MapProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srt">Выбранный порог ВПП </param>
        void Configure(int srt)
        {
            APSurfaces.SurfaceA.LoadPointsList();
            APSurfaces.SurfaceB.LoadPointsList();
            APSurfaces.SurfaceC.LoadPointsList();
            APSurfaces.SurfaceOuterHorizontal.LoadPointsList();
            APSurfaces.SurfaceD.LoadPointsList(); 
            APSurfaces.SurfaceE.LoadPointsList();
            APSurfaces.SurfaceNoiseB.LoadPointsList();
            APSurfaces.SurfaceNoiseV.LoadPointsList();
            APSurfaces.SurfaceNoiseG.LoadPointsList();

            APSurfaces.APPVP_B.LoadPointsList();

            APSurfaces.TakeoffSurface.LoadPointsList(); //зеленая область
            APSurfaces.TakeoffSurfaceInfo.LoadPointsList();
            APSurfaces.TransitionSurface.LoadPointsList(APSurfaces.ApproachSurface,  APSurfaces.SurfaceA.Height); //желтая область
            APSurfaces.ApproachSurface.LoadPointsList(); //синяя область
            APSurfaces.CancelledApproachSurface.LoadPointsList(); //красная трапеция рядом с зеленой
            APSurfaces.InnerApproachSurface.LoadPointsList();
            APSurfaces.InnerTransitionSurface.LoadPointsList(APSurfaces.InnerApproachSurface, APSurfaces.CancelledApproachSurface);
            RedrawGmap();
        }

        private void LoadAPInf()
        {
            APInf.AirfieldHeight = 17.87;
            APInf.KTA = new CMGeoPoint(enumGeoPointType.geoptControlDot, 43.398056, 132.148889, 14.8, "КТА");

            APInf.Runways.Add(new CMAPRunway("ВПП1", 3502.54, 60, new CMRunwayThreshold(enumGeoPointType.geoptRunwayThreshold, 43.389269, 132.130642, 9.87, CMBaseGeoPoint.ShowDegreeFraction(61, 0, 44), 71, "07R", 16.5), new CMRunwayThreshold(enumGeoPointType.geoptRunwayThreshold, 43.404542, 132.168464, 13.30, CMBaseGeoPoint.ShowDegreeFraction(241, 2, 18), 251, "25L", 16.0)));
            APInf.Runways.Add(new CMAPRunway("ВПП2", 3503.23, 60, new CMRunwayThreshold(enumGeoPointType.geoptRunwayThreshold, 43.391319, 132.129086, 9.67, CMBaseGeoPoint.ShowDegreeFraction(61, 2, 56), 71, "07L", 16.5), new CMRunwayThreshold(enumGeoPointType.geoptRunwayThreshold, 43.406583, 132.166925, 9.79, CMBaseGeoPoint.ShowDegreeFraction(241, 2, 56), 251, "25R", 16.0)));

            cmbListCoursesLA.Items.AddRange(APInf.Runways.GetArrListCoursesLA());
            cmbListCoursesLA.SelectedIndex = 1;

        }

        private void LoadAPSurfaces()
        {
            //добавить инициацию других плоскостей
            APSurfaces.SurfaceA.Height = 50;
            APSurfaces.SurfaceA.Radius = 4000;
            APSurfaces.SurfaceB.Angle = 0.05;
            APSurfaces.SurfaceB.Radius = 6000;
            APSurfaces.SurfaceC.Radius = 15000;
            APSurfaces.SurfaceC.Height = 0;
            APSurfaces.SurfaceC.SurfaceName = "Поверхность R-15";
            APSurfaces.SurfaceOuterHorizontal.Radius = 15000;
            APSurfaces.SurfaceOuterHorizontal.Height = 150;
            APSurfaces.SurfaceOuterHorizontal.SurfaceName = "Внешняя горизонтальная поверхность";
            APSurfaces.SurfaceD.Radius = 60000;
            APSurfaces.SurfaceD.SurfaceName = "Поверхность R-60";
            APSurfaces.SurfaceE.Radius = 30000;
            APSurfaces.SurfaceE.SurfaceName = "Поверхность R-30";

            APSurfaces.APInf = APInf;

        }

        PointLatLng RunwayEnd
        {
            get { return APInf.CRW.RunwayEnd.Coordinates; }
        }
        PointLatLng RunwayBegin
        {
            get { return APInf.CRW.RunwayBegin.Coordinates; }
        }

        void RedrawGmap()
        {
            if (gmaper.Overlays.Count != 0)
            {
                polyOverlay.Clear();///
                gmaper.Overlays.Remove(polyOverlay);
            }
            GmapsDraw(gmaper);
        }


        void StartCheck(CMCheckedGeoPoint STpoint)
        {
            FillPointSpec(STpoint);

            //TODO - убрать в класс
            STpoint.WriteResultCheck(string.Format("Lat {0}; Lng {1}", CMBaseGeoPoint.ShowDegMiSec(STpoint.Lat), CMBaseGeoPoint.ShowDegMiSec(STpoint.Lng)));
            STpoint.WriteResultCheck(string.Format("Азимут {0}; Дальность {1}; Абсолютная высота {2}", STpoint.Angle.ToString(), STpoint.Distance.ToString(), STpoint.Height.ToString()));
            STpoint.WriteResultCheck(string.Format("X ={0}; Y ={1}", STpoint.Offset.Lat.ToString(), STpoint.Offset.Lng.ToString()));

            
            // Проверка плоскостей
            //Перевести все проверки к такому виду
            STpoint.AddData(APSurfaces.SurfaceA.CheckSurface(STpoint, APSurfaces.SurfaceB)); //горизонтальная
            STpoint.AddData(APSurfaces.SurfaceB.CheckSurface(STpoint, APSurfaces.SurfaceA)); //коническая
            STpoint.AddData(APSurfaces.SurfaceC.CheckSurface(STpoint, APSurfaces.SurfaceB)); //круглая
            STpoint.AddData(APSurfaces.SurfaceOuterHorizontal.CheckSurface(STpoint, APSurfaces.SurfaceB)); //круглая
            STpoint.AddData(APSurfaces.SurfaceE.CheckSurface(STpoint, APSurfaces.SurfaceC)); //круглая
            STpoint.AddData(APSurfaces.SurfaceD.CheckSurface(STpoint, APSurfaces.SurfaceE)); //круглая

            STpoint.AddData(APSurfaces.SurfaceNoiseG.CheckSurface(STpoint)); //Эллипс
            STpoint.AddData(APSurfaces.SurfaceNoiseV.CheckSurface(STpoint, APSurfaces.SurfaceNoiseG)); //Эллипс
            STpoint.AddData(APSurfaces.SurfaceNoiseB.CheckSurface(STpoint, APSurfaces.SurfaceNoiseV)); //Эллипс
            
            STpoint.AddData(APSurfaces.ApproachSurface.CheckSurface(STpoint));
            STpoint.AddData(APSurfaces.TakeoffSurface.CheckSurface(STpoint));
            STpoint.AddData(APSurfaces.TakeoffSurfaceInfo.CheckSurface(STpoint));
            
            STpoint.AddData(APSurfaces.InnerApproachSurface.CheckSurface(STpoint));
            STpoint.AddData(APSurfaces.CancelledApproachSurface.CheckSurface(STpoint, STpoint.Offset.Lat));
            STpoint.AddData(APSurfaces.InnerTransitionSurface.CheckSurface(STpoint, APSurfaces.InnerApproachSurface, APSurfaces.CancelledApproachSurface));
            STpoint.AddData(APSurfaces.TransitionSurface.CheckSurface(STpoint));
            STpoint.AddData(APSurfaces.APPVP_B.CheckSurface(STpoint));

            //Перевести все проверки к такому виду

            TextResult.Text += TextResult.Text.Length > 0 ? Environment.NewLine + "***********************************" + Environment.NewLine : "";
            TextResult.Text += STpoint.ResultCheck +Environment.NewLine;
            TextResult.Text += STpoint.CheckResult() + Environment.NewLine;

            mCheckedGeoPoin.Add(STpoint);
            rtxtDotDescription.Text = STpoint.ResultCheck + Environment.NewLine;
            rtxtDotDescription.Text += STpoint.CheckResult();

            GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = STpoint.GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType.green);
            //marker.ToolTipText = STpoint.ResultCheck;

            marker.Tag = new CMBaseGeoPoint(STpoint.GeoDotType, mCheckedGeoPoin.Count-1);
            polyOverlay.Markers.Add(marker);
           
            listBox1.Items.Add(STpoint.GetCaption());

        }

     
        public void GmapsDraw(GMapControl Map)
        {
            //отрисовываем карту
            Map.ShowCenter = false;
            //Map.Bearing = 30;
            if (comboBox1.SelectedItem != null)
            {
                gmaper.MapProvider = comboBox1.SelectedItem as GMapProvider;
            }
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            ShowVPP();
            ShowSurfaces();
            ShowRouts();

            Map.Overlays.Add(polyOverlay);
            // gmap.SetPositionByKeywords("Vladivostok, Russia");
            Map.Position = new PointLatLng(43.389384, 132.130923);
        }

        void ShowSurfaces()
        {
            GMapPolygon GMapSurfaceAPoligon = new GMapPolygon(APSurfaces.SurfaceA.PointsList, "SurfaceA");
            GMapSurfaceAPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Black));
            GMapSurfaceAPoligon.Stroke = new Pen(Color.Black, 1);

            GMapPolygon GMapSurfaceBPoligon = new GMapPolygon(APSurfaces.SurfaceB.PointsList, "SurfaceB");
            GMapSurfaceBPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Black));
            GMapSurfaceBPoligon.Stroke = new Pen(Color.Black, 1);
            
            GMapPolygon GMapSurfaceCPoligon = new GMapPolygon(APSurfaces.SurfaceC.PointsList, "SurfaceC");
            GMapSurfaceCPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Black));
            GMapSurfaceCPoligon.Stroke = new Pen(Color.Black, 1);

            GMapPolygon GMapSurfaceEPoligon = new GMapPolygon(APSurfaces.SurfaceE.PointsList, "SurfaceE");
            GMapSurfaceEPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Black));
            GMapSurfaceEPoligon.Stroke = new Pen(Color.Black, 1);

            GMapPolygon GMapSurfaceNoiseBPoligon = new GMapPolygon(APSurfaces.SurfaceNoiseB.PointsList, "SurfaceNoiseB");
            GMapSurfaceNoiseBPoligon.Fill = new SolidBrush(Color.FromArgb(2, Color.DarkTurquoise));
            GMapSurfaceNoiseBPoligon.Stroke = new Pen(Color.DarkTurquoise, 1);

            GMapPolygon GMapSurfaceNoiseVPoligon = new GMapPolygon(APSurfaces.SurfaceNoiseV.PointsList, "SurfaceNoiseV");
            GMapSurfaceNoiseVPoligon.Fill = new SolidBrush(Color.FromArgb(2, Color.Blue));
            GMapSurfaceNoiseVPoligon.Stroke = new Pen(Color.Blue, 1);

            GMapPolygon GMapSurfaceNoiseGPoligon = new GMapPolygon(APSurfaces.SurfaceNoiseG.PointsList, "SurfaceNoiseG");
            GMapSurfaceNoiseGPoligon.Fill = new SolidBrush(Color.FromArgb(2, Color.Red));
            GMapSurfaceNoiseGPoligon.Stroke = new Pen(Color.Red, 1);
            

            GMapPolygon GMapAPPVP_BPoligon = new GMapPolygon(APSurfaces.APPVP_B.PointsList, "GMapAPPVP_BPoligon");
            GMapAPPVP_BPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Red));
            GMapAPPVP_BPoligon.Stroke = new Pen(Color.Red, 1);

            GMapPolygon GMapAPPVP_EPoligon = new GMapPolygon(APSurfaces.APPVP_B.PointsList2, "GMapAPPVP_BPoligon");
            GMapAPPVP_EPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Red));
            GMapAPPVP_EPoligon.Stroke = new Pen(Color.Red, 1);


            GMapPolygon GMapSurfaceDPoligon = new GMapPolygon(APSurfaces.SurfaceD.PointsList, "SurfaceD");
            GMapSurfaceDPoligon.Fill = new SolidBrush(Color.FromArgb(3, Color.Black));
            GMapSurfaceDPoligon.Stroke = new Pen(Color.Black, 1);
            
            GMapPolygon GMapTakeoffSurfacePoligon = new GMapPolygon(APSurfaces.TakeoffSurface.PointsList, "TakeoffSurface");
            GMapTakeoffSurfacePoligon.Fill = new SolidBrush(Color.FromArgb(30, Color.Green));
            GMapTakeoffSurfacePoligon.Stroke = new Pen(Color.DarkTurquoise, 1);
           
            GMapPolygon GMapApproachSurfacePoligon = new GMapPolygon(APSurfaces.ApproachSurface.PointsList, "ApproachSurface");
            GMapApproachSurfacePoligon.Fill = new SolidBrush(Color.FromArgb(30, Color.Blue));
            GMapApproachSurfacePoligon.Stroke = new Pen(Color.Blue, 1);
            
            GMapPolygon GMapCancelledApproachSurfacePoligon = new GMapPolygon(APSurfaces.CancelledApproachSurface.PointsList, "CancelledApproachSurface");
            GMapCancelledApproachSurfacePoligon.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            GMapCancelledApproachSurfacePoligon.Stroke = new Pen(Color.Black, 1);
            
            GMapPolygon GMapInnerApproachSurface = new GMapPolygon(APSurfaces.InnerApproachSurface.PointsList, "InnerApproachSurface");
            GMapInnerApproachSurface.Fill = new SolidBrush(Color.FromArgb(30, Color.Cyan));
            GMapInnerApproachSurface.Stroke = new Pen(Color.Cyan, 1);
            
            GMapPolygon GMapTransitionSurfacePoligon = new GMapPolygon(APSurfaces.TransitionSurface.PointsList, "TransitionSurface");
            GMapTransitionSurfacePoligon.Fill = new SolidBrush(Color.FromArgb(50, Color.DarkTurquoise));
            GMapTransitionSurfacePoligon.Stroke = new Pen(Color.DarkTurquoise, 1);

            GMapPolygon GMapTransitionSurfacePoligon2 = new GMapPolygon(APSurfaces.TransitionSurface.PointsList2, "TransitionSurface");
            GMapTransitionSurfacePoligon2.Fill = new SolidBrush(Color.FromArgb(50, Color.DarkTurquoise));
            GMapTransitionSurfacePoligon2.Stroke = new Pen(Color.DarkTurquoise, 1);

            GMapPolygon GMapInnerTransitionSurface = new GMapPolygon(APSurfaces.InnerTransitionSurface.PointsList, "InnerApproachSurface");
            GMapInnerTransitionSurface.Fill = new SolidBrush(Color.FromArgb(30, Color.Brown));
            GMapInnerTransitionSurface.Stroke = new Pen(Color.Brown, 1);
            
            GMapPolygon GMapInnerTransitionSurface2 = new GMapPolygon(APSurfaces.InnerTransitionSurface.PointsList2, "InnerApproachSurface");
            GMapInnerTransitionSurface2.Fill = new SolidBrush(Color.FromArgb(30, Color.Brown));
            GMapInnerTransitionSurface2.Stroke = new Pen(Color.Brown, 1);

            //TODO нужно динамически формировать полигоны для впп


            ////Position for redraw
            if (ShowInterception)
            {
                //TODO раскоменть это после всех проверок
                //if (InnerTransitionSurface.Visible == true)
                //    polyOverlay.Polygons.Add(GMapInnerTransitionSurface);
                //if (TransitionSurface.Visible == true)
                //    polyOverlay.Polygons.Add(GMapTransitionSurfacePoligon);
                //if (InnerApproachSurface.Visible == true)
                //    polyOverlay.Polygons.Add(GMapInnerApproachSurface);
                //if (CancelledApproachSurface.Visible == true)
                //    polyOverlay.Polygons.Add(GMapCancelledApproachSurfacePoligon);
                //if (ApproachSurface.Visible == true)
                //    polyOverlay.Polygons.Add(GMapApproachSurfacePoligon);
                //if (TakeoffSurface.Visible == true)
                //    polyOverlay.Polygons.Add(GMapTakeoffSurfacePoligon);
            }
            else
            {


                polyOverlay.Polygons.Add(GMapSurfaceAPoligon);
                polyOverlay.Polygons.Add(GMapSurfaceBPoligon);
                polyOverlay.Polygons.Add(GMapSurfaceCPoligon);
                polyOverlay.Polygons.Add(GMapSurfaceEPoligon);

                polyOverlay.Polygons.Add(GMapSurfaceNoiseBPoligon);
                polyOverlay.Polygons.Add(GMapSurfaceNoiseVPoligon);
                polyOverlay.Polygons.Add(GMapSurfaceNoiseGPoligon);
                


                polyOverlay.Polygons.Add(GMapSurfaceDPoligon);

                polyOverlay.Polygons.Add(GMapAPPVP_BPoligon);
                polyOverlay.Polygons.Add(GMapAPPVP_EPoligon);

                polyOverlay.Polygons.Add(GMapTakeoffSurfacePoligon);
                polyOverlay.Polygons.Add(GMapApproachSurfacePoligon);
                polyOverlay.Polygons.Add(GMapCancelledApproachSurfacePoligon);
                polyOverlay.Polygons.Add(GMapInnerApproachSurface);
                polyOverlay.Polygons.Add(GMapTransitionSurfacePoligon);
                polyOverlay.Polygons.Add(GMapTransitionSurfacePoligon2);
                polyOverlay.Polygons.Add(GMapInnerTransitionSurface);
                polyOverlay.Polygons.Add(GMapInnerTransitionSurface2);
            }


        }

        void ShowVPP()
        {
            GMapPolygon vpp1 = new GMapPolygon(APInf.Runways.items[0].PointsList(), "vpp1");
            vpp1.Fill = new SolidBrush(Color.FromArgb(70, Color.Black));
            vpp1.Stroke = new Pen(Color.Black, 2);
            polyOverlay.Polygons.Add(vpp1);

            GMapPolygon vpp2 = new GMapPolygon(APInf.Runways.items[1].PointsList(), "vpp2");
            vpp2.Fill = new SolidBrush(Color.FromArgb(70, Color.Black));
            vpp2.Stroke = new Pen(Color.Black, 2);
            polyOverlay.Polygons.Add(vpp2);
        }

        void ShowRouts()
        {
            List<PointLatLng> mPointsList1 = new List<PointLatLng>();

            mPointsList1 = new List<PointLatLng>();
            //mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, CMBaseGeoPoint.ShowDegreeFraction(61, 02, 18), 14010).Coordinates);
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 17512.5));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 14010));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 10507.5));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 7005));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 3502.5));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 1701.25));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayBegin.BackTrueCourse, 800.75));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 800.75));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 1701.25));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 3502.5));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 7005));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 10507.5));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 14010));
            mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayEnd.BackTrueCourse, 17512.5));
            //mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayEnd, CMBaseGeoPoint.ShowDegreeFraction(61, 1, 30), 14010).Coordinates);
            //mPointsList1.Add(CMGeoBase.GetCoordinate(APInf.CRW.RunwayBegin, CMBaseGeoPoint.ShowDegreeFraction(241, 02, 18), 14010).Coordinates);

            //ShowPolygonPoints(mPointsList1);

            polyOverlay.Routes.Add(new GMapRoute(mPointsList1, "2"));

            mPointsList1 = new List<PointLatLng>();
            mPointsList1.Add(APInf.CRW.RunwayBegin.Coordinates);
            mPointsList1.Add(APInf.CRW.RunwayEnd.Coordinates);
            polyOverlay.Routes.Add(new GMapRoute(mPointsList1, "4"));
        }


        //TODO процедура отрисовки точек полигона
        void ShowPolygonPoints(List<PointLatLng> poly)
        {
            RemoveGroupDot(enumGeoPointType.geoptPolyDot);
            for (int i = 0; i < poly.Count; i++)
            {
                polyOverlay.Markers.Add(CMGeoPoint.GetGMarkerGoogle(i.ToString(), poly[i], enumGeoPointType.geoptPolyDot, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.yellow_small)); //GetGMarkerGoogle
                ///TextResult.Text += string.Format("{0} {1}", poly[i].Lat, poly[i].Lng) + " \r\n"; ;
            }
        }

        void FillPointSpec(CMCheckedGeoPoint point)
        {
            point.Angle = Math.Round(CMGeoBase.GetAsimut(APInf.KTA, point, 0),2);
            point.Distance = Math.Round(CMGeoBase.GetDistance(APInf.KTA, point),2);
            point.HeightAboveRunwayBegin = point.Height - APInf.CRW.RunwayBegin.Height;
            point.Offset = OffsetRunwayBegin(point);

        }

        PointLatLng OffsetRunwayBegin(CMGeoPoint point)
        {
            PointLatLng v1 = CMGeoBase.Mercator(point.Coordinates);
            PointLatLng v2 = CMGeoBase.Mercator(RunwayBegin);

            double AzimutP1 = CMGeoBase.GetAsimut(APInf.CRW.RunwayBegin, point);
            double AzimutP2 = CMGeoBase.GetAsimut(APInf.CRW.RunwayEnd, APInf.CRW.RunwayBegin);

            double Angle = Math.Abs(AzimutP1 - APInf.CRW.RunwayBegin.BackTrueCourse);

            double K = 1;

            if (AzimutP2 >= 0 && AzimutP2 <= 180)
            {
                if (AzimutP1 >= AzimutP2 && AzimutP1 <= (AzimutP2 + 180))
                    K = -1;
                else
                    K = 1;

            }
            else
            {
                if (AzimutP1 <= AzimutP2 && AzimutP1 >= (AzimutP2 - 180))
                    K = 1;
                else
                    K = -1;
            }

            double Latitude = 0;//(v1.Lat - v2.Lat) * Math.Cos(Angle * DegToRad);
            double Longtitude = 0;//(v1.Lng - v2.Lng) * Math.Cos(Angle * DegToRad);
            double distance = CMGeoBase.GetDistance(APInf.CRW.RunwayBegin, point);

            double x = distance * Math.Cos(Angle * CMGeoBase.DegToRad);
            double y = Math.Abs((distance * Math.Sin(Angle * CMGeoBase.DegToRad))) * K;

            Latitude = Math.Round(y, 2);
            Longtitude = Math.Round(x, 2);

            PointLatLng v3 = new PointLatLng(Longtitude, Latitude);

            return v3;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < mCheckedGeoPoin.Count)
            {
                z = new GmapsSave(this.gmaper, polyOverlay);
                z.start();
                z.bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                //gmaper.Refresh();
            }
            else
                MessageBox.Show("Пожалуйста, выберите препятствие из списка", "Внимание...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < mCheckedGeoPoin.Count)
            {

                mCheckedGeoPoin[listBox1.SelectedIndex].ImageToBase64(Image.FromStream(z.ImageStream));
                gmaper.ReloadMap();

                Report rp = new Report(mCheckedGeoPoin[listBox1.SelectedIndex]);
                rp.Show();

            }
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {

            ShowInterception = ShowInterception == true ? false : true;
            button2.Text = ShowInterception == true ? "Отобразить все плоскости" : "Отключить не пересекаемые плоскости";

            if (gmaper.Overlays.Count != 0)
            {
                List<PointLatLng> ttx = new List<PointLatLng>();

                for (int i = 0; i < polyOverlay.Markers.Count; i++)
                {
                    if (polyOverlay.Markers[i] is GMap.NET.WindowsForms.Markers.GMarkerGoogle)
                    {
                        ttx.Add(polyOverlay.Markers[i].Position);
                    }
                }
                polyOverlay.Clear();
                gmaper.Overlays.Remove(polyOverlay);

                foreach (PointLatLng pp in ttx)
                {
                    GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new PointLatLng(pp.Lat, pp.Lng),
                  GMap.NET.WindowsForms.Markers.GMarkerGoogleType.green);
                    polyOverlay.Markers.Add(marker);
                }
            }
            GmapsDraw(gmaper);
        }

        private void gmaper_OnMapZoomChanged()
        {
            nudMapZoom.Value = (decimal)gmaper.Zoom;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            gmaper.MapProvider = comboBox1.SelectedItem as GMapProvider;
        }

        private void nudMapZoom_ValueChanged(object sender, EventArgs e)
        {
            gmaper.Zoom = (double)nudMapZoom.Value;
        }

        private void gmaper_MouseMove(object sender, MouseEventArgs e)
        {
            lblCurPosition.Text = string.Format("{0} {1}", CMBaseGeoPoint.ShowDegMiSec(gmaper.FromLocalToLatLng(e.X, e.Y).Lat), CMBaseGeoPoint.ShowDegMiSec(gmaper.FromLocalToLatLng(e.X, e.Y).Lng));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TextResult.Text = "";
            rtxtDotDescription.Text = "";
            textBox1.Text = "";
            //DataStorage.ClearList();
            mCheckedGeoPoin.Clear();
            listBox1.Items.Clear();
            RemoveGroupDot(enumGeoPointType.geoptObstacleDot);
        }

        private void RemoveGroupDot(enumGeoPointType geodottype)
        { 
            for (int i = polyOverlay.Markers.Count - 1; i >= 0; i--)
            {
                if (polyOverlay.Markers[i].Tag != null )
                    if (((CLMGeo.CMBaseGeoPoint)(polyOverlay.Markers[i].Tag)).GeoDotType == geodottype)
                    polyOverlay.Markers.RemoveAt(i);

                if (i > polyOverlay.Markers.Count)
                    i = polyOverlay.Markers.Count;
            }
        }


        private void gmaper_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ShowCheckedGeoPointEdt(enumGeoPointType.geoptObstacleDot, gmaper.FromLocalToLatLng(e.X, e.Y).Lat, gmaper.FromLocalToLatLng(e.X, e.Y).Lng, APInf.AirfieldHeight);
            
        }

        private void ShowCheckedGeoPointEdt(enumGeoPointType geodottype, double lat, double lng, double height = 0,string DotName = "")
        {
            CMFrmCheckedGeoPointEdt mform = new CMFrmCheckedGeoPointEdt(APInf, geodottype, lat, lng, height, DotName);

            mform.ShowDialog();

            if (mform.Dialog_Result)
                StartCheck(mform.CheckedGeoPoint);
        }
        
        private void ShowCheckedGeoPointEdt(enumGeoPointType geodottype, double lat, double lng, double height = 0)
        {
            CMFrmCheckedGeoPointEdt mform = new CMFrmCheckedGeoPointEdt(APInf,geodottype, lat, lng, height );
            
            mform.ShowDialog();

            if (mform.Dialog_Result)
                StartCheck(mform.CheckedGeoPoint);
        }

        private void ShowCheckedGeoPointEdt(enumGeoPointType geodottype)
        {
            CMFrmCheckedGeoPointEdt mform = new CMFrmCheckedGeoPointEdt(APInf,geodottype, APInf.KTA.Lat, APInf.KTA.Lng, APInf.AirfieldHeight);
            mform.ShowDialog();

            if (mform.Dialog_Result)
                StartCheck(mform.CheckedGeoPoint);
        }


        private void btnShowAPControlPoints_Click(object sender, EventArgs e)
        {
            if (showControlDot == false)
            {
                showControlDot = true;
                polyOverlay.Markers.Add(APInf.KTA.GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_small));

                for (int i = 0; i < APInf.Runways.Count; i++)
                {
                    polyOverlay.Markers.Add(APInf.Runways.items[i].RunwayThreshold1.GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_small,enumGeoPointType.geoptControlDot));
                    polyOverlay.Markers.Add(APInf.Runways.items[i].RunwayThreshold2.GetGMarkerGoogle(GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_small, enumGeoPointType.geoptControlDot));
                }
            
            }
            else
            {
                showControlDot = false;
                RemoveGroupDot(enumGeoPointType.geoptControlDot);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FindPointOnMap(textBox1.Text);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case  Keys.Return :
                    FindPointOnMap(textBox1.Text);
                    break;
                default:
                    break;
            }
        }

        // ищем координаты по ключевым словам
        private void FindPointOnMap(string searchstring)
        {
            if (textBox1.Text.Length > 0)
            {
                GeoCoderStatusCode stc = new GeoCoderStatusCode();

                //PointLatLng fPoint = (PointLatLng)GMap.NET.MapProviders.GoogleMapProvider.Instance.GetPoint(searchstring, out stc);
                object fPoint = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetPoint(searchstring, out stc);

                if (stc == GeoCoderStatusCode.G_GEO_SUCCESS)
                    ShowCheckedGeoPointEdt(enumGeoPointType.geoptObstacleDot, ((PointLatLng)fPoint).Lat, ((PointLatLng)fPoint).Lng, APInf.AirfieldHeight, searchstring);
                else
                    MessageBox.Show(string.Format("Что то пошло не так. Статус поиска - {0}",stc.ToString()),"Поиск адреса",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbListCoursesLA_SelectedIndexChanged(object sender, EventArgs e)
        {
            APInf.Runways.SelectRunwayFromListCoursesLA(cmbListCoursesLA.SelectedIndex);
            Configure(cmbListCoursesLA.SelectedIndex);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < mCheckedGeoPoin.Count)
            {
                //var t = polyOverlay.Markers[listBox1.SelectedIndex];
                rtxtDotDescription.Text = mCheckedGeoPoin[listBox1.SelectedIndex].ResultCheck + Environment.NewLine;
                rtxtDotDescription.Text += mCheckedGeoPoin[listBox1.SelectedIndex].CheckResult();
                gmaper.Position = mCheckedGeoPoin[listBox1.SelectedIndex].Coordinates;



            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            ShowCheckedGeoPointEdt(enumGeoPointType.geoptObstacleDot);
        }

        private void gmaper_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            int i = ((CMBaseGeoPoint)(item.Tag)).ItemNom;
            if (i >=0 && i < listBox1.Items.Count)
                listBox1.SelectedIndex = i;
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            DrawEllipses();
            //testproc();
            //Draw3Circle();
            //Draw3Circle1();
            //Draw3Circle2();
            //Draw3Sqrt();
        }

        

        double GetFi4Ellipse(double fi, double a, double b)
        {

            double Fi4Ellipse = 0;
            double X = a * Math.Cos((fi * CMGeoBase.DegToRad));
            double Y = -(b * Math.Sin((fi * CMGeoBase.DegToRad)));

            if (fi == 0 || fi == 90 || fi == 180 || fi == 270 || fi == 360)
                Fi4Ellipse = fi*CMGeoBase.DegToRad;
            else
            {
                
                if (fi > 0 && fi < 90)
                    Fi4Ellipse = Math.Atan(Math.Abs(Y / X));
                else if (fi > 90 && fi < 180)
                    Fi4Ellipse = (90 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(X / Y));
                else if (fi > 180 && fi < 270)
                    Fi4Ellipse = (180 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(Y / X));
                else if (fi > 270 && fi < 360)
                    Fi4Ellipse = (270 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(X / Y));
            }
            double _Fi4Ellipse = Fi4Ellipse * CMGeoBase.RadToDeg  + 61;
            return (_Fi4Ellipse > 360 ? _Fi4Ellipse - 360 : _Fi4Ellipse) ;
        }


        double GetFi4Ellipse_(double fi, double a, double b)
        {

            double Fi4Ellipse = 0;
            double FiSm = 60 * CMGeoBase.DegToRad;


            double _X = a * Math.Cos((fi * CMGeoBase.DegToRad));
            double _Y = -(b * Math.Sin((fi * CMGeoBase.DegToRad)));

            double X = Math.Cos(FiSm) * _X - (a * _Y/b) * Math.Sin(FiSm);
            double Y = (_X *b/a ) *  Math.Sin(FiSm) + _Y * Math.Cos(FiSm);


            if (fi == 0 || fi == 90 || fi == 180 || fi == 270 || fi == 360)
            { }//Fi4Ellipse = fi * CMGeoBase.DegToRad;
            else
            {

                if (fi > 0 && fi < 90)
                    Fi4Ellipse = Math.Atan(Math.Abs(Y / X));
                else if (fi > 90 && fi < 180)
                    Fi4Ellipse = (90 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(X / Y));
                else if (fi > 180 && fi < 270)
                    Fi4Ellipse = (180 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(Y / X));
                else if (fi > 270 && fi < 360)
                    Fi4Ellipse = (270 * CMGeoBase.DegToRad) + Math.Atan(Math.Abs(X / Y));
            }

            return Fi4Ellipse * CMGeoBase.RadToDeg;
        }



        void DrawEllipses()
        { 
             DrawEllipse(5947.81,726.22);


             DrawEllipse(8196.22,1007.73);


             DrawEllipse(11073.75, 1368.12);
        
        }
        void DrawEllipse(double a, double b)
        {

            List<PointLatLng> mPointsList = new List<PointLatLng>();

            for (double fi = 0; fi < 360; fi += 1)
            {

                double distance = Math.Sqrt(Math.Pow((a * Math.Cos((fi * CMGeoBase.DegToRad))), 2) + Math.Pow((b * Math.Sin((fi * CMGeoBase.DegToRad))), 2));
                double Fi4Ellipse = GetFi4Ellipse(fi, a, b);
                TextResult.Text += string.Format("< {0} << {1} d {2}", fi, Fi4Ellipse, distance) + " \r\n"; ;

                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, Fi4Ellipse, distance).Coordinates);
            }


            GMapPolygon GMapSurfaceAPoligon = new GMapPolygon(mPointsList, "Ellipse")
            {
                Fill = new SolidBrush(Color.FromArgb(3, Color.Black)),
                Stroke = new Pen(Color.Black, 1)
            };
            polyOverlay.Polygons.Add(GMapSurfaceAPoligon);

        }


        void DrawEllipse_()
        {
            double a = 5947.81;
            double b = 726.22;
            double cx = 0;
            double cy = 0;
            double Ugol = 60;
            double X = 0;
            double Y = 0;

            List<PointF> PFlist = new List<PointF>();
            List<PointLatLng> mPointsList = new List<PointLatLng>();

            for (int fi = 0; fi < 360; fi += 5)//шаг формирования точек 5 градусов
            {
                //PointF p = new PointF();
                X = a * (float)Math.Cos(fi * CMGeoBase.DegToRad);
                Y = b * (float)Math.Sin(fi * CMGeoBase.DegToRad);

                double tempX = X;
                double tempY = Y;
                X = cx + tempX * Math.Cos(-Ugol * CMGeoBase.DegToRad) + tempY * Math.Sin(-Ugol * CMGeoBase.DegToRad);
                Y = cy - (tempX * (-Math.Sin(-Ugol * CMGeoBase.DegToRad)) + tempY * Math.Cos(-Ugol * CMGeoBase.DegToRad));

                PFlist.Add(new PointF((float)(X), (float)(Y)));
                mPointsList.Add(CMGeoBase.GetCoordinate(APInf.KTA, fi, (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)))).Coordinates);

            }

            ShowPolygonPoints(mPointsList);

        }

        void testproc()    
        {
            //показать на карте точки, которые образуют данный полигон

            
            //GetAsimut( APInf.CRW.RunwayEnd, APInf.CRW.RunwayBegin);
            //GetAsimut(APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd);


            //CMGeoPoint FinishPoint = CMGeoBase.GetCoordinate(new CMGeoPoint(enumGeoPointType.geoptControlDot, 43.389269, 132.130642, 9.87, "07R"), CMBaseGeoPoint.ShowDegreeFraction(61, 1, 30), 3502.54);
            
            ////CMGeoPoint FinishPoint = GetCoordinate(new CMGeoPoint(enumGeoPointType.geoptControlDot, 43.389269, 132.130642, 9.87, "07R"), CMBaseGeoPoint.ShowDegreeFraction(61, 1, 30), 3502.54);
            //////CMGeoPoint FinishPoint = GetCoordinate(new CMGeoPoint(enumGeoPointType.geoptControlDot, CMBaseGeoPoint.ShowDegreeFraction(42, 0, 10.5), CMBaseGeoPoint.ShowDegreeFraction(105, 47, 19.4567), 14.8, "КТА"), CMBaseGeoPoint.ShowDegreeFraction(48, 18, 03.4), 25258.87);
            //TextResult.Text  =  string.Format ("{0} --- {1}",CMBaseGeoPoint.ShowDegMiSec(FinishPoint.Coordinates.Lat),CMBaseGeoPoint.ShowDegMiSec(FinishPoint.Coordinates.Lng));

            //TextResult.Text  += string.Format("{0}",CMGeoBase.GetDistance(APInf.CRW.RunwayBegin,APInf.CRW.RunwayEnd));

            TextResult.Text += string.Format("Начало ВПП {0}  {1}", CMBaseGeoPoint.ShowDegMiSec(APInf.CRW.RunwayBegin.Coordinates.Lat), CMBaseGeoPoint.ShowDegMiSec(APInf.CRW.RunwayBegin.Coordinates.Lng)) + " \r\n";
            TextResult.Text += string.Format("Конец ВПП {0}  {1}", CMBaseGeoPoint.ShowDegMiSec(APInf.CRW.RunwayEnd.Coordinates.Lat), CMBaseGeoPoint.ShowDegMiSec(APInf.CRW.RunwayEnd.Coordinates.Lng)) + " \r\n";

            

            //TextResult.Text += string.Format(" {0} ", GetAsimut(APInf.CRW.RunwayEnd, APInf.CRW.RunwayBegin)) + " \r\n";

            TextResult.Text += string.Format("Расстояние <=> {0} ", CMGeoBase.GetDistance(APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd)) + " \r\n";


            TextResult.Text += string.Format("А1 с К-> Н {0} ", CMBaseGeoPoint.ShowDegMiSec(CMGeoBase.GetAsimut(APInf.CRW.RunwayEnd, APInf.CRW.RunwayBegin, 0))) + " \r\n";
            TextResult.Text += string.Format("А2 с К-> Н {0} ", CMBaseGeoPoint.ShowDegMiSec(CMGeoBase.GetAsimut(APInf.CRW.RunwayEnd, APInf.CRW.RunwayBegin, 1))) + " \r\n";

            TextResult.Text += string.Format("А1 с Н-> К {0} ", CMBaseGeoPoint.ShowDegMiSec(CMGeoBase.GetAsimut(APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd, 0))) + " \r\n";
            TextResult.Text += string.Format("А2 с Н-> К {0} ", CMBaseGeoPoint.ShowDegMiSec(CMGeoBase.GetAsimut(APInf.CRW.RunwayBegin, APInf.CRW.RunwayEnd, 1))) + " \r\n";

            TextResult.Text += string.Format("Аold с Н-> К {0} ", CMBaseGeoPoint.ShowDegMiSec(CMGeoBase.Azimut(APInf.CRW.RunwayBegin.Coordinates, APInf.CRW.RunwayEnd.Coordinates))) + " \r\n";
            TextResult.Text += string.Format("Аold с К-> Н {0} ", CMBaseGeoPoint.ShowDegMiSec(CMGeoBase.Azimut(APInf.CRW.RunwayEnd.Coordinates, APInf.CRW.RunwayBegin.Coordinates))) + " \r\n";
        }

        private void nudAzimut_ValueChanged(object sender, EventArgs e)
        {
            
            gmaper.Bearing = (float)nudAzimut.Value;
            bool IsRotated = gmaper.IsRotated;
            
        }

        private void btnFileSave_Click(object sender, EventArgs e)
        {
            if (mCheckedGeoPoin.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string FileName = saveFileDialog1.FileName;

                    StreamWriter sw = new StreamWriter(FileName, false, Encoding.Default);

                    sw.WriteLine("PointName;LatDegree;LatMinutes;LatSeconds;LngDegree;LngMinutes;LngSeconds;X;Y;Angle;Distance;Height;Description");

                    for (int i = 0; i < mCheckedGeoPoin.Count; i++)
                    {
                        string sGeoPoint;
                        //sGeoPoint = string.Format("{0};{1};{2}", ((CLMGeo.CMGeoPoint)(mCheckedGeoPoin[0])).Lat, ((CLMGeo.CMGeoPoint)(mCheckedGeoPoin[0])).Lng, ((CLMGeo.CMGeoPoint)(mCheckedGeoPoin[0])).Height);

                        sGeoPoint = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}", 
                            ((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Text
                            , CMBaseGeoPoint.GetDegree(((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Lat)
                            , CMBaseGeoPoint.GetMinutes(((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Lat)
                            , CMBaseGeoPoint.GetSeconds(((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Lat)
                            , CMBaseGeoPoint.GetDegree(((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Lng)
                            , CMBaseGeoPoint.GetMinutes(((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Lng)
                            , CMBaseGeoPoint.GetSeconds(((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Lng)
                            , mCheckedGeoPoin[i].Offset.Lat
                            , mCheckedGeoPoin[i].Offset.Lng
                            , mCheckedGeoPoin[i].Angle
                            , mCheckedGeoPoin[i].Distance
                            , ((CLMGeo.CMGeoPoint)(mCheckedGeoPoin[i])).Height
                            , ((CLMGeo.CMCheckedGeoPoint)(mCheckedGeoPoin[i])).Description
                            );
                        sw.WriteLine(sGeoPoint);
                        TextResult.Text += sGeoPoint + " \r\n";


                    }
                    sw.Close();
                }
            }
            //
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string FileName = openFileDialog1.FileName;
                if (File.Exists(FileName))
                {
                    FileStream FS = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    StreamReader sr = new StreamReader(FS, Encoding.Default);
                    btnClear_Click(sender, e);
                    
                    while (sr.EndOfStream == false)
                    {
                        string sGeoPoint;    
                        sGeoPoint = sr.ReadLine();

                        MatchCollection coll;
                        Regex rStr = new Regex("^(?<PointName>[а-яА-Яa-zA-Z0-9 ]*)[;](?<LatDegree>[0-9]*)[;](?<LatMinutes>[0-9]*)[;](?<LatSeconds>[0-9.,]*)[;](?<LngDegree>[0-9]*)[;](?<LngMinutes>[0-9]*)[;](?<LngSeconds>[0-9.,]*)[;](?<Height>[0-9.,]*)[;](?<Description>[а-яА-Яa-zA-Z0-9 ]*)$");

                        coll = rStr.Matches(sGeoPoint);
                        if (coll.Count <= 0)
                        {
                            rStr = new Regex("^(?<PointName>[а-яА-Яa-zA-Z0-9 ]*)[;](?<LatDegree>[0-9]*)[;](?<LatMinutes>[0-9]*)[;](?<LatSeconds>[0-9.,]*)[;](?<LngDegree>[0-9]*)[;](?<LngMinutes>[0-9]*)[;](?<LngSeconds>[0-9.,]*)[;](?<X>[-0-9.,]*)[;](?<Y>[-0-9.,]*)[;](?<Angle>[-0-9.,]*)[;](?<Distance>[-0-9.,]*)[;](?<Height>[0-9.,]*)[;](?<Description>[а-яА-Яa-zA-Z0-9 ]*)$");

                            coll = rStr.Matches(sGeoPoint);
                        
                        }

                        if (coll.Count > 0)
                        {

                            CMCheckedGeoPoint CheckedGeoPoint = new CMCheckedGeoPoint(enumGeoPointType.geoptObstacleDot);
                            CheckedGeoPoint.Text = coll[0].Groups["PointName"].ToString();
                            CheckedGeoPoint.Height = Convert.ToDouble(coll[0].Groups["Height"].ToString());
                            CheckedGeoPoint.Description = coll[0].Groups["Description"].ToString();
                            CheckedGeoPoint.Lat = CMBaseGeoPoint.ShowDegreeFraction(Convert.ToDouble(coll[0].Groups["LatDegree"].ToString()), Convert.ToDouble(coll[0].Groups["LatMinutes"].ToString()), Convert.ToDouble(coll[0].Groups["LatSeconds"].ToString()));
                            CheckedGeoPoint.Lng = CMBaseGeoPoint.ShowDegreeFraction(Convert.ToDouble(coll[0].Groups["LngDegree"].ToString()), Convert.ToDouble(coll[0].Groups["LngMinutes"].ToString()), Convert.ToDouble(coll[0].Groups["LngSeconds"].ToString()));

                            StartCheck(CheckedGeoPoint);
                        }


                    }

                    sr.Close();
                    FS.Close();
                    FS = null;

                }
            
            }
        }
        
    }
}