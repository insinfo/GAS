using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Text;

namespace GAS
{
    public class DesignerSurface : UserControl
    {
        #region Events
        public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);
        public event SelectionChangedEventHandler SelectionChanged;
        public delegate void StatusUpdateEventHandler(object sender, StatusUpdateEventArgs e);
        public event StatusUpdateEventHandler StatusUpdate;
        #endregion
        public bool ResizingMode { get; set; } = false;
        public bool ResizingMode_KeepAR { get; set; } = true;

        //propriedades privadas
        private const float MinimumGridSize = 10;
        private MyPoint dragOffset = new MyPoint(0, 0);
        private MyPoint dragStart = new MyPoint(0, 0);

        public bool selectionDragging = false;
        private bool selectable = true;
        public bool dragging = false;
        private bool draggingfs = false;
        private bool rotating = false;
        private bool hoverdraw = false;
        private float startingRotation = 0;
        private float originalRotation = 0;
        private Rectangle selectionRect;
        private MyPoint rectp0 = new MyPoint();
        private Rectangle hoverRect;
        private int hoverrotation = 0;
        private Size Size0;

        private bool justselected = false;

        private Rectangle m_SurfaceBounds = new Rectangle(0, 0, 10000, 7000);
        private Rectangle m_SurfaceMargins = new Rectangle(10, 10, 10000, 7000);
        public int m_HorizRes = 300;//DPI horizontal
        public int m_VertRes = 300;//DPI vertical
        private bool m_ShowGrid = true;
        private Color m_NonPrintingAreaColor = Color.Gray;
        private Color m_GridColor = Color.LightBlue;
        private float m_GridSize = 100;
        private int m_GridLineWidth = 1;
        private int m_MarginLineWidth = 1;
        private Color m_MarginColor = Color.Green;
        private GraphicObject m_SelectedObject;
        private float m_Zoom = 0.5F;

        private bool m_snaptogrid = false;
        private bool m_mousehoverselect = false;
        private bool m_quickconnectmode = false;

        public GraphicObjectCollection m_drawingObjects = new GraphicObjectCollection();

        public Dictionary<string, GraphicObject> m_SelectedObjects = new Dictionary<string, GraphicObject>();

        public enum AlignDirection
        {
            Lefts,
            Centers,
            Rights,
            Tops,
            Middles,
            Bottoms,
            EqualizeHorizontal,
            EqualizeVertical
        }

        //propriedades publicas
        public Rectangle SurfaceBounds
        {
            //bounds in 1/100ths of an inch, just like the printer objects use
            get
            {
                return m_SurfaceBounds;
            }
            set
            {
                m_SurfaceBounds = value;
                this.Invalidate();
            }
        }
        public bool SnapToGrid
        {
            get
            {
                return m_snaptogrid;
            }
            set
            {
                m_snaptogrid = value;
            }
        }
        public bool MouseHoverSelect
        {
            get
            {
                return m_mousehoverselect;
            }
            set
            {
                m_mousehoverselect = value;
            }
        }
        public Rectangle SurfaceMargins
        {
            //bounds in 1/100ths of an inch, just like the printer objects use
            get
            {
                return m_SurfaceMargins;
            }
            set
            {
                m_SurfaceMargins = value;
            }
        }
        public float Zoom
        {
            get
            {
                return m_Zoom;
            }
            set
            {
                double xp = 0;
                double yp = 0;
                xp = this.HorizontalScroll.Value / (m_Zoom * this.Width);
                yp = this.VerticalScroll.Value / (m_Zoom * this.Height);
                if (value > 0.05F)
                {
                    if (value > 100)
                    {
                        m_Zoom = 100.0F;
                        this.Invalidate();
                        this.HorizontalScroll.Value = (int)(xp * 100.0F * this.Width);
                        this.VerticalScroll.Value = (int)(yp * 100.0F * this.Height);
                        this.Invalidate();
                    }
                    else
                    {
                        m_Zoom = value;
                        this.Invalidate();
                        if (!(xp * value * this.Width < this.HorizontalScroll.SmallChange) & !(xp * value * this.Width > this.HorizontalScroll.Maximum))
                        {
                            this.HorizontalScroll.Value = Convert.ToInt32(xp * value * this.Width);
                        }
                        if (!(yp * value * this.Height < this.HorizontalScroll.SmallChange) & !(yp * value * this.Height > this.VerticalScroll.Maximum))
                        {
                            this.VerticalScroll.Value = Convert.ToInt32(yp * value * this.Height);
                        }
                        this.Invalidate();
                    }
                }
                else
                {
                    m_Zoom = 0.05F;
                    this.Invalidate();
                    this.HorizontalScroll.Value = (int)(xp * 0.05F * this.Width);
                    this.VerticalScroll.Value = (int)(yp * 0.05F * this.Height);
                    this.Invalidate();
                }
            }
        }
        public bool SelectRectangle
        {
            get
            {
                return selectable;
            }
            set
            {
                selectable = value;
            }
        }
        public virtual GraphicObjectCollection DrawingObjects
        {
            get
            {
                return m_drawingObjects;
            }
        }
        public virtual Color MarginColor
        {
            get
            {
                return m_MarginColor;
            }
            set
            {
                m_MarginColor = value;
            }
        }
        public virtual int MarginLineWidth
        {
            get
            {
                return m_MarginLineWidth;
            }
            set
            {
                if (value > 0)
                {
                    m_MarginLineWidth = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("MarginLineWidth", "MarginLineWidth must be greater than zero");
                }
            }
        }
        public virtual bool ShowGrid
        {
            get
            {
                return m_ShowGrid;
            }
            set
            {
                m_ShowGrid = false;
            }
        }
        public virtual Color GridColor
        {
            get
            {
                return m_GridColor;
            }
            set
            {
                m_GridColor = value;
            }
        }
        public virtual Color NonPrintingAreaColor
        {
            get
            {
                return m_NonPrintingAreaColor;
            }
            set
            {
                m_NonPrintingAreaColor = value;
            }
        }
        public virtual int GridLineWidth
        {
            get
            {
                return m_GridLineWidth;
            }
            set
            {
                if (value > 0)
                {
                    m_GridLineWidth = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("GridLineWidth", "GridLineWidth must be greater than zero");
                }
            }
        }
        public virtual float GridSize
        {
            //in 100ths of an inch
            get
            {
                return m_GridSize;
            }
            set
            {
                if (value > MinimumGridSize)
                {
                    m_GridSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("GridSize", string.Format("Grid Size must be greater than {0}", MinimumGridSize));
                }
            }
        }

