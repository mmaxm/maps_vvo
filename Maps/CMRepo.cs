using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GMap.NET;
using System.Drawing;

namespace Maps
{
    public class CMRepo
    {
        public string ImageIn64 { get; set; }
        public double Distance { get; set; }
        public double Angle { get; set; }
        public double Height { get; set; }
        public double HeightAboveRunwayBegin { get; set; }
        public CMShowGeoCoord ShowLat;
        public CMShowGeoCoord ShowLng;
        public PointLatLng Offset { get; set; }

        private List<DataRow> ResultList = new List<DataRow>();

        public CMRepo()
        { 
        
        }

        public void AddData(DataRow InDataRow)
        {
            ResultList.Add(InDataRow);
        }

        public List<DataRow> GetResults()
        {
            return ResultList;
        }

        public void ClearList()
        {
            ResultList.Clear();
        }

        public void ImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                ImageIn64 = Convert.ToBase64String(imageBytes);

            }
        }
    }
}
