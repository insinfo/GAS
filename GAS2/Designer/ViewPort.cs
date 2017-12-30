using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAS2
{
    public class ViewPort : UserControl
    {
        public float DPIH = 96;//DPI horizontal
        public float DPIV = 96;//DPI vertical
        public float Zoom = 1;//Zoom
        public List<Element> Elements = new List<Element>();
        public Element ElementSelected = null;
        public Point MouseClickStartPosition = new Point();
        public bool IsDragging = false;

        public ViewPort()
        {
            this.MouseClick += ViewPort_MouseClick;
            this.MouseMove += ViewPort_MouseMove;
            this.MouseDown += ViewPort_MouseDown;
            this.MouseUp += ViewPort_MouseUp;
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void ViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            MouseClickStartPosition = new Point(e.X, e.Y);
            ElementSelected = this.FindObjectAtPoint(MouseClickStartPosition);
            if (ElementSelected != null)
            {
                IsDragging = true;
            }                        
        }

        private void ViewPort_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ViewPort_MouseMove(object sender, MouseEventArgs e)
        {         
            // get the current mouse position
            var mx = e.X;
            var my = e.Y;

            // calculate the distance the mouse has moved
            // since the last mousemove
            var dx = mx - MouseClickStartPosition.X;
            var dy = my - MouseClickStartPosition.Y;
            
            if (IsDragging)
            {
                ElementSelected.X += dx;
                ElementSelected.Y += dy;
                this.Invalidate();
            }

            // reset the starting mouse position for the next mousemove
            MouseClickStartPosition.X = mx;
            MouseClickStartPosition.Y = my;
        }

        private void ViewPort_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(Color.FromArgb(200, 200, 200));

            this.DPIH = g.DpiX;
            this.DPIV = g.DpiY;

            g.ScaleTransform(Zoom, Zoom);
            g.PageUnit = GraphicsUnit.Pixel;
            DrawObjects(g);
        }

        public void DrawObjects(Graphics g)
        {
            foreach (Element element in Elements)
            {
                element.Draw(g);
            }
        }

        public void AddElement(Element element)
        {
            Elements.Add(element);
            this.Invalidate();
        }

        public Element FindObjectAtPoint(Point pt)
        {
            var elementsCount = Elements.Count;
            if (elementsCount > 0)
            {
                for (int i = elementsCount - 1; i >= 0; i--)
                {
                    Element element = Elements[i];
                    if (element.HitTest(pt))
                    {
                        return element;
                    }
                }
            }
            return null;
        }
    }

    public class Element
    {
        public int X { get; set; } = 10;
        public int Y { get; set; } = 10;
        public int Width { get; set; } = 300;
        public int Height { get; set; } = 100;
        public float Rotation { get; set; } = 0;
        public Color BackgroundColor { get; set; } = Color.Gray;
        public Color ForegroundColor { get; set; } = Color.Red;

        public virtual void Draw(Graphics g)
        {

        }

        public virtual bool HitTest(Point pt)
        {
            GraphicsPath gp = new GraphicsPath();
            Matrix myMatrix = new Matrix();
            gp.AddRectangle(new Rectangle(X, Y, Width, Height));
            if (this.Rotation != 0)
            {
                myMatrix.RotateAt(this.Rotation, new PointF(this.X + this.Width / 2.0F,
                    this.Y + this.Height / 2.0F), MatrixOrder.Append);
            }
            gp.Transform(myMatrix);
            return gp.IsVisible(pt);
        }
    }

    public class RoundedRectangleElement : Element
    {
        public float Radius { get; set; } = 50;

        public override void Draw(Graphics g)
        {
            if (base.Width / 2.0 < this.Radius)
            {
                this.Radius = base.Width / 2.0F - 2;
            }
            else if (base.Height / 2.0 < this.Radius)
            {
                this.Radius = base.Height / 2.0F - 2;
            }

            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(base.X + Radius, base.Y, X + base.Width - this.Radius, base.Y);
            gp.AddArc(X + Width - Radius, Y, Radius, Radius, 270, 90);
            gp.AddLine(X + Width, Y + Radius, X + Width, Y + Height - Radius);
            gp.AddArc(X + Width - Radius, Y + Height - Radius, Radius, Radius, 0, 90);
            gp.AddLine(X + Width - Radius, Y + Height, X + Radius, Y + Height);
            gp.AddArc(X, Y + Height - Radius, Radius, Radius, 90, 90);
            gp.AddLine(X, Y + Height - Radius, X, Y + Radius);
            gp.AddArc(X, Y, Radius, Radius, 180, 90);
            gp.CloseFigure();

            //g.DrawPath(new Pen(Color.FromArgb(50, 170, 215), 1), gp);          
            var br = new SolidBrush(BackgroundColor);
            g.FillPath(br, gp);
            br.Dispose();
            gp.Dispose();
        }
    }

    public class TextElement : Element
    {
        public string Text { get; set; } = "test test";
        public float FontSize { get; set; } = 24f;
        public Font FontFace { get; set; } = new Font("Arial", 24f);

        public override void Draw(Graphics g)
        {
            var bouds = new Rectangle(X, Y, Width, Height);
            TextRenderer.DrawText(g, Text, this.FontFace,
                bouds, ForegroundColor);
                       
        }
    }
}