        public bool DrawFloatingTables { get; set; } = true;
        public bool DrawPropertyLists { get; set; } = false;

        //utilitarios
        protected float ConvertToHPixels(float value)
        {
            return value * this.m_HorizRes;
        }
        protected float ConvertToVPixels(float value)
        {
            return value * this.m_VertRes;
        }
        //Converte de centésimo de polegada para pixels
        protected Rectangle ConvertToPixels(Rectangle rect)
        {
            //convert from 100ths of an inch to pixels
            Rectangle Bounds = new Rectangle((int)(ConvertToHPixels(rect.X / 100.0F)),
                (int)(ConvertToVPixels(rect.Y / 100.0F)),
                (int)(ConvertToHPixels(rect.Width / 100.0F)),
                (int)(ConvertToVPixels(rect.Height / 100.0F)));
            return Bounds;
        }
        protected Rectangle ZoomRectangle(Rectangle originalRect)
        {
            Rectangle myNewRect = new Rectangle(
                (int)(originalRect.X * this.Zoom),
                (int)(originalRect.Y * this.Zoom),
                (int)(originalRect.Width * this.Zoom),
                (int)(originalRect.Height * this.Zoom));
            return myNewRect;
        }
        protected Rectangle DeZoomRectangle(Rectangle originalRect)
        {
            Rectangle myNewRect = new Rectangle(
                (int)(originalRect.X / this.Zoom),
                (int)(originalRect.Y / this.Zoom),
                (int)(originalRect.Width / this.Zoom),
                (int)(originalRect.Height / this.Zoom));
            return myNewRect;
        }

        //desenha o grid
        public virtual void DrawGrid(Graphics g)
        {
            Rectangle bounds = new Rectangle();
            int horizGridSize = (int)(ConvertToHPixels(GridSize / 100) * this.Zoom);
            int vertGridSize = (int)(ConvertToVPixels(GridSize / 100) * this.Zoom);
            bounds = ConvertToPixels(SurfaceBounds);
            bounds = ZoomRectangle(bounds);
            if (this.AutoScrollMinSize.Height != bounds.Height && this.AutoScrollMinSize.Width != bounds.Width)
            {
                this.AutoScrollMinSize = new Size(bounds.Width, bounds.Height);
            }

            g.Clear(m_NonPrintingAreaColor);
            g.FillRectangle(new SolidBrush(this.BackColor), bounds);

            Pen gridPen = new Pen(GridColor, m_GridLineWidth);
            //gridPen.DashStyle = DashStyle.Dot
            int i = 0;

            for (i = vertGridSize; i <= bounds.Height; i += vertGridSize)
            {
                g.DrawLine(gridPen, 0, i, bounds.Width, i);
            }

            for (i = horizGridSize; i <= bounds.Width; i += horizGridSize)
            {
                g.DrawLine(gridPen, i, 0, i, bounds.Height);
            }
        }
        //desenha as margins
        protected virtual void DrawMargins(Graphics g)
        {
            Rectangle margins = ZoomRectangle(ConvertToPixels(this.m_SurfaceMargins));
            Pen marginPen = new Pen(m_MarginColor);
            marginPen.DashStyle = DashStyle.Dash;
            marginPen.Width = m_MarginLineWidth;
            g.DrawRectangle(marginPen, margins);
        }

