using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS2
{
    [Serializable()]
    public class GraphicObjectCollection : List<GraphicObject>
    {
        protected int m_HorizRes = 72;
        protected int m_VertRes = 72;

        public void DrawSelectedObject(Graphics g, GraphicObject selectedObject, float Scale)
        {
            GraphicsContainer gCon1 = null;
            GraphicsContainer gCon2 = null;

            gCon1 = g.BeginContainer();

            g.ScaleTransform(Scale, Scale, MatrixOrder.Append);

            gCon2 = g.BeginContainer();

            g.PageUnit = GraphicsUnit.Pixel;

            if (selectedObject != null)
            {
                Rectangle hoverRect = new Rectangle();
                switch (selectedObject.ObjectType)
                {
                    case ObjectType.Rectangle:
                    case ObjectType.Text:
                    case ObjectType.Image:
                        hoverRect.X = selectedObject.X - 10;
                        hoverRect.Y = selectedObject.Y - 10;
                        hoverRect.Height = selectedObject.Height + 20;
                        hoverRect.Width = selectedObject.Width + 20;
                        //Dim strdist As SizeF = g.MeasureString(selectedObject.Tag, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel, 0, False), New PointF(0, 0), New StringFormat(StringFormatFlags.NoClip, 0))
                        break;
                    default:
                        hoverRect.X = selectedObject.X - 10;
                        hoverRect.Y = selectedObject.Y - 10;
                        hoverRect.Height = selectedObject.Height + 30;
                        hoverRect.Width = selectedObject.Width + 20;
                        SizeF strdist = g.MeasureString(selectedObject.Tag, new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel, 0, false), new PointF(0, 0), new StringFormat(StringFormatFlags.NoClip, 0));
                        if (strdist.Width > selectedObject.Width)
                        {
                            hoverRect.X = (int)(selectedObject.X + (selectedObject.Width - strdist.Width) / 2.0F - 10);
                            hoverRect.Width = (int)(strdist.Width + 20);
                        }
                        break;
                }

                Matrix myOriginalMatrix = g.Transform;
                if (selectedObject.Rotation != 0)
                {
                    myOriginalMatrix.RotateAt(selectedObject.Rotation, new PointF((float)(hoverRect.X + hoverRect.Width / 2.0), (float)(hoverRect.Y + hoverRect.Height / 2.0)), MatrixOrder.Append);
                    g.Transform = myOriginalMatrix;
                }
                g.PageUnit = GraphicsUnit.Pixel;
                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                color1 = Color.FromArgb(50, 130, 185, 200);
                color2 = Color.FromArgb(70, 2, 100, 130);
                color3 = Color.FromArgb(50, 2, 100, 130);
                if (hoverRect.Width > 0)
                {
                    LinearGradientBrush gbrush = new LinearGradientBrush(hoverRect, color1, color2, LinearGradientMode.Vertical);
                    DrawRoundRect(g, new Pen(color3, 1), hoverRect.X, hoverRect.Y, hoverRect.Width, hoverRect.Height, 15, gbrush);
                    //g.Transform = myOriginalMatrix
                }
            }
            g.EndContainer(gCon2);
            g.EndContainer(gCon1);
        }

        public void DrawRoundRect(Graphics g, Pen p, int x, int y, int width, int height, int radius, Brush myBrush)
        {
            if (width / 2.0 < radius)
            {
                radius = Convert.ToInt32(width / 2.0 - 2);
            }
            else if (height / 2.0 < radius)
            {
                radius = Convert.ToInt32(height / 2.0 - 2);
            }

            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - radius, y);
            gp.AddArc(x + width - radius, y, radius, radius, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - radius);
            gp.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            gp.AddLine(x + width - radius, y + height, x + radius, y + height);
            gp.AddArc(x, y + height - radius, radius, radius, 90, 90);
            gp.AddLine(x, y + height - radius, x, y + radius);
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.CloseFigure();

            g.DrawPath(p, gp);
            g.FillPath(myBrush, gp);
            gp.Dispose();
        }

        public void DrawObjects(Graphics g, float Scale, bool transparent)
        {
            GraphicObject drawObj = null;
            int i = 0;
            GraphicsContainer gCon = null;
            Matrix myOriginalMatrix = g.Transform;
            gCon = g.BeginContainer();
            g.PageUnit = GraphicsUnit.Pixel;
            g.ScaleTransform(Scale, Scale);
            Color oldlinecolor = new Color();
            Color oldfillcolor = new Color();
            Color oldgradcolor1 = new Color();
            Color oldgradcolor2 = new Color();
            if (this.Count > 0)
            {
                for (i = 0; i < this.Count; i++)
                {
                    drawObj = (GraphicObject)this[i];
                    if (drawObj.ObjectType == ObjectType.Rectangle)
                    {
                        if (transparent && !drawObj.Selected)
                        {
                            RectangleGraphic tempVar = (RectangleGraphic)drawObj;
                            oldlinecolor = tempVar.LineColor;
                            oldfillcolor = tempVar.FillColor;
                            oldgradcolor1 = tempVar.GradientColor1;
                            oldgradcolor2 = tempVar.GradientColor2;
                            tempVar.LineColor = Color.FromArgb(50, tempVar.LineColor);
                            tempVar.FillColor = Color.FromArgb(50, tempVar.FillColor);
                            tempVar.GradientColor1 = Color.FromArgb(50, tempVar.GradientColor1);
                            tempVar.GradientColor2 = Color.FromArgb(50, tempVar.GradientColor2);
                        }
                        drawObj.Draw(g);
                        
                        if (transparent && !drawObj.Selected)
                        {
                            RectangleGraphic tempVar2 = (RectangleGraphic)drawObj;
                            tempVar2.LineColor = oldlinecolor;
                            tempVar2.FillColor = oldfillcolor;
                            tempVar2.GradientColor1 = oldgradcolor1;
                            tempVar2.GradientColor2 = oldgradcolor2;
                        }
                    }
                }
                #region não implementado
                /*for (i = 0; i < this.Count; i++)
                {
                    drawObj = (GraphicObject)this[i];
                    if (drawObj.ObjectType == ObjectType.Nenhum)
                    {
                        if (drawObj is ShapeGraphic && transparent && !drawObj.Selected)
                        {
                            ShapeGraphic tempVar3 = (ShapeGraphic)drawObj;
                            oldlinecolor = tempVar3.LineColor;
                            oldfillcolor = tempVar3.FillColor;
                            oldgradcolor1 = tempVar3.GradientColor1;
                            oldgradcolor2 = tempVar3.GradientColor2;
                            tempVar3.LineColor = Color.FromArgb(50, tempVar3.LineColor);
                            tempVar3.FillColor = Color.FromArgb(50, tempVar3.FillColor);
                            tempVar3.GradientColor1 = Color.FromArgb(50, tempVar3.GradientColor1);
                            tempVar3.GradientColor2 = Color.FromArgb(50, tempVar3.GradientColor2);
                            tempVar3.SemiTransparent = true;
                        }
                        if (drawObj.ObjectType != ObjectType.GO_Rectangle)
                        {
                            if (drawObj.DrawOverride != null)
                            {
                                drawObj.DrawOverride.Invoke(g);
                            }
                            else
                            {
                                drawObj.Draw(g);
                            }
                        }
                        if (drawObj is ShapeGraphic && transparent && !drawObj.Selected)
                        {
                            ShapeGraphic tempVar4 = (ShapeGraphic)drawObj;
                            tempVar4.LineColor = oldlinecolor;
                            tempVar4.FillColor = oldfillcolor;
                            tempVar4.GradientColor1 = oldgradcolor1;
                            tempVar4.GradientColor2 = oldgradcolor2;
                            tempVar4.SemiTransparent = false;
                        }
                    }
                }*/
                /*for (i = 0; i < this.Count; i++)
                {
                    drawObj = (GraphicObject)this.Item(i);
                    if (drawObj.ObjectType != ObjectType.Nenhum)
                    {
                        if (drawObj is ShapeGraphic && transparent && !drawObj.Selected)
                        {
                            ShapeGraphic tempVar5 = (ShapeGraphic)drawObj;
                            oldlinecolor = tempVar5.LineColor;
                            oldfillcolor = tempVar5.FillColor;
                            oldgradcolor1 = tempVar5.GradientColor1;
                            oldgradcolor2 = tempVar5.GradientColor2;
                            tempVar5.SemiTransparent = true;
                        }
                        if (drawObj.ObjectType != ObjectType.GO_Rectangle)
                        {
                            if (drawObj.DrawOverride != null)
                            {
                                drawObj.DrawOverride.Invoke(g);
                            }
                            else
                            {
                                drawObj.Draw(g);
                            }
                        }
                        if (drawObj is ShapeGraphic && transparent && !drawObj.Selected)
                        {
                            ShapeGraphic tempVar6 = (ShapeGraphic)drawObj;
                            tempVar6.LineColor = oldlinecolor;
                            tempVar6.FillColor = oldfillcolor;
                            tempVar6.GradientColor1 = oldgradcolor1;
                            tempVar6.GradientColor2 = oldgradcolor2;
                            tempVar6.SemiTransparent = false;
                        }
                    }
                }*/
#endregion
            }
            g.EndContainer(gCon);
            g.Transform = myOriginalMatrix;
        }

        public void PrintObjects(Graphics g, int dx, int dy)
        {
            GraphicObject drawObj = null;
            int i = 0;
            g.PageUnit = GraphicsUnit.Pixel;
            if (this.Count > 0)
            {
                for (i = 0; i < this.Count; i++)
                {
                    drawObj = (GraphicObject)this[i];
                    drawObj.X += dx;
                    drawObj.Y += dy;
                    drawObj.Draw(g);
                }
                for (i = 0; i < this.Count; i++)
                {
                    drawObj = (GraphicObject)this[i];
                    drawObj.X -= dx;
                    drawObj.Y -= dy;
                }
            }
        }

        public GraphicObject FindObjectAtPoint(GAS2.MyPoint pt)
        {
            List<GraphicObject> objlist = new List<GraphicObject>();

            GraphicObject drawObj = null;
            int i = 0;
            if (this.Count > 0)
            {
                for (i = this.Count - 1; i >= 0; i--)
                {
                    drawObj = (GraphicObject)this[i];
                    if (drawObj.HitTest(pt.ToSDPoint()))
                    {
                        objlist.Add(drawObj);
                    }
                }
            }

            if (objlist.Count > 1)
            {
                foreach (var obj in objlist)
                {
                    if (obj.ObjectType != ObjectType.Rectangle)
                    {
                        return obj;
                    }
                }
            }
            else if (objlist.Count == 1)
            {
                return objlist[0];
            }
            else
            {
                return null;
            }
            return null;

        }

        public GraphicObject FindObjectWithName(string name)
        {
            GraphicObject drawObj = null;
            int i = 0;
            if (this.Count > 0)
            {
                for (i = this.Count - 1; i >= 0; i--)
                {
                    drawObj = (GraphicObject)this[i];
                    if (drawObj.Name == name)
                    {
                        return drawObj;
                        break;
                    }
                }
            }
            return null;
        }

        public GraphicObject FindObjectWithTag(string tag)
        {
            int i = 0;
            if (this.Count > 0)
            {
                for (i = this.Count - 1; i >= 0; i--)
                {
                    if (this[i].Tag == tag)
                    {
                        return this[i];
                        break;
                    }
                }
            }
            return null;

        }

        public int HorizontalResolution
        {
            get
            {
                return m_HorizRes;
            }
            set
            {
                m_HorizRes = value;
            }
        }

        public int VerticalResolution
        {
            get
            {
                return m_VertRes;
            }
            set
            {
                m_VertRes = value;
            }
        }

        public GraphicObjectCollection()
        {
        }

        public GraphicObjectCollection(GraphicObjectCollection value) : base()
        {
            this.AddRange(value);
        }

        public GraphicObjectCollection(GraphicObject[] value) : base()
        {
            this.AddRange(value);
        }

        public void AddRange(GraphicObject[] value)
        {
            int i = 0;
            while (i < value.Length)
            {
                this.Add(value[i]);
                i = (i + 1);
            }
        }
       
    }
}
