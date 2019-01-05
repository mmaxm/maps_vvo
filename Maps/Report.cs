using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Reporting.WinForms;
using CLMGeo;

namespace Maps
{
    public partial class Report : Form
    {

        //CMRepo localData;
        public CMCheckedGeoPoint mCheckedGeoPoint;



        public Report(CMCheckedGeoPoint value)
        {
            InitializeComponent();
            mCheckedGeoPoint = value;
        }

        public CMCheckedGeoPoint CheckedGeoPoint
        {
            set { mCheckedGeoPoint = value; }
        }

        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {

            List<CMCGPCheckedResult> list = mCheckedGeoPoint.GetResults();
            reportViewer1.LocalReport.DataSources.Clear(); //clear report
            reportViewer1.LocalReport.ReportEmbeddedResource = "Maps.Report1.rdlc"; // bind reportviewer with .rdlc
            //здесь мутим передачу строк
            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("SurfacesDS", list); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = list;

            //string temp = ImageToBase64(Image.FromFile(@"D:\temp\123.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            string PointOffset = "X = " + mCheckedGeoPoint.Offset.Lat + " м ,Y = " + mCheckedGeoPoint.Offset.Lng + " м";


            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", mCheckedGeoPoint.ImageIn64));
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("HeightOfPoint", mCheckedGeoPoint.Height.ToString() + " м"));
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("AzimutDalnost", mCheckedGeoPoint.Angle.ToString()));
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("PointOffset", PointOffset));
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("DistanceFromKTA", mCheckedGeoPoint.Distance.ToString() + " м"));
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("RunwayBeginHeight", mCheckedGeoPoint.HeightAboveRunwayBegin.ToString() + " м"));
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("DotName", mCheckedGeoPoint.Text));

            this.reportViewer1.RefreshReport();
        }

        

    }

    public struct DataRow
    {
        public string SurfaceName { get; set; }
        public bool Interseption { get; set; }
        public double PointHeight { get; set; }
        public double SurfaceHeight { get; set; }
        public double excess { get; set; }
    }


   
}
