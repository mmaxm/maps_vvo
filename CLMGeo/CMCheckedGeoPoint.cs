using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GMap.NET;
using System.Drawing;

namespace CLMGeo
{

    public class CMCheckedGeoPoint : CMGeoPoint
    {

        public string ResultCheck = "";
//        public bool IsObstacle = false;
        public double Distance = 0; // дистанция от точки
        public double Angle = 0; //угол
        public double HeightAboveRunwayBegin = 0;
        public PointLatLng Offset ;
        public string ImageIn64 { get; set; }
        public string Description = "";

        private List<CMCGPCheckedResult> mResultList = new List<CMCGPCheckedResult>();


        public CMCheckedGeoPoint()
        { 
        
        }

        public CMCheckedGeoPoint(enumGeoPointType geodottype)
            : base(geodottype)
        { 
        
        }

        public CMCheckedGeoPoint(enumGeoPointType geodottype, double lat, double lng, double height = 0)
            : base(geodottype, lat, lng, height)
        { 
        }

        public CMCheckedGeoPoint(enumGeoPointType geodottype, double lat, double lng, double height = 0, string text = "")
            : base(geodottype, lat, lng, height, text)
        {

        }

        public CMCheckedGeoPoint(enumGeoPointType geodottype, PointLatLng coordinates, double height = 0)
            : base(geodottype, coordinates, height)
        {
        }

        public void WriteResultCheck(string resultcheck)
        {
            ResultCheck += (ResultCheck.Length > 0 ? " \r\n" : "") + resultcheck;
        }

        public void AddData(CMCGPCheckedResult InDataRow)
        {
            mResultList.Add(InDataRow);
        }

        public List<CMCGPCheckedResult> GetResults()
        {
            return mResultList;
        }

        public void ClearList()
        {
            mResultList.Clear();
        }

        public string CheckResult()
        {
            string result = "";

            if (mResultList != null)
            {

                for (int i = 0; i < mResultList.Count; i++)
			    {
                    result += (mResultList[i].ResultText.Length>0 && mResultList[i].ResultText != null) ? (mResultList[i].ResultText + " \r\n") : "";
    			}

            }
            return result;
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

        //public List<DataRow> GetRows()
        //{
        //    List<DataRow> ldr = new System.Collections.Generic.List<DataRow>();

        //}



    }

    

}
