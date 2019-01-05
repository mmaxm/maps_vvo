using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMGeo
{
    public class CMCGPCheckedResult
    {
        public string mSurfaceName = "";
        public bool mIsObstacle = false;
        public double mPointHeight = 0;
        public double mSurfaceHeight = 0;
        public double mExceeding = 0;
        public string mResultText = "";
        
        public CMCGPCheckedResult()
        {

        }


        public string SurfaceName
        {
            get { return mSurfaceName; }
            set { mSurfaceName = value; }
        }
        public bool IsObstacle
        {
            get { return mIsObstacle; }
            set { mIsObstacle = value; }
        }

        public double PointHeight
        {
            get { return mPointHeight; }
            set { mPointHeight = value; }        
        }

        public double SurfaceHeight
        {
            get { return mSurfaceHeight; }
            set { mSurfaceHeight = value; }        
        }

        public double Exceeding
        {
            get { return mExceeding; }
            set { mExceeding = value; }        
        }

        public string ResultText
        {
            get { return mResultText; }
            set { mResultText = value; }        
        }
    
    }



}


/*
public struct CMCGPCheckedResult
    {
        public string SurfaceName ;
        public bool IsObstacle ;
        public double PointHeight ;
        public double SurfaceHeight ;
        public double Exceeding ;
        public string ResultText ;

        //public CMCGPCheckedResult()
        //{ 
        
        //}
    }
*/