        public bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!dragging)
            {
                g.InterpolationMode = InterpolationMode.Bilinear;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.TextRenderingHint = TextRenderingHint.SystemDefault;
                g.SmoothingMode = SmoothingMode.AntiAlias;
            }
            else
            {

                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                g.SmoothingMode = SmoothingMode.None;
            }

            //get the dpi settings of the graphics context,
            //for example; 96dpi on screen, 600dpi for the printer
            //used to adjust grid and margin sizing.

            this.m_HorizRes = (int)g.DpiX;
            this.m_VertRes = (int)g.DpiY;

            //handle the possibility that the viewport is scrolled,
            //adjust my origin coordintates to compensate
            MyPoint pt = new MyPoint(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            g.TranslateTransform(Convert.ToSingle(pt.X), Convert.ToSingle(pt.Y));

            DrawGrid(g);

            if (hoverdraw)
            {
                Matrix myOriginalMatrix = new Matrix();
                GraphicsContainer gCon1 = g.BeginContainer();

                if (!IsRunningOnMono())
                {
                    myOriginalMatrix = g.Transform;
                }

                if (hoverrotation != 0)
                {
                    myOriginalMatrix.RotateAt(hoverrotation, new PointF(
                        (hoverRect.X + hoverRect.Width / 2.0F) * Zoom,
                        (hoverRect.Y + hoverRect.Height / 2.0F) * Zoom),
                        MatrixOrder.Append);
                    g.Transform = myOriginalMatrix;
                }

                g.ScaleTransform(Zoom, Zoom);

                g.PageUnit = GraphicsUnit.Pixel;

                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                color1 = Color.FromArgb(50, 170, 215, 230);
                color2 = Color.FromArgb(50, 2, 140, 140);
                color3 = Color.FromArgb(50, 2, 140, 140);

                LinearGradientBrush gbrush = new LinearGradientBrush(hoverRect, color1, color2, LinearGradientMode.Vertical);
                DrawRoundRect(g, new Pen(color3, 1), hoverRect.X, hoverRect.Y, hoverRect.Width, hoverRect.Height, 15, gbrush);

                if (!IsRunningOnMono())
                {
                    g.Transform = myOriginalMatrix;
                }
                g.EndContainer(gCon1);
            }

            //draw the actual objects onto the page, on top of the grid
            if (this.SelectedObject == null)
            {
                this.SelectedObjects.Clear();
            }

            foreach (GraphicObject gr in this.SelectedObjects.Values)
            {
                gr.Selected = true;
                this.DrawingObjects.DrawSelectedObject(g, gr, this.Zoom);
            }

            foreach (GraphicObject gr in this.DrawingObjects)
            {
                if (!this.SelectedObjects.ContainsKey(gr.Name))
                {
                    gr.Selected = false;
                }
            }

            this.DrawingObjects.HorizontalResolution = (int)g.DpiX;
            this.DrawingObjects.VerticalResolution = (int)g.DpiY;
            this.DrawingObjects.DrawObjects(g, this.Zoom, dragging);

            /*
            if (DrawPropertyLists)
            {
                foreach (GraphicObject gr in this.DrawingObjects)
                {
                    if (gr.Calculated)
                    {
                        DrawPropertyListBlock(g, gr);
                    }
                }
            }*/

            //Draw dashed line margin indicators, over top of objects
            DrawMargins(g);

            //draw selection rectangle (click and drag to select interface)
            //on top of everything else, but transparent
            if (selectionDragging)
            {
                DrawSelectionRectangle(g, selectionRect);
            }
            //Catch ex As System.Exception
            //Debug.WriteLine(ex.ToString)
            //Throw New System.ApplicationException("Error Drawing Graphics Surface", ex)
            // End Try

            if (ResizingMode)
            {
                foreach (var obj in DrawingObjects)
                {
                    if (obj.ObjectType != ObjectType.Nenhum)
                    {
                        DrawResizingAnchors(g, this.Zoom, obj);
                    }
                }
            }

        }

        public void DrawResizingAnchors(Graphics g, float Scale, GraphicObject gobj)
        {
            GraphicsContainer gCon = null;
            Matrix myOriginalMatrix = g.Transform;

            gCon = g.BeginContainer();

            g.PageUnit = GraphicsUnit.Pixel;
            g.ScaleTransform(Scale, Scale);

            g.DrawRectangle(new Pen(Brushes.SteelBlue, 2), gobj.GetBoundsRectangle());

            g.DrawRectangle(new Pen(Brushes.SteelBlue, 2), gobj.X - 2, gobj.Y - 2, 4, 4);
            g.DrawRectangle(new Pen(Brushes.SteelBlue, 2), gobj.X + gobj.Width - 2, gobj.Y - 2, 4, 4);
            g.DrawRectangle(new Pen(Brushes.SteelBlue, 2), gobj.X - 2, gobj.Y + gobj.Height - 2, 4, 4);
            g.DrawRectangle(new Pen(Brushes.SteelBlue, 2), gobj.X + gobj.Width - 2, gobj.Y + gobj.Height - 2, 4, 4);

            g.EndContainer(gCon);
            g.Transform = myOriginalMatrix;

        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    return true;
                case Keys.Down:
                    return true;
                case Keys.Left:
                    return true;
                case Keys.Right:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        public void AlignSelectedObjects(AlignDirection direction)
        {
            if (this.SelectedObjects.Count > 1)
            {
                int refpos = 0;

                switch (direction)
                {
                    case AlignDirection.Lefts:
                        {
                            refpos = 10000000;
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                if (obj.X < refpos)
                                {
                                    refpos = obj.X;
                                }
                            }
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                obj.X = refpos;
                            }
                            break;
                        }
                    case AlignDirection.Centers:
                        {
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                refpos += (int)(obj.X + obj.Width / 2.0F);
                            }
                            refpos /= this.SelectedObjects.Count;
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                obj.X = (int)(refpos - obj.Width / 2.0F);
                            }
                            break;
                        }
                    case AlignDirection.Rights:
                        {
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                refpos += obj.X + obj.Width;
                            }
                            refpos /= this.SelectedObjects.Count;
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                obj.X = refpos - obj.Width;
                            }
                            break;
                        }
                    case AlignDirection.Tops:
                        {
                            refpos = 10000000;
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                if (obj.Y < refpos)
                                {
                                    refpos = obj.Y;
                                }
                            }
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                obj.Y = refpos;
                            }
                            break;
                        }
                    case AlignDirection.Middles:
                        {
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                refpos += (int)(obj.Y + obj.Height / 2.0F);
                            }
                            refpos /= this.SelectedObjects.Count;
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                obj.Y = (int)(refpos - obj.Height / 2.0F);
                            }
                            break;
                        }
                    case AlignDirection.Bottoms:
                        {
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                refpos += obj.Y + obj.Height;
                            }
                            refpos /= this.SelectedObjects.Count;
                            foreach (GraphicObject obj in this.SelectedObjects.Values)
                            {
                                obj.Y = refpos - obj.Height;
                            }
                            break;
                        }
                    case AlignDirection.EqualizeHorizontal:
                        {
                            List<GraphicObject> orderedlist = this.SelectedObjects.Values.OrderBy((o) => o.X).ToList();
                            int avgdist = 0;
                            int i = 0;
                            for (i = 1; i < orderedlist.Count; i++)
                            {
                                avgdist += (orderedlist[i].X) - (orderedlist[i - 1].X + orderedlist[i - 1].Width);
                            }
                            avgdist /= (orderedlist.Count - 1);
                            for (i = 1; i < orderedlist.Count; i++)
                            {
                                orderedlist[i].X = orderedlist[i - 1].X + orderedlist[i - 1].Width + avgdist;
                            }
                            break;
                        }
                    case AlignDirection.EqualizeVertical:
                        {
                            List<GraphicObject> orderedlist = this.SelectedObjects.Values.OrderBy((o) => o.Y).ToList();
                            int avgdist = 0;
                            int i = 0;
                            for (i = 1; i < orderedlist.Count; i++)
                            {
                                avgdist += (orderedlist[i].Y) - (orderedlist[i - 1].Y + orderedlist[i - 1].Height);
                            }
                            avgdist /= (orderedlist.Count - 1);
                            for (i = 1; i < orderedlist.Count; i++)
                            {
                                orderedlist[i].Y = orderedlist[i - 1].Y + orderedlist[i - 1].Height + avgdist;
                            }
                            break;
                        }
                }
                this.Invalidate();
                Application.DoEvents();
                this.Invalidate();
                Application.DoEvents();
            }
        }
        //desenha um retangulo que desmotra que o objeto esta selecionado
        private void DrawSelectionRectangle(Graphics g, Rectangle selectionRect)
        {
            SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(25, Color.LightSalmon));
            Rectangle normalizedRectangle = new Rectangle();

            //make sure the rectangle's upper left point is
            //up and to the left relative to the other points of the rectangle by
            //ensuring that it has a positive width and height.
            normalizedRectangle.Size = selectionRect.Size;
            if (selectionRect.Width < 0)
            {
                normalizedRectangle.X = selectionRect.X - normalizedRectangle.Width;
            }
            else
            {
                normalizedRectangle.X = selectionRect.X;
            }
            if (selectionRect.Height < 0)
            {
                normalizedRectangle.Y = selectionRect.Y - normalizedRectangle.Height;
            }
            else
            {
                normalizedRectangle.Y = selectionRect.Y;
            }
            g.FillRectangle(selectionBrush, normalizedRectangle);
        }
        //util
        public MyPoint gscTogoc(MyPoint gsPT)
        {
            MyPoint myNewPoint = new MyPoint();
            myNewPoint.X = Convert.ToInt32((gsPT.X - this.AutoScrollPosition.X) / this.Zoom);
            myNewPoint.Y = Convert.ToInt32((gsPT.Y - this.AutoScrollPosition.Y) / this.Zoom);
            return myNewPoint;
        }
        public MyPoint gscTogoc(int X, int Y)
        {
            MyPoint myNewPoint = new MyPoint();
            myNewPoint.X = Convert.ToInt32((X - this.AutoScrollPosition.X) / this.Zoom);
            myNewPoint.Y = Convert.ToInt32((Y - this.AutoScrollPosition.Y) / this.Zoom);
            return myNewPoint;
        }
        public MyPoint gocTogsc(MyPoint goPT)
        {
            //note that there is no need for a New for my Point
            //before I can assign values to X and Y
            //as it is a structure not an object
            MyPoint myNewPoint = new MyPoint();
            myNewPoint.X = Convert.ToInt32((goPT.X) * this.Zoom);
            myNewPoint.Y = Convert.ToInt32((goPT.Y) * this.Zoom);

            return myNewPoint;
        }
        public MyPoint gocTogsc(int X, int Y)
        {
            MyPoint myNewPoint = new MyPoint();
            myNewPoint.X = Convert.ToInt32(X * this.Zoom);
            myNewPoint.Y = Convert.ToInt32(Y * this.Zoom);
            return myNewPoint;
        }

        //evento do movimento do mouse 
        private void GraphicsSurface_MouseClick(object sender, MouseEventArgs e)
        {
            this.Invalidate();
            MyPoint mousePT = gscTogoc(e.X, e.Y);
            dragStart = new MyPoint(e.X, e.Y);
            this.SelectedObject = this.DrawingObjects.FindObjectAtPoint(mousePT);

            if (SelectedObject != null)
            {
                Size0 = SelectedObject.GetSize();
            }

            if (!ResizingMode)
            {

                if (this.SelectedObject == null)
                {
                    this.SelectedObjects.Clear();
                    justselected = false;
                    if (My.Computer.Keyboard.ShiftKeyDown)
                    {
                        draggingfs = true;
                    }
                }
                else
                {
                    if (My.Computer.Keyboard.CtrlKeyDown)
                    {
                        if (!this.SelectedObjects.ContainsKey(this.SelectedObject.Name))
                        {
                            this.SelectedObjects.Add(this.SelectedObject.Name, this.SelectedObject);
                        }
                        else
                        {
                            this.SelectedObjects.Remove(this.SelectedObject.Name);
                        }
                        justselected = true;
                    }
                    else
                    {
                        if (!justselected)
                        {
                            this.SelectedObjects.Clear();
                        }
                        if (!this.SelectedObjects.ContainsKey(this.SelectedObject.Name))
                        {
                            this.SelectedObjects.Add(this.SelectedObject.Name, this.SelectedObject);
                        }
                        justselected = false;
                    }
                }

                if (m_SelectedObject != null)
                {
                    if ((e.Button & MouseButtons.Right) != 0)
                    {
                        //rotating = True
                        dragging = true;
                        startingRotation = AngleToPoint(m_SelectedObject.GetPosition(), mousePT);
                        originalRotation = m_SelectedObject.Rotation;
                    }
                    else
                    {
                        dragging = true;
                        dragOffset.X = m_SelectedObject.X - mousePT.X;
                        dragOffset.Y = m_SelectedObject.Y - mousePT.Y;
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Left & this.SelectRectangle & !My.Computer.Keyboard.ShiftKeyDown)
                    {
                        selectionDragging = true;
                        rectp0.X = mousePT.X * this.Zoom;
                        rectp0.Y = mousePT.Y * this.Zoom;
                        selectionRect.Height = 0;
                        selectionRect.Width = 0;
                    }
                }
            }
            else
            {
                Cursor = Cursors.SizeNWSE;
            }
        }

        public void ZoomAll()
        {

            int minx = 10000;
            int miny = 10000;
            int maxx = 0;
            int maxy = 0;

            foreach (GraphicObject gobj in this.DrawingObjects)
            {
                if (gobj.ObjectType != ObjectType.Nenhum)
                {
                    if (gobj.X <= minx)
                    {
                        minx = gobj.X;
                    }
                    if (gobj.X + gobj.Width >= maxx)
                    {
                        maxx = gobj.X + gobj.Width + 60;
                    }
                    if (gobj.Y <= miny)
                    {
                        miny = gobj.Y;
                    }
                    if (gobj.Y + gobj.Height >= maxy)
                    {
                        maxy = gobj.Y + gobj.Height + 60;
                    }
                }
            }

            int windowheight = 0;
            int windowwidth = 0;
            int fsx = 0;
            int fsy = 0;
            int fsheight = 0;
            int fswidth = 0;

            windowheight = this.Height;
            windowwidth = this.Width;

            fsx = this.HorizontalScroll.Value;
            fsy = this.VerticalScroll.Value;
            fswidth = fsx + windowwidth;
            fsheight = fsy + windowheight;

            int newx = 0;
            int newy = 0;

            newx = minx - 30;
            newy = miny - 30;

            if (newx < 0)
            {
                newx = 0;
            }
            if (newy < 0)
            {
                newy = 0;
            }

            double zoomx = 1.0D;
            double zoomy = 1.0D;

            zoomx = windowwidth / (double)(maxx - newx);
            zoomy = windowheight / (double)(maxy - newy);

            if (zoomx > zoomy)
            {
                this.Zoom = Convert.ToSingle(zoomy);
            }
            else
            {
                this.Zoom = Convert.ToSingle(zoomx);
            }

            if (newx * this.Zoom < this.HorizontalScroll.Maximum)
            {
                this.HorizontalScroll.Value = Convert.ToInt32(newx * this.Zoom);
            }
            if (newy * this.Zoom < this.VerticalScroll.Maximum)
            {
                this.VerticalScroll.Value = Convert.ToInt32(newy * this.Zoom);
            }

            this.Invalidate();

        }

        public void Center()
        {

            int minx = 10000;
            int miny = 10000;
            int maxx = 0;
            int maxy = 0;
            int middlex = 0;
            int middley = 0;

            foreach (GraphicObject gobj in this.DrawingObjects)
            {
                if (gobj.ObjectType != ObjectType.Nenhum)
                {
                    if (gobj.X <= minx)
                    {
                        minx = gobj.X;
                    }
                    if (gobj.X + gobj.Width >= maxx)
                    {
                        maxx = gobj.X + gobj.Width + 60;
                    }
                    if (gobj.Y <= miny)
                    {
                        miny = gobj.Y;
                    }
                    if (gobj.Y + gobj.Height >= maxy)
                    {
                        maxy = gobj.Y + gobj.Height + 60;
                    }
                }
            }

            middlex = Convert.ToInt32((minx + maxx) / 2.0);
            middley = Convert.ToInt32((miny + maxy) / 2.0);

            this.HorizontalScroll.Value = (int)(middlex * this.Zoom - this.Width / 2.0);
            this.VerticalScroll.Value = (int)(middley * this.Zoom - this.Height / 2.0);

            this.Invalidate();
            this.Invalidate();
        }

        private void GraphicsSurface_MouseMove(object sender, MouseEventArgs e)
        {
            int dx = (int)-(e.X - dragStart.X);
            int dy = (int)-(e.Y - dragStart.Y);

            if (!ResizingMode)
            {

                if (draggingfs)
                {
                    if (!My.Computer.Keyboard.ShiftKeyDown)
                    {
                        draggingfs = false;
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Hand;
                    }


                    if (this.HorizontalScroll.Value + dx > this.HorizontalScroll.Minimum)
                    {
                        this.HorizontalScroll.Value += dx;
                    }
                    else
                    {
                        this.HorizontalScroll.Value += this.HorizontalScroll.Minimum;
                    }
                    if (this.VerticalScroll.Value + dy > this.VerticalScroll.Minimum)
                    {
                        this.VerticalScroll.Value += dy;
                    }
                    else
                    {
                        this.VerticalScroll.Value += this.VerticalScroll.Minimum;
                    }
                    dragStart = new MyPoint(e.X, e.Y);
                }
                else
                {
                    #region não implementado
                    //Cursor.Current = Cursors.Default
                    /* if (!this.QuickConnect | !My.Computer.Keyboard.CtrlKeyDown)
                     {
                         Point dragPoint = gscTogoc(e.X, e.Y);

                         GraphicObject obj = this.DrawingObjects.FindObjectAtPoint(dragPoint);

                         if (obj != null)
                         {
                             if (obj.ObjectType != ObjectType.GO_FloatingTable & obj.ObjectType != ObjectType.GO_Text & obj.ObjectType != ObjectType.GO_Image & obj.ObjectType != ObjectType.GO_Table & obj.ObjectType != ObjectType.Nenhum & obj.ObjectType != ObjectType.GO_SpreadsheetTable & obj.ObjectType != ObjectType.GO_MasterTable & obj.ObjectType != ObjectType.GO_Rectangle)
                             {
                                 hoverrotation = obj.Rotation;
                                 this.hoverRect.X = obj.X - 10;
                                 this.hoverRect.Y = obj.Y - 10;
                                 switch (obj.ObjectType)
                                 {
                                     case ObjectType.GO_Animation:
                                     case ObjectType.GO_Image:
                                     case ObjectType.GO_Table:
                                     case ObjectType.GO_FloatingTable:
                                     case ObjectType.GO_Text:
                                     case ObjectType.GO_MasterTable:
                                     case ObjectType.GO_SpreadsheetTable:
                                     case ObjectType.GO_Rectangle:
                                         this.hoverRect.Height = obj.Height + 20;
                                         this.hoverRect.Width = obj.Width + 20;
                                         break;
                                     default:
                                         this.hoverRect.Height = obj.Height + 30;
                                         this.hoverRect.Width = obj.Width + 20;
                                         Graphics g = Graphics.FromHwnd(this.Handle);
                                         SizeF strdist = g.MeasureString(obj.Tag, new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel, 0, false), new PointF(0, 0), new StringFormat(StringFormatFlags.NoClip, 0));
                                         if (strdist.Width > obj.Width)
                                         {
                                             this.hoverRect.X = obj.X + (obj.Width - strdist.Width) / 2.0 - 10;
                                             this.hoverRect.Width = strdist.Width + 20;
                                         }
                                         break;
                                 }
                                 hoverdraw = true;
                             }
                         }
                         else
                         {
                             hoverdraw = false;
                         }

                         if (m_SelectedObject != null)
                         {

                             if (!m_SelectedObject.IsConnector & SelectRectangle)
                             {

                                 if (dragging)
                                 {

                                     Cursor.Current = Cursors.Hand;

                                     dragPoint.Offset(dragOffset.X, dragOffset.Y);
                                     //m_SelectedObject.SetPosition(dragPoint)

                                     foreach (GraphicObject gr in this.SelectedObjects.Values)
                                     {
                                         Point p = new Point(gr.X, gr.Y);
                                         p.Offset((e.X - dragStart.X) / this.Zoom, (e.Y - dragStart.Y) / this.Zoom);
                                         gr.SetPosition(p);
                                     }

                                     dragStart = new Point(e.X, e.Y);

                                     if (StatusUpdate != null)
                                         StatusUpdate(this, new StatusUpdateEventArgs(StatusUpdateType.ObjectMoved, this.SelectedObject, string.Format("Object Moved to {0}, {1}", dragPoint.X, dragPoint.Y), dragPoint, 0));

                                 }
                                 else if (rotating)
                                 {
                                     Cursor.Current = Cursors.SizeAll;
                                     float currentRotation = AngleToPoint(m_SelectedObject.GetPosition, dragPoint);

                                     currentRotation = Convert.ToInt32((currentRotation - startingRotation + originalRotation) % 360);

                                     m_SelectedObject.Rotation = currentRotation;

                                     if (StatusUpdate != null)
                                         StatusUpdate(this, new StatusUpdateEventArgs(StatusUpdateType.ObjectRotated, this.SelectedObject, string.Format("Object Rotated to {0} degrees", currentRotation), null, currentRotation));


                                 }
                                 else
                                 {
                                     Cursor.Current = Cursors.Default;
                                 }
                             }
                             else
                             {
                                 Cursor.Current = Cursors.Default;
                             }
                         }
                         else
                         {
                             if ((selectionDragging & SelectRectangle) != 0)
                             {
                                 int x0 = 0;
                                 int y0 = 0;
                                 int x1 = 0;
                                 int y1 = 0;

                                 x0 = rectp0.X;
                                 y0 = rectp0.Y;
                                 x1 = (int)(dragPoint.X * this.Zoom);
                                 y1 = (int)(dragPoint.Y * this.Zoom);

                                 if (x1 > x0)
                                 {
                                     selectionRect.X = x0;
                                 }
                                 else
                                 {
                                     selectionRect.X = x1;
                                 }

                                 if (y1 > y0)
                                 {
                                     selectionRect.Y = y0;
                                 }
                                 else
                                 {
                                     selectionRect.Y = y1;
                                 }

                                 selectionRect.Width = Math.Abs(x1 - x0);
                                 selectionRect.Height = Math.Abs(y1 - y0);

                                 Cursor.Current = Cursors.Default;
                             }
                             else
                             {
                                 Cursor.Current = Cursors.Default;
                             }
                         }
                     }
                     else
                     {
                         Cursor.Current = Cursors.Default;
                     }
                     */
                    #endregion
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                if (this.SelectedObject != null && Cursor == Cursors.SizeNWSE)
                {
                    int neww = 0;
                    int newh = 0;

                    if (ResizingMode_KeepAR)
                    {
                        neww = Convert.ToInt32(Size0.Width - Convert.ToDouble(Size0.Width / (double)Size0.Height) * Math.Min(dx, dy));
                        newh = Size0.Height - Math.Min(dx, dy);
                    }
                    else
                    {
                        neww = Size0.Width - dx;
                        newh = Size0.Height - dy;
                    }

                    if (neww > 10 && newh > 10)
                    {
                        this.SelectedObject.Width = neww;
                        this.SelectedObject.Height = newh;
                    }
                    else
                    {
                        this.SelectedObject.Width = 10;
                        this.SelectedObject.Height = 10;
                    }
                }
                this.Invalidate();
            }
        }

        private void GraphicsSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ResizingMode)
            {
                draggingfs = false;

                if (dragging & SnapToGrid)
                {
                    int horizGridSize = (int)(ConvertToHPixels(GridSize / 100) * this.Zoom);
                    int vertGridSize = (int)(ConvertToVPixels(GridSize / 100) * this.Zoom);
                    Rectangle bounds = ConvertToPixels(SurfaceBounds);
                    bounds = ZoomRectangle(bounds);
                    MyPoint oc = new MyPoint();
                    int nlh = 0;
                    int nlv = 0;
                    int snapx = 0;
                    int snapy = 0;
                    nlh = Convert.ToInt32(bounds.Width / (double)horizGridSize);
                    nlv = Convert.ToInt32(bounds.Height / (double)vertGridSize);
                    foreach (GraphicObject go in this.SelectedObjects.Values)
                    {
                        oc = new MyPoint(go.X + go.Width / 2.0, go.Y + go.Height / 2.0);
                        snapx = Convert.ToInt32(Math.Round(oc.X / (double)horizGridSize) * horizGridSize - go.Width / 2.0);
                        snapy = Convert.ToInt32(Math.Round(oc.Y / (double)vertGridSize) * vertGridSize - go.Height / 2.0);
                        go.SetPosition(new MyPoint(snapx, snapy));
                    }
                }
                dragging = false;
                rotating = false;
                if (selectionDragging)
                {
                    Rectangle zoomedSelection = DeZoomRectangle(selectionRect);
                    //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#:
                    //				GraphicObject graphicObj = null;
                    foreach (GraphicObject graphicObj in this.DrawingObjects)
                    {
                        if (graphicObj.HitTest(zoomedSelection))
                        {
                            this.SelectedObject = graphicObj;
                            break;
                        }
                    }
                    foreach (GraphicObject graphicObj in this.DrawingObjects)
                    {
                        if (graphicObj.HitTest(zoomedSelection))
                        {
                            if (!this.SelectedObjects.ContainsKey(graphicObj.Name))
                            {
                                this.SelectedObjects.Add(graphicObj.Name, graphicObj);
                            }
                        }
                    }
                    selectionDragging = false;
                    justselected = true;
                }

            }
            else
            {
                Cursor = Cursors.Default;
                //era pra ser igual a nulo
                Size0 = new Size();
            }
            this.Invalidate();
        }

        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta != 0) //has wheel been moved?
            {

                //The .NET docs suggest that e.Delta returns the actual number of notches
                //the mouse wheel has been rotated, but in actuality each roll of the mouse wheel
                //returns a value of +/- 120 (depending on the direction of rotation).
                //120 is actually a system constant, and therefore there is a possibility that it could
                //change to allow for higher-resolution mouse wheels.

                //check out <http://msdn.microsoft.com/library/en-us/winui/winui/windowsuserinterface/userinput/mouseinput/mouseinputreference/mouseinputmessages/wm_mousewheel.asp>
                //for more information

                if (My.Computer.Keyboard.CtrlKeyDown)
                {
                    int dx = 0;
                    int dy = 0;

                    System.Drawing.Point Cursorpos = new System.Drawing.Point();
                    System.Drawing.Point Pos1 = new System.Drawing.Point();
                    System.Drawing.Point Pos2 = new System.Drawing.Point();
                    Cursorpos = Cursor.Position;
                    Pos1 = gscTogoc(Cursorpos.X, Cursorpos.Y).ToSDPoint();

                    //do zoom
                    if (e.Delta > 0)
                    {
                        this.Zoom += 0.05F;
                    }
                    else
                    {
                        this.Zoom -= 0.05F;
                    }
                    Pos2 = gscTogoc(Cursorpos.X, Cursorpos.Y).ToSDPoint();

                    dx = Pos1.X - Pos2.X;
                    dy = Pos1.Y - Pos2.Y;

                    try
                    {
                        //adjust viewpoint to keep same position
                        this.HorizontalScroll.Value += dx;
                        this.VerticalScroll.Value += dy;
                    }
                    catch (Exception ex)
                    {

                    }

                    if (StatusUpdate != null)
                        StatusUpdate(this, new StatusUpdateEventArgs(StatusUpdateType.SurfaceZoomChanged, this.SelectedObject, string.Format("Zoom set to {0}", this.Zoom * 100), null, this.Zoom));

                }
                else
                {

                    int detents = Convert.ToInt32(e.Delta / 120.0);

                    if (detents > 0)
                    {
                        if (this.VerticalScroll.Value > 4 * My.Computer.Mouse.WheelScrollLines)
                        {
                            if (4 * My.Computer.Mouse.WheelScrollLines > this.VerticalScroll.SmallChange)
                            {
                                this.VerticalScroll.Value -= 4 * My.Computer.Mouse.WheelScrollLines;
                            }
                        }
                        else
                        {
                            this.VerticalScroll.Value = 0;
                        }
                    }
                    else if (detents < 0)
                    {
                        if (this.VerticalScroll.Value > 4 * My.Computer.Mouse.WheelScrollLines)
                        {
                            if (4 * My.Computer.Mouse.WheelScrollLines > this.VerticalScroll.SmallChange)
                            {
                                this.VerticalScroll.Value += 4 * My.Computer.Mouse.WheelScrollLines;
                            }
                        }
                        else
                        {
                            this.VerticalScroll.Value = 0;
                        }
                    }
                }
                this.Invalidate();
            }
        }

        //TODO: Rewrite to handle multiple selected objects
        public GraphicObject SelectedObject
        {
            get
            {
                return m_SelectedObject;
            }
            set
            {
                if (!(value == m_SelectedObject))
                {
                    if (m_drawingObjects.Contains(value) || value == null)
                    {
                        m_SelectedObject = value;
                        if (SelectionChanged != null)
                            SelectionChanged(this, new SelectionChangedEventArgs(value));
                        if (value == null)
                        {
                            if (StatusUpdate != null)
                                StatusUpdate(this, new StatusUpdateEventArgs(StatusUpdateType.SelectionChanged, value, "No Object Selected", null, 0));
                        }
                        else
                        {
                            if (StatusUpdate != null)
                                StatusUpdate(this, new StatusUpdateEventArgs(StatusUpdateType.SelectionChanged, value, "Selected Object Changed", value.GetPosition(), 0));
                        }
                        this.Invalidate();
                    }
                }
            }
        }

        public Dictionary<string, GraphicObject> SelectedObjects
        {
            get
            {
                return m_SelectedObjects;
            }
        }

        public void DeleteSelectedObject(GraphicObject gobj)
        {
            GraphicObject objectToDelete = gobj;
            if (objectToDelete != null)
            {
                if (this.DrawingObjects.Contains(objectToDelete))
                {
                    this.DrawingObjects.Remove(objectToDelete);
                    this.SelectedObject = null;
                    this.Invalidate();
                }
            }
        }

        public void DeleteAllObjects()
        {
            this.DrawingObjects.Clear();
            this.Invalidate();
        }

        private float AngleToPoint(MyPoint Origin, MyPoint Target)
        {
            //a cool little utility function, 
            //given two points finds the angle between them....
            //forced me to recall my highschool math, 
            //but the task is made easier by a special overload to
            //Atan that takes X,Y co-ordinates.
            float Angle = 0;
            Target.X = Target.X - Origin.X;
            Target.Y = Target.Y - Origin.Y;
            Angle = (float)(Math.Atan2(Target.Y, Target.X) / (Math.PI / 180));
            return Angle;
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

        #region não implementado
        /*private void DrawPropertyListBlock(Graphics canvas, GraphicObject gobj)
        {

            if (gobj.Owner == null)
            {
                return;
            }

            GraphicsContainer gCon = canvas.BeginContainer();
            canvas.ScaleTransform(Zoom, Zoom);

            double X = 0;
            double Y = 0;
            double Padding = 0;
            double Height = 0;
            double Width = 0;

            Padding = gobj.Owner.GetFlowsheet.FlowsheetOptions.DisplayCornerPropertyListPadding;

            X = gobj.X + gobj.Width + 15;
            Y = gobj.Y + gobj.Height + 20;

            SolidBrush tpaint = new SolidBrush(Color.DimGray);

            SolidBrush bgpaint = new SolidBrush(Color.FromArgb(200, 255, 255, 255));

            Font tfont = new Font(gobj.Owner.GetFlowsheet.FlowsheetOptions.DisplayCornerPropertyListFontName, gobj.Owner.GetFlowsheet.FlowsheetOptions.DisplayCornerPropertyListFontSize, GraphicsUnit.Pixel);

            if (gobj.Owner != null)
            {

                if (gobj.Owner.GetFlowsheet != null)
                {

                    int count = 0;

                    var fs = gobj.Owner.GetFlowsheet;
                    List<string> props = new List<string>(fs.FlowsheetOptions.VisibleProperties(gobj.Owner.GetType().Name));
                    props.AddRange(((IDictionary<string, object>)gobj.Owner.ExtraProperties).Keys.ToArray);

                    if (gobj.Owner.GraphicObject.ObjectType == DWSIM.Interfaces.Enums.GraphicObjects.ObjectType.CapeOpenUO)
                    {
                        props = gobj.Owner.GetProperties(Interfaces.Enums.PropertyType.ALL).ToList;
                    }

                    List<string> propstoremove = new List<string>();

                    if (gobj.Owner.GraphicObject.ObjectType == DWSIM.Interfaces.Enums.GraphicObjects.ObjectType.MaterialStream)
                    {
                        foreach (var p in props)
                        {
                            if (gobj.Owner.GetPropertyValue(p).Equals(double.MinValue))
                            {
                                propstoremove.Add(p);
                            }
                        }
                        for (int i = 0; i < propstoremove.Count; i++)
                        {
                            props.Remove(propstoremove[i]);
                        }
                    }

                    count = props.Count;

                    var fsize = canvas.MeasureString("MEASURE", tfont);

                    Height = (count - 1) * (fsize.Height + Padding) + Padding;

                    string propstring = null;
                    string propval = null;
                    string propunit = null;
                    string text = null;
                    object pval0 = null;

                    List<string> texts = new List<string>();

                    int n = 1;
                    foreach (var prop in props)
                    {
                        propstring = gobj.Owner.GetFlowsheet.GetTranslatedString(prop);
                        pval0 = gobj.Owner.GetPropertyValue(prop, gobj.Owner.GetFlowsheet.FlowsheetOptions.SelectedUnitSystem);
                        if (pval0 == null)
                        {
                            break;
                        }
                        if (pval0 is double)
                        {
                            propval = Convert.ToDouble(pval0).ToString(gobj.Owner.GetFlowsheet.FlowsheetOptions.NumberFormat);
                        }
                        else
                        {
                            propval = pval0.ToString();
                        }
                        propunit = gobj.Owner.GetPropertyUnit(prop, gobj.Owner.GetFlowsheet.FlowsheetOptions.SelectedUnitSystem);
                        text = propstring + ": " + propval + " " + propunit;
                        if (canvas.MeasureString(text, tfont).Width > (float)Width)
                        {
                            Width = canvas.MeasureString(text, tfont).Width;
                        }
                        texts.Add(text);
                        n += 1;
                    }

                    canvas.FillRectangle(bgpaint, Convert.ToInt32(X), Convert.ToInt32(Y + Padding), Convert.ToInt32(Width), Convert.ToInt32(Height + Padding));

                    n = 0;
                    foreach (string textWithinLoop in texts)
                    {
                        text = textWithinLoop;
                        canvas.DrawString(textWithinLoop, tfont, tpaint, X, Y + n * (fsize.Height + Padding));
                        n += 1;
                    }
                    props.Clear();
                    props = null;
                }
            }
            canvas.EndContainer(gCon);
        }*/
        #endregion
        //parte de conversão de codigo a revisar
        private bool EventsSubscribed = false;
        private void SubscribeToEvents()
        {
            if (EventsSubscribed)
                return;
            else
                EventsSubscribed = true;

            base.MouseDown += GraphicsSurface_MouseClick;
        }
        //construtor
        public DesignerSurface()
        {
            SubscribeToEvents();
        }



    }


}


