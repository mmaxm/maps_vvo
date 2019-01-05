using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maps
{
    public class CMAPSurfaces
    {

        public CMAPSurfaceA SurfaceA ; // внутренняя горизонтальная поверхности
        public CMAPSurfaceB SurfaceB ; //конническая поверхности
        public CMAPSurfaceC SurfaceC; //R15
        public CMAPSurfaceC SurfaceOuterHorizontal; //

        public CMAPSurfaceD SurfaceD; //R60
        public CMAPSurfaceC SurfaceE; //R30

        public CMAPPVP APPVP_B;
        public CMApproachSurfaceData ApproachSurface ;
        public CMTransitionSurfaceData TransitionSurface ;
        public CMTakeoffSurfaceData TakeoffSurface ;
        public CMTakeoffSurfaceData TakeoffSurfaceInfo;
        public CMInnerApproachSurfaceData InnerApproachSurface ;
        public CMCancelledApproachSurfaceData CancelledApproachSurface ;
        public CMInnerTransitionSurfaceData InnerTransitionSurface ;
        public CMBAPSurfaceNoise SurfaceNoiseG;
        public CMBAPSurfaceNoise SurfaceNoiseV;
        public CMBAPSurfaceNoise SurfaceNoiseB;

        protected CMAirPortInfo mAPInf;

        public CMAPSurfaces()
        {
            SurfaceA = new CMAPSurfaceA();
            SurfaceB = new CMAPSurfaceB();
            SurfaceC = new CMAPSurfaceC();
            SurfaceOuterHorizontal = new CMAPSurfaceC();
            SurfaceD = new CMAPSurfaceD();
            SurfaceE = new CMAPSurfaceC();
            SurfaceNoiseG = new CMBAPSurfaceNoise(5947.81, 726.22, 61, "Шумовая зона Г");
            SurfaceNoiseV = new CMBAPSurfaceNoise(8196.22, 1007.73, 61, "Шумовая зона В");
            SurfaceNoiseB = new CMBAPSurfaceNoise(11073.75, 1368.12, 61, "Шумовая зона Б");

            APPVP_B = new CMAPPVP();

            ApproachSurface = new CMApproachSurfaceData();
            TransitionSurface = new CMTransitionSurfaceData();
            TakeoffSurface = new CMTakeoffSurfaceData(0.016, "Поверхность взлета");
            TakeoffSurfaceInfo = new CMTakeoffSurfaceData(0.012, "Информационная поверхность");

            InnerApproachSurface = new CMInnerApproachSurfaceData();
            CancelledApproachSurface = new CMCancelledApproachSurfaceData();
            InnerTransitionSurface = new CMInnerTransitionSurfaceData();            
        }

        public CMAPSurfaces(CMAirPortInfo apinf)
        {
            SurfaceA = new CMAPSurfaceA();
            SurfaceB = new CMAPSurfaceB();
            SurfaceC = new CMAPSurfaceC();
            SurfaceOuterHorizontal = new CMAPSurfaceC();
            SurfaceD = new CMAPSurfaceD();
            SurfaceNoiseG = new CMBAPSurfaceNoise(5947.81, 726.22,61,"Шумовая зона Г");
            SurfaceNoiseV = new CMBAPSurfaceNoise(8196.22, 1007.73, 61, "Шумовая зона В");
            SurfaceNoiseB = new CMBAPSurfaceNoise(11073.75, 1368.12, 61,"Шумовая зона Б");

            APPVP_B = new CMAPPVP();

            ApproachSurface = new CMApproachSurfaceData();
            TransitionSurface = new CMTransitionSurfaceData();

            TakeoffSurface = new CMTakeoffSurfaceData(0.016, "Поверхность взлета");
            TakeoffSurfaceInfo = new CMTakeoffSurfaceData(0.012, "Информационная поверхность");

            InnerApproachSurface = new CMInnerApproachSurfaceData();
            CancelledApproachSurface = new CMCancelledApproachSurfaceData();
            InnerTransitionSurface = new CMInnerTransitionSurfaceData();
            setapinf(apinf);
        }


        private void setapinf (CMAirPortInfo apinf)
        {
            SurfaceA.APInf = apinf;
            SurfaceB.APInf = apinf;
            SurfaceC.APInf = apinf;
            SurfaceOuterHorizontal.APInf = apinf;
            SurfaceE.APInf = apinf;
            SurfaceD.APInf = apinf;
            APPVP_B.APInf = apinf;
            ApproachSurface.APInf = apinf;
            TransitionSurface.APInf = apinf;
            TakeoffSurface.APInf = apinf;
            TakeoffSurfaceInfo.APInf = apinf;
            InnerApproachSurface.APInf = apinf;
            CancelledApproachSurface.APInf = apinf;
            InnerTransitionSurface.APInf = apinf;
            SurfaceNoiseG.APInf = apinf;
            SurfaceNoiseV.APInf = apinf;
            SurfaceNoiseB.APInf = apinf;

        }


        public void LoadPointList()
        { 
            
        }

        public CMAirPortInfo APInf
        {
            get { return mAPInf; }
            set 
            { 
                mAPInf = value;
                setapinf(mAPInf);
            }
        }

       
    }
}
