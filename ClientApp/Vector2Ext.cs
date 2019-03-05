using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public static class Vector2Ext
    {
        //public static Point ToPoint(this Vector2 v)
        //{
        //    return new Point((int)v.X, (int)v.Y);
        //}

        public static PointF ToPointF(this Vector2 v)
        {
            return new PointF(v.X, v.Y);
        }
    }
}
