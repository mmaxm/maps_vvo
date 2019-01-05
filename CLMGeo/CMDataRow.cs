using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMGeo
{
    public class CMDataRow
    {
        public string SurfaceName { get; set; }
        public bool Interseption { get; set; }
        public double PointHeight { get; set; }
        public double SurfaceHeight { get; set; }
        public double excess { get; set; }

        public CMDataRow()
        { }

    }
}

/*
 * public struct DataRow
    {
        public string SurfaceName { get; set; }
        public bool Interseption { get; set; }
        public double PointHeight { get; set; }
        public double SurfaceHeight { get; set; }
        public double excess { get; set; }
    }
 * */
