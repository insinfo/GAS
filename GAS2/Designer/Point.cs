using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS2
{
    public class MyPoint : IPoint
    {
        public MyPoint()
        {
        }

        public MyPoint(double x, double y)
        {
            this.X = Convert.ToInt32(x);
            this.Y = Convert.ToInt32(y);
        }

        public System.Drawing.Point ToSDPoint()
        {
            return new System.Drawing.Point(Convert.ToInt32(X), Convert.ToInt32(Y));
        }

        public System.Drawing.PointF ToSDPointF()
        {
            return new System.Drawing.PointF((float)X, (float)Y);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public void Offset(double p1, double p2)
        {
            X += p1;
            Y += p2;
        }

    }

}
