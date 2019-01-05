using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMGeo
{
    public class CMAPRunways
    {
        List<CMAPRunway> mAPRunways;
        int mCurrentRunwaysID = 0;

        public CMAPRunways()
        { 
            mAPRunways = new List<CMAPRunway>();
        }

        public int Count
        {
            get { return mAPRunways.Count; }
        }

        public void Add(CMAPRunway aprunway)
        {
            mAPRunways.Add(aprunway);
        }

        public void Remove(int index)
        { 
            if (index < mAPRunways.Count)
                mAPRunways.Remove(mAPRunways[index]);
        }

        public List<CMAPRunway> items
        {
            get { return mAPRunways; }
            set { mAPRunways = value; }
        }

        public CMAPRunway GetItem(int index)
        {
            if (index < mAPRunways.Count)
                return mAPRunways[index];
            else
                return null;
        }

        public void SetItem(int index,CMAPRunway value)
        {
            if (index < mAPRunways.Count)
                mAPRunways[index] = value;
        }

        public string GetListCoursesLanding()
        {
            string result="";
            for (int i = 0; i < mAPRunways.Count; i++)
            {
                result += string.Format("{0}{1}", (result.Length > 0 ? ", " : ""),mAPRunways[i].RunwayThreshold1.Text);
                result += string.Format("{0}{1}", (result.Length > 0 ? ", " : ""), mAPRunways[i].RunwayThreshold2.Text);
            }
            return result;
        }

        public string [] GetArrListCoursesLA()
        {
            string [] items = new string [mAPRunways.Count*2];
            
            for (int i = 0; i < mAPRunways.Count; i++)
            {
                int j = i * 2;
                items[j] = mAPRunways[i].RunwayThreshold1.Text;
                items[j+1] = mAPRunways[i].RunwayThreshold2.Text;
            }
            return items;
        }

        public CMAPRunway Current ()
        {
            if (mCurrentRunwaysID >= 0 && mCurrentRunwaysID < mAPRunways.Count)
                return mAPRunways[mCurrentRunwaysID];
             else
                return null;
        }

        public CMAPRunway Current(int index)
        {
            if (index >= 0)
            {
                if (SelectRunwayFromListCoursesLA(index))
                    return mAPRunways[mCurrentRunwaysID];
                else
                    return null;
            }
            else
                return null;
        }

        public bool SelectRunwayFromListCoursesLA(int index)
        {
            bool Result = false;

            if (index >= 0 && index /  2 < mAPRunways.Count)
            {
                mCurrentRunwaysID = index / 2;
                mAPRunways[mCurrentRunwaysID].Directional = ((index % 2 > 0) ? false : true);
                Result = true;
            }

            return Result;
        }
    }
}
