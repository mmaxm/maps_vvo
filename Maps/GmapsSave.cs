﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using GMap.NET.MapProviders;

namespace Maps
{
    internal class GmapsSave : IDisposable
    {
        public MemoryStream ImageStream = new MemoryStream();

        public BackgroundWorker bg = new BackgroundWorker();
        readonly List<GPoint> tileArea = new List<GPoint>();
        GMapControl GmapsControl;
        GMapOverlay GmapsObjects ;//= new GMapOverlay("objects");
        RectLatLng AreaGpx = RectLatLng.Empty;

        public GmapsSave(GMapControl InGmapcontrol,GMapOverlay InGmapsOverlay)
            {
                
                GmapsControl = InGmapcontrol;
                GmapsObjects = InGmapsOverlay;
                bg.WorkerReportsProgress = true;
                bg.WorkerSupportsCancellation = true;
                bg.DoWork += new DoWorkEventHandler(bg_DoWork);
                bg.ProgressChanged += new ProgressChangedEventHandler(bg_ProgressChanged);
                //bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            }
          
        void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GPoint p = (GPoint)e.UserState;
        }
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            MapInfo info = (MapInfo)e.Argument;
            if (!info.Area.IsEmpty)
            {

                string bigImage = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + Path.DirectorySeparatorChar + "GMap at zoom " + info.Zoom + " - " + info.Type + "-" + DateTime.Now.Ticks + ".png";
                e.Result = bigImage;
                
                // current area
                GPoint topLeftPx = info.Type.Projection.FromLatLngToPixel(info.Area.LocationTopLeft, info.Zoom);
                GPoint rightButtomPx = info.Type.Projection.FromLatLngToPixel(info.Area.Bottom, info.Area.Right, info.Zoom);
                GPoint pxDelta = new GPoint(rightButtomPx.X - topLeftPx.X, rightButtomPx.Y - topLeftPx.Y);
                GMap.NET.GSize maxOfTiles = info.Type.Projection.GetTileMatrixMaxXY(info.Zoom);

                int padding = info.MakeWorldFile ? 0 : 22;
                {
                    using (Bitmap bmpDestination = new Bitmap((int)(pxDelta.X + padding * 2), (int)(pxDelta.Y + padding * 2)))
                    {
                        using (Graphics gfx = Graphics.FromImage(bmpDestination))
                        {
                            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gfx.SmoothingMode = SmoothingMode.HighQuality;

                            int i = 0;

                            // get tiles & combine into one
                            lock (tileArea)
                            {
                                foreach (var p in tileArea)
                                {
                                    if (bg.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    int pc = (int)(((double)++i / tileArea.Count) * 100);
                                    bg.ReportProgress(pc, p);

                                    foreach (var tp in info.Type.Overlays)
                                    {
                                        Exception ex;
                                        GMapImage tile;

                                        // tile number inversion(BottomLeft -> TopLeft) for pergo maps
                                        if (tp.InvertedAxisY)
                                        {
                                            tile = GMaps.Instance.GetImageFrom(tp, new GPoint(p.X, maxOfTiles.Height - p.Y), info.Zoom, out ex) as GMapImage;
                                        }
                                        else // ok
                                        {
                                            tile = GMaps.Instance.GetImageFrom(tp, p, info.Zoom, out ex) as GMapImage;
                                        }

                                        if (tile != null)
                                        {
                                            using (tile)
                                            {
                                                long x = p.X * info.Type.Projection.TileSize.Width - topLeftPx.X + padding;
                                                long y = p.Y * info.Type.Projection.TileSize.Width - topLeftPx.Y + padding;
                                                {
                                                    gfx.DrawImage(tile.Img, x, y, info.Type.Projection.TileSize.Width, info.Type.Projection.TileSize.Height);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // draw polygons
                            {
                                foreach (GMapPolygon r in GmapsObjects.Polygons)
                                {
                                    if (r.IsVisible)
                                    {
                                        using (GraphicsPath rp = new GraphicsPath())
                                        {
                                            for (int j = 0; j < r.Points.Count; j++)
                                            {
                                                var pr = r.Points[j];
                                                GPoint px = info.Type.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, info.Zoom);

                                                px.Offset(padding, padding);
                                                px.Offset(-topLeftPx.X, -topLeftPx.Y);

                                                GPoint p2 = px;

                                                if (j == 0)
                                                {
                                                    rp.AddLine(p2.X, p2.Y, p2.X, p2.Y);
                                                }
                                                else
                                                {
                                                    System.Drawing.PointF p = rp.GetLastPoint();
                                                    rp.AddLine(p.X, p.Y, p2.X, p2.Y);
                                                }
                                            }

                                            if (rp.PointCount > 0)
                                            {
                                                rp.CloseFigure();

                                                gfx.FillPath(r.Fill, rp);

                                                gfx.DrawPath(r.Stroke, rp);
                                            }
                                        }
                                    }
                                }
                            }

                            // draw markers
                            {
                                foreach (GMapMarker r in GmapsObjects.Markers)
                                {
                                    if (r.IsVisible)
                                    {
                                        var pr = r.Position;
                                        GPoint px = info.Type.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, info.Zoom);

                                        px.Offset(padding, padding);
                                        px.Offset(-topLeftPx.X, -topLeftPx.Y);
                                        px.Offset(r.Offset.X, r.Offset.Y);

                                        r.LocalPosition = new System.Drawing.Point((int)px.X, (int)px.Y);

                                        r.OnRender(gfx);
                                    }
                                }

                                // tooltips above
                                foreach (GMapMarker m in GmapsObjects.Markers)
                                {
                                    if (m.IsVisible && m.ToolTip != null && m.IsVisible)
                                    {
                                        if (!string.IsNullOrEmpty(m.ToolTipText))
                                        {
                                            m.ToolTip.OnRender(gfx);
                                        }
                                    }
                                }
                            }

                            // draw info
                            {
                                System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
                                {
                                    rect.Location = new System.Drawing.Point(padding, padding);
                                    rect.Size = new System.Drawing.Size((int)pxDelta.X, (int)pxDelta.Y);
                                }

                                using (Font f = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold))
                                {
                                    // draw bounds & coordinates
                                    using (Pen p = new Pen(Brushes.DimGray, 3))
                                    {
                                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

                                        gfx.DrawRectangle(p, rect);

                                        string topleft = info.Area.LocationTopLeft.ToString();
                                        SizeF s = gfx.MeasureString(topleft, f);

                                        gfx.DrawString(topleft, f, p.Brush, rect.X + s.Height / 2, rect.Y + s.Height / 2);

                                        string rightBottom = new PointLatLng(info.Area.Bottom, info.Area.Right).ToString();
                                        SizeF s2 = gfx.MeasureString(rightBottom, f);

                                        gfx.DrawString(rightBottom, f, p.Brush, rect.Right - s2.Width - s2.Height / 2, rect.Bottom - s2.Height - s2.Height / 2);
                                    }

                                    // draw scale
                                    using (Pen p = new Pen(Brushes.Blue, 1))
                                    {
                                        double rez = info.Type.Projection.GetGroundResolution(info.Zoom, info.Area.Bottom);
                                        int px100 = (int)(100.0 / rez); // 100 meters
                                        int px1000 = (int)(1000.0 / rez); // 1km   

                                        gfx.DrawRectangle(p, rect.X + 10, rect.Bottom - 20, px1000, 10);
                                        gfx.DrawRectangle(p, rect.X + 10, rect.Bottom - 20, px100, 10);

                                        string leftBottom = "scale: 100m | 1Km";
                                        SizeF s = gfx.MeasureString(leftBottom, f);
                                        gfx.DrawString(leftBottom, f, p.Brush, rect.X + 10, rect.Bottom - s.Height - 20);
                                    }
                                }
                            }
                        }

                        bmpDestination.Save(ImageStream, ImageFormat.Jpeg);
                        
                       
                    }
                }

                //The worldfile for the original image is:

                //0.000067897543      // the horizontal size of a pixel in coordinate units (longitude degrees in this case);
                //0.0000000
                //0.0000000
                //-0.0000554613012    // the comparable vertical pixel size in latitude degrees, negative because latitude decreases as you go from top to bottom in the image.
                //-111.743323868834   // longitude of the pixel in the upper-left-hand corner.
                //35.1254392635083    // latitude of the pixel in the upper-left-hand corner.

                // generate world file
                if (info.MakeWorldFile)
                {
                    string wf = bigImage + "w";
                    using (StreamWriter world = File.CreateText(wf))
                    {
                        world.WriteLine("{0:0.000000000000}", (info.Area.WidthLng / pxDelta.X));
                        world.WriteLine("0.0000000");
                        world.WriteLine("0.0000000");
                        world.WriteLine("{0:0.000000000000}", (-info.Area.HeightLat / pxDelta.Y));
                        world.WriteLine("{0:0.000000000000}", info.Area.Left);
                        world.WriteLine("{0:0.000000000000}", info.Area.Top);
                        world.Close();
                    }
                }
            }
        }

        public void start()
        {
            RectLatLng? area = null;

           // GmapsControl.SelectedArea = new RectLatLng(43.438430, 132.053190, 0.2, 0.1);
            //GmapsControl.SelectedArea = new RectLatLng(43.451366, 132.025624, 0.2, 0.1);

                area = GmapsControl.SelectedArea;

                if (area.Value.IsEmpty)
                {
                    MessageBox.Show("Select map area holding ALT", "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int zoomzoom = Convert.ToInt32(GmapsControl.Zoom) + 1;

                if (!bg.IsBusy)
                {
                    lock (tileArea)
                    {

                        tileArea.Clear();
                        tileArea.AddRange(GmapsControl.MapProvider.Projection.GetAreaTileList(area.Value, zoomzoom, 1));
                        tileArea.TrimExcess();
                    }

                    bg.RunWorkerAsync(new MapInfo(area.Value, zoomzoom, GmapsControl.MapProvider, false));

                }

                

            
        }

        public struct MapInfo
        {
            public RectLatLng Area;
            public int Zoom;
            public GMapProvider Type;
            public bool MakeWorldFile;

            public MapInfo(RectLatLng Area, int Zoom, GMapProvider Type, bool makeWorldFile)
            {
                this.Area = Area;
                this.Zoom = Zoom;
                this.Type = Type;
                this.MakeWorldFile = makeWorldFile;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }
                ImageStream.Close();
                //ImageStream.Dispose();
                ImageStream = null;

                bg.Dispose();
                bg = null;
                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~GmapsSave() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
