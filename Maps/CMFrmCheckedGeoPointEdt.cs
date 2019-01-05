using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CLMGeo;
//using CMGeoBase;
using System.Device.Location;
using GMap.NET;

namespace Maps
{
    public partial class CMFrmCheckedGeoPointEdt : Form
    {

        private CMCheckedGeoPoint mCheckedGeoPoint;
        private CMAirPortInfo mAPInf ;//= new CMAirPortInfo();
        public bool DialogResult = false;

        public CMFrmCheckedGeoPointEdt()
        {
            InitializeComponent();
        }

        public CMFrmCheckedGeoPointEdt(enumGeoPointType geodottype, double lat, double lng, double height = 0)
        {
            InitializeComponent();

            mCheckedGeoPoint = new CMCheckedGeoPoint(geodottype, lat, lng, height );
        }


        public CMFrmCheckedGeoPointEdt(CMAirPortInfo apinf, enumGeoPointType geodottype, double lat, double lng, double height = 0, string DotName="")
        {
            InitializeComponent();
            mAPInf = apinf;
            //txtText.Text = DotName;
            mCheckedGeoPoint = new CMCheckedGeoPoint(geodottype, lat, lng, height);
            mCheckedGeoPoint.Text = DotName;
        }

        public CMFrmCheckedGeoPointEdt(CMAirPortInfo apinf,enumGeoPointType geodottype, double lat, double lng, double height = 0)
        {
            InitializeComponent();
            mAPInf = apinf;
            mCheckedGeoPoint = new CMCheckedGeoPoint(geodottype, lat, lng, height);
        }

        public CMFrmCheckedGeoPointEdt(CMCheckedGeoPoint checkedgeopoint)
        {
            InitializeComponent();
            mCheckedGeoPoint = checkedgeopoint;
        }

        public CMFrmCheckedGeoPointEdt( CMAirPortInfo apinf,CMCheckedGeoPoint checkedgeopoint)
        {
            InitializeComponent();
            mCheckedGeoPoint = checkedgeopoint;
            mAPInf = apinf;
        }

        public CMAirPortInfo APInf
        {
            get { return mAPInf; }
            set { mAPInf = value; }
        }

        void SelectControl()
        {
            Lat_GrVal.Enabled = rb1.Checked;
            Lat_MinVal.Enabled = rb1.Checked;
            Lat_SecVal.Enabled = rb1.Checked;
            Lng_GrVal.Enabled = rb1.Checked;
            Lng_MinVal.Enabled = rb1.Checked;
            Lng_SecVal.Enabled = rb1.Checked;

            nudAzimut.Enabled = rb2.Checked;
            nudDistance.Enabled = rb2.Checked;

            //nudX.Enabled = rb3.Checked;
            //nudY.Enabled = rb3.Checked;
        }

        public  CMCheckedGeoPoint CheckedGeoPoint
        {
            get 
            { 
              if (mCheckedGeoPoint == null)
                mCheckedGeoPoint = new CMCheckedGeoPoint();
                
              return mCheckedGeoPoint; 
            }
            set { mCheckedGeoPoint = value; }
        }

        void LoadData()
        {
            if (mCheckedGeoPoint != null)
            {
                SelectControl();

                txtText.Text = mCheckedGeoPoint.Text;
                PHeight.Value = (decimal)mCheckedGeoPoint.Height;
                txtDescription.Text = mCheckedGeoPoint.Description;

                LoadData1();
                LoadData2();
                //LoadData3();
            }
        
        }

        void LoadData1()
        {
            Lat_GrVal.Value = (decimal)CMBaseGeoPoint.GetDegree(mCheckedGeoPoint.Lat);
            Lat_MinVal.Value = (decimal)CMBaseGeoPoint.GetMinutes(mCheckedGeoPoint.Lat);
            Lat_SecVal.Value = (decimal)CMBaseGeoPoint.GetSeconds(mCheckedGeoPoint.Lat);

            Lng_GrVal.Value = (decimal)CMBaseGeoPoint.GetDegree(mCheckedGeoPoint.Lng);
            Lng_MinVal.Value = (decimal)CMBaseGeoPoint.GetMinutes(mCheckedGeoPoint.Lng);
            Lng_SecVal.Value = (decimal)CMBaseGeoPoint.GetSeconds(mCheckedGeoPoint.Lng);        
        }

        void LoadData2()
        {
            nudAzimut.Value = (decimal)mCheckedGeoPoint.Angle;
            nudDistance.Value = (decimal)mCheckedGeoPoint.Distance;
        }


        private void CMFrmCheckedGeoPointEdt_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        void SaveData()
        {
            
            CheckedGeoPoint.Text = txtText.Text ;
            CheckedGeoPoint.Height = (double)PHeight.Value;
            CheckedGeoPoint.Description = txtDescription.Text;

            SaveData1();
            SaveData2();

            DialogResult = true;
            this.Close();
            
        }

        void CalcGeoPoint()
        {
            if (mCheckedGeoPoint != null)
            {
                CheckedGeoPoint.Coordinates = CMGeoBase.GetCoordinate(mAPInf.KTA.Coordinates, (double)nudAzimut.Value, (double)nudDistance.Value);
                LoadData1();
            }
        }


        void SaveData1()
        {
            CheckedGeoPoint.Lat = CMBaseGeoPoint.ShowDegreeFraction((double)Lat_GrVal.Value, (double)Lat_MinVal.Value, (double)Lat_SecVal.Value);
            CheckedGeoPoint.Lng = CMBaseGeoPoint.ShowDegreeFraction((double)Lng_GrVal.Value, (double)Lng_MinVal.Value, (double)Lng_SecVal.Value);        
        }

        void SaveData2()
        {
            CheckedGeoPoint.Angle = (double)nudAzimut.Value;
            CheckedGeoPoint.Distance = (double)nudDistance.Value;
        }


        private void cmdOk_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            SelectControl();
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            SelectControl();
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            SelectControl();
        }

        private void nudAzimut_Leave(object sender, EventArgs e)
        {
            CalcGeoPoint();
        }

        private void nudDistance_Leave(object sender, EventArgs e)
        {
            CalcGeoPoint();
        }


    }
}
