
namespace Demo.WindowsForms.CustomMarkers
{
   using System;
   using System.Drawing;
   using System.Runtime.Serialization;
   using GMap.NET;
   using GMap.NET.WindowsForms;

#if !PocketPC
   [Serializable]
   public class GMapMarkerCircle : GMapMarker, ISerializable
#else
   public class GMapMarkerCircle : GMapMarker
#endif
   {
      /// <summary>
      /// In Meters
      /// </summary>
      public int Radius;
      public bool CirleState = false;
      /// <summary>
      /// specifies how the outline is painted
      /// </summary>
      [NonSerialized]
#if !PocketPC
      public Pen Stroke = new Pen(Color.FromArgb(155, Color.MidnightBlue));
#else
      public Pen Stroke = new Pen(Color.MidnightBlue);
#endif

      /// <summary>
      /// background color
      /// </summary>
      [NonSerialized]
#if !PocketPC
      public Brush Fill = new SolidBrush(Color.FromArgb(30,Color.Transparent));
#else
      public Brush Fill = new System.Drawing.SolidBrush(Color.AliceBlue);
#endif

      /// <summary>
      /// is filled
      /// </summary>
      public bool IsFilled = true;

      public GMapMarkerCircle(PointLatLng p)
         : base(p)
      {
         Radius = 100; // 100m
         IsHitTestVisible = false;
      }



       /// 
      //public PointLatLng OtstupVPP(double distOtst, int mode, PointLatLng FRMPoint)
      //{
      //    double len = Math.Cos(FRMPoint.Lat * Math.PI / 180) * 40000 / 360;
      //    double x = 0;
      //    double y = 0;
      //    switch (mode)
      //    {
      //        case 1: //-90
      //            {
      //                x = FRMPoint.Lng + distOtst * Math.Cos((180 - 61) * Math.PI / 180) / len; // 90 - 61 + 270 + 180
      //                y = FRMPoint.Lat + distOtst * Math.Sin((180 - 61) * Math.PI / 180) / 111.1;
      //                break;
      //            }
      //        case 2: //0
      //            {
      //                x = FRMPoint.Lng + distOtst * Math.Cos((90 - 61) * Math.PI / 180) / len;
      //                y = FRMPoint.Lat + distOtst * Math.Sin((90 - 61) * Math.PI / 180) / 111.1;
      //                break;
      //            }
      //        case 3:  // 90
      //            {
      //                x = FRMPoint.Lng + distOtst * Math.Cos((360 - 61) * Math.PI / 180) / len;//90-61+270
      //                y = FRMPoint.Lat + distOtst * Math.Sin((360 - 61) * Math.PI / 180) / 111.1;
      //                break;
      //            }
      //        case 4:      //180             
      //            {
      //                x = FRMPoint.Lng + distOtst * Math.Cos((270 - 61) * Math.PI / 180) / len; //90-61+180
      //                y = FRMPoint.Lat + distOtst * Math.Sin((270 - 61) * Math.PI / 180) / 111.1;
      //                break;
      //            }
      //        default: break;

      //    }

      //    return new PointLatLng(y, x);

      //}
       /// ///////////////
       
      public override void OnRender(Graphics g)
      {
          
          int R = (int)((Radius) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat)) *2;

          ///new
          int twidth = (int)((3061.2188) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat));
          int theight = (int)((1696.8336) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat));

          //int twidth = (int)((3061.2188) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat));
          //int theight = (int)((1696.8336) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat));

          int x = LocalPosition.X - R / 2;
          int y = LocalPosition.Y - R / 2;
          int width = R;
          int height = R;
          int startAngle = 61;
          int sweepAngle = 180;
          

          if (IsFilled)
          {
              if (CirleState)
              {
                  g.FillEllipse(Fill, new System.Drawing.Rectangle(LocalPosition.X - R / 2, LocalPosition.Y - R / 2, R, R));
              }
              else
              {
                  System.Drawing.Drawing2D.GraphicsPath path1 = new System.Drawing.Drawing2D.GraphicsPath();

                  path1.AddArc(x, y, width, height, startAngle, sweepAngle);

                  g.FillPath(Fill, path1);
                  g.DrawPath(Stroke, path1);
              }
          }
          if (CirleState)
          { g.DrawEllipse(Stroke, new System.Drawing.Rectangle(LocalPosition.X - R / 2, LocalPosition.Y - R / 2, R, R)); }
          else

          {
              System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
              Pen blackPen = new Pen(Color.Black, 3);

              path2.AddArc(x, y, width, height, startAngle, sweepAngle);
              path2.AddArc(x + twidth, y - theight, width, height, startAngle - 180, sweepAngle);
              path2.AddArc(x, y, width, height, startAngle, sweepAngle);

              g.FillPath(Fill, path2);
              g.DrawPath(Stroke, path2);
              
              
          }

      }

      public override void Dispose()
      {
         if(Stroke != null)
         {
            Stroke.Dispose();
            Stroke = null;
         }

         if(Fill != null)
         {
            Fill.Dispose();
            Fill = null;
         }

         base.Dispose();
      }

#if !PocketPC

      #region ISerializable Members

      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);

         // TODO: Radius, IsFilled
      }

      protected GMapMarkerCircle(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
         // TODO: Radius, IsFilled
      }

      #endregion

#endif
   }
}
