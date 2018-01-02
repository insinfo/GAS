using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS
{
    [Serializable()]
    public class RectangleGraphic : GraphicObject
    {
        #region Constructors
        public RectangleGraphic() : base()
        {
            this.ObjectType = ObjectType.Rectangle;
        }

        public RectangleGraphic(MyPoint graphicPosition, string text) : this()
        {
            this.SetPosition(graphicPosition);
            this.Text = text;
        }
        #endregion

        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public string Text { get; set; } = "";
        public Color FontColor { get; set; } = Color.SteelBlue;
        public TextRenderingHint TextRenderStyle { get; set; } = TextRenderingHint.SystemDefault;
        public float LineWidth { get; set; } = 3;
        public bool GradientMode { get; set; } = false;
        public Color LineColor { get; set; } = Color.SteelBlue;
        public bool Fill { get; set; } = false;
        public Color FillColor { get; set; } = Color.LightSteelBlue;
        public Color GradientColor1 { get; set; } = Color.LightSteelBlue;
        public Color GradientColor2 { get; set; } = Color.SteelBlue;
        public bool RoundEdges { get; set; } = true;
        public int Opacity { get; set; } = 100;

        public override void Draw(Graphics g)
        {
            GraphicsContainer gContainer = null;
            Matrix myMatrix = null;

            gContainer = g.BeginContainer();
            myMatrix = g.Transform;

            if (m_Rotation != 0)
            {
                myMatrix.RotateAt(m_Rotation, new PointF(X, Y), MatrixOrder.Append);
                g.Transform = myMatrix;
            }

            g.TextRenderingHint = this.TextRenderStyle;           

           Rectangle rect = new System.Drawing.Rectangle(X, Y, this.Width, this.Height);

            var size = g.MeasureString(Text, Font, 1000);

            PointF pos = new PointF(X + (Width - size.Width) / 2.0F, Y + Height - size.Height - 4);

            this.LineColor = Color.FromArgb(this.Opacity, this.LineColor);
            //Me.FontColor = Color.FromArgb(Me.Opacity, Me.FontColor)
            this.FillColor = Color.FromArgb(this.Opacity, this.FillColor);
            this.GradientColor1 = Color.FromArgb(this.Opacity, this.GradientColor1);
            this.GradientColor2 = Color.FromArgb(this.Opacity, this.GradientColor2);

            //draw borders
            if (RoundEdges)
            {
                this.DrawRoundRect(g, new Pen(this.LineColor, this.LineWidth), X, Y, Width, Height, 3, Brushes.Transparent);
            }
            else
            {
                g.DrawRectangle(new Pen(this.LineColor, this.LineWidth), rect);
            }

            //draw actual rectangle
            if (GradientMode)
            {
                if (RoundEdges)
                {
                    this.DrawRoundRect(g, new Pen(Brushes.Transparent, 1), X, Y, Width, Height, 3, new LinearGradientBrush(rect, this.GradientColor1, this.GradientColor2, LinearGradientMode.Vertical));
                }
                else
                {
                    g.FillRectangle(new LinearGradientBrush(rect, this.GradientColor1, this.GradientColor2, LinearGradientMode.Vertical), rect);
                }
            }
            else
            {
                if (RoundEdges)
                {
                    this.DrawRoundRect(g, new Pen(Brushes.Transparent, 1), X, Y, Width, Height, 3, new SolidBrush(this.FillColor));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(this.FillColor), rect);
                }
            }

            //draw text
            g.DrawString(Text, Font, new SolidBrush(FontColor), pos);
            g.EndContainer(gContainer);

        }

    }

}
