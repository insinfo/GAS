using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GAS
{
    [System.Serializable()]
    public abstract class GraphicObject 
    {
        internal float m_Rotation;

        public bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        public Rectangle GetBoundsRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        /*public static IGraphicObject ReturnInstance(string typename)
        {
            Type t = Type.GetType(typename, false);
            if (t != null)
            {
                return (IGraphicObject)Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }*/

        /*public virtual bool Calculated
        {
            get
            {
                switch (this.Status)
                {
                    case Status.Calculated:
                        return true;
                    case Status.Calculating:
                        return false;
                    case Status.ErrorCalculating:
                        return false;
                    case Status.Idle:
                        return true;
                    case Status.Inactive:
                        return false;
                    case Status.NotCalculated:
                        return false;
                    default:
                        return false;
                }
            }
            set
            {
                if (value == false)
                {
                    Status = Status.ErrorCalculating;
                }
                else
                {
                    Status = Status.Calculated;
                }
            }
        }*/

        public virtual bool HitTest(System.Drawing.Point pt)
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

        public virtual bool HitTest(Rectangle rect)
        {
            //is this object contained within the supplied rectangle
            GraphicsPath gp = new GraphicsPath();
            Matrix myMatrix = new Matrix();
            gp.AddRectangle(new Rectangle(X, Y, Width, Height));
            if (this.Rotation != 0)
            {
                myMatrix.RotateAt(this.Rotation, new PointF(this.X + this.Width / 2.0F, 
                    this.Y + this.Height / 2.0F),
                    MatrixOrder.Append);
            }
            gp.Transform(myMatrix);
            Rectangle gpRect = Rectangle.Round(gp.GetBounds());
            return rect.Contains(gpRect);
        }

        #region Constructors
        protected GraphicObject()
        {
        }

        protected GraphicObject(MyPoint graphicPosition) : this()
        {
            this.SetPosition(graphicPosition);
        }

        protected GraphicObject(int posX, int posY) : this(new MyPoint(posX, posY))
        {
        }

        protected GraphicObject(MyPoint graphicPosition, Size graphicSize) : this(graphicPosition)
        {
            this.SetSize(graphicSize);
            this.AutoSize = false;
        }

        protected GraphicObject(int posX, int posY, Size graphicSize) : this(new MyPoint(posX, posY), graphicSize)
        {
        }

        protected GraphicObject(int posX, int posY, int width, int height) : this(new MyPoint(posX, posY), new Size(width, height))
        {
        }

        protected GraphicObject(MyPoint graphicPosition, float Rotation) : this()
        {
            this.SetPosition(graphicPosition);
            this.Rotation = (int)Rotation;
        }

        protected GraphicObject(int posX, int posY, float Rotation) : this(new MyPoint(posX, posY), Rotation)
        {
        }

        protected GraphicObject(MyPoint graphicPosition, Size graphicSize, float Rotation) : this(graphicPosition, Rotation)
        {
            this.SetSize(graphicSize);
            this.AutoSize = false;
        }

        protected GraphicObject(int posX, int posY, Size graphicSize, float Rotation) : this(new MyPoint(posX, posY), graphicSize, Rotation)
        {
        }

        protected GraphicObject(int posX, int posY, int width, int height, float Rotation) : this(new MyPoint(posX, posY), new Size(width, height), Rotation)
        {
        }
        #endregion

        public virtual MyPoint GetPosition()
        {
            MyPoint myPosition = new MyPoint(X, Y);
            return myPosition;
        }

        public virtual void SetPosition(MyPoint Value)
        {
            X = (int)Value.X;
            Y = (int)Value.Y;
        }

        public virtual void SetSize(Size Value)
        {
            Width = Value.Width;
            Height = Value.Height;
        }

        public virtual Size GetSize()
        {
            Size mySize = new Size(Width, Height);
            return mySize;
        }

        public virtual int Rotation
        {
            get
            {
                return (int)m_Rotation;
                if (IsRunningOnMono())
                {
                    return 0;
                }
                //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
                return 0;
            }
            set
            {
                if (Math.Abs(value) <= 360)
                {
                    m_Rotation = value;
                    if (IsRunningOnMono())
                    {
                        m_Rotation = 0;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Rotation", "Rotation must be between -360.0 and 360.0");
                }
            }
        }

        public virtual void Draw(Graphics g)
        {
            //PositionConnectors();
            //RotateConnectors();
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

        /*public virtual void PositionConnectors()
        {
            //To be implemented in derived classes.
        }*/

        /*
        public void RotateConnectors()
        {
            Point center = new Point(X + this.Width / 2.0, Y + this.Height / 2.0);
            Point pt = new Point();
            double raio = 0;
            double angulo = 0;
            //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#:
            //			ConnectionPoint con = null;
            foreach (ConnectionPoint con in this.InputConnectors)
            {
                pt = con.Position;
                raio = Math.Pow(Math.Pow(pt.X - center.X, 2) + Math.Pow(pt.Y - center.Y, 2), 0.5);
                angulo = Math.Atan2(pt.Y - center.Y, pt.X - center.X);
                pt.X = Convert.ToInt32(center.X + raio * Math.Cos(angulo + this.Rotation / 360 * 2 * Math.PI));
                pt.Y = Convert.ToInt32(center.Y + raio * Math.Sin(angulo + this.Rotation / 360 * 2 * Math.PI));
                con.Position = pt;
            }
            foreach (ConnectionPoint con in this.OutputConnectors)
            {
                pt = con.Position;
                raio = Math.Pow(Math.Pow(pt.X - center.X, 2) + Math.Pow(pt.Y - center.Y, 2), 0.5);
                angulo = Math.Atan2(pt.Y - center.Y, pt.X - center.X);
                pt.X = Convert.ToInt32(center.X + raio * Math.Cos(angulo + this.Rotation / 360 * 2 * Math.PI));
                pt.Y = Convert.ToInt32(center.Y + raio * Math.Sin(angulo + this.Rotation / 360 * 2 * Math.PI));
                con.Position = pt;
            }
            pt = this.EnergyConnector.Position;
            raio = Math.Pow(Math.Pow(pt.X - center.X, 2) + Math.Pow(pt.Y - center.Y, 2), 0.5);
            angulo = Math.Atan2(pt.Y - center.Y, pt.X - center.X);
            pt.X = Convert.ToInt32(center.X + raio * Math.Cos(angulo + this.Rotation / 360 * 2 * Math.PI));
            pt.Y = Convert.ToInt32(center.Y + raio * Math.Sin(angulo + this.Rotation / 360 * 2 * Math.PI));
            this.EnergyConnector.Position = pt;

        }

        public virtual void CreateConnectors(int InCount, int OutCount)
        {
            //Creates all the connection points.
            for (int I = 1; I <= InCount; I++)
            {
                ConnectionPoint Con = new ConnectionPoint();
                Con.Type = ConType.ConIn;
                InputConnectors.Add(Con);
            }

            for (int I = 1; I <= OutCount; I++)
            {
                ConnectionPoint Con = new ConnectionPoint();
                Con.Type = ConType.ConOut;
                OutputConnectors.Add(Con);
            }
        }

        public virtual bool LoadData(List<XElement> data)
        {

            XMLSerializer.XMLSerializer.Deserialize(this, data);

            //DWSIM Mobile compatibility

            if (ObjectType == Enums.GraphicObjects.ObjectType.CompressorExpander)
            {
                ObjectType = Enums.GraphicObjects.ObjectType.Compressor;
            }
            if (ObjectType == Enums.GraphicObjects.ObjectType.HeaterCooler)
            {
                ObjectType = Enums.GraphicObjects.ObjectType.Heater;
            }

            //Other Properties

            Globalization.CultureInfo ci = Globalization.CultureInfo.InvariantCulture;
            XElement xel = (
                from XElement xel2 in data
                where xel2.Name == "AdditionalInfo"
                select xel2).SingleOrDefault();

            if (xel != null)
            {
                ArrayList arr = new ArrayList();
                foreach (XElement xel2 in xel.Elements)
                {
                    arr.Add(XMLSerializer.XMLSerializer.StringToArray2(xel2.Value, ci, Type.GetType("System.Double")));
                }
                this.AdditionalInfo = arr.ToArray(Type.GetType("System.Object"));
            }
            return true;

        }

        public virtual List<XElement> SaveData()
        {
            List<XElement> elements = XmlSerializer.Serialize(this);
            CultureInfo ci = CultureInfo.InvariantCulture;


            if (this.AdditionalInfo != null)
            {
                elements.Add(new XElement("AdditionalInfo", new XElement("Info", XMLSerializer.XMLSerializer.ArrayToString2(this.AdditionalInfo(0), ci)), new XElement("Info", XMLSerializer.XMLSerializer.ArrayToString2(this.AdditionalInfo(1), ci)), new XElement("Info", XMLSerializer.XMLSerializer.ArrayToString2(this.AdditionalInfo(2), ci)), new XElement("Info", XMLSerializer.XMLSerializer.ArrayToString2(this.AdditionalInfo(3), ci))));
            }

            elements.Add(new XElement("InputConnectors"));

            foreach (ConnectionPoint cp in InputConnectors)
            {
                if ((cp.IsAttached & cp.AttachedConnector != null) != 0)
                {
                    elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", cp.IsAttached.ToString(ci)), new XAttribute("ConnType", cp.Type.ToString()), new XAttribute("AttachedFromObjID", cp.AttachedConnector.AttachedFrom.Name.ToString()), new XAttribute("AttachedFromConnIndex", cp.AttachedConnector.AttachedFromConnectorIndex), new XAttribute("AttachedFromEnergyConn", cp.AttachedConnector.AttachedFromEnergy.ToString())));
                }
                else
                {
                    elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", cp.IsAttached.ToString(ci))));
                }
            }

            elements.Add(new XElement("OutputConnectors"));

            foreach (ConnectionPoint cp in OutputConnectors)
            {
                if ((cp.IsAttached & cp.AttachedConnector != null) != 0)
                {
                    elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", cp.IsAttached.ToString(ci)), new XAttribute("ConnType", cp.Type.ToString()), new XAttribute("AttachedToObjID", cp.AttachedConnector.AttachedTo.Name.ToString()), new XAttribute("AttachedToConnIndex", cp.AttachedConnector.AttachedToConnectorIndex), new XAttribute("AttachedToEnergyConn", cp.AttachedConnector.AttachedToEnergy.ToString())));
                }
                else
                {
                    elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", cp.IsAttached.ToString(ci))));
                }
            }

            elements.Add(new XElement("EnergyConnector"));

            if (EnergyConnector.IsAttached)
            {
                elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", EnergyConnector.IsAttached.ToString(ci)), new XAttribute("AttachedToObjID", EnergyConnector.AttachedConnector.AttachedTo.Name.ToString()), new XAttribute("AttachedToConnIndex", EnergyConnector.AttachedConnector.AttachedToConnectorIndex), new XAttribute("AttachedToEnergyConn", EnergyConnector.AttachedConnector.AttachedToEnergy.ToString())), new XAttribute("AttachedFromObjID", EnergyConnector.AttachedConnector.AttachedFrom.Name.ToString()), new XAttribute("AttachedFromConnIndex", EnergyConnector.AttachedConnector.AttachedFromConnectorIndex), new XAttribute("AttachedFromEnergyConn", EnergyConnector.AttachedConnector.AttachedFromEnergy.ToString()));
            }
            else
            {
                elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", EnergyConnector.IsAttached.ToString(ci))));
            }

            elements.Add(new XElement("SpecialConnectors"));

            foreach (ConnectionPoint cp in SpecialConnectors)
            {
                if (cp.IsAttached)
                {
                    elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", cp.IsAttached.ToString(ci)), new XAttribute("AttachedToObjID", cp.AttachedConnector.AttachedTo.Name.ToString()), new XAttribute("AttachedToConnIndex", cp.AttachedConnector.AttachedToConnectorIndex), new XAttribute("AttachedToEnergyConn", cp.AttachedConnector.AttachedToEnergy.ToString())), new XAttribute("AttachedFromObjID", cp.AttachedConnector.AttachedFrom.Name.ToString()), new XAttribute("AttachedFromConnIndex", cp.AttachedConnector.AttachedFromConnectorIndex), new XAttribute("AttachedFromEnergyConn", cp.AttachedConnector.AttachedFromEnergy.ToString()));
                }
                else
                {
                    elements[elements.Count - 1].Add(new XElement("Connector", new XAttribute("IsAttached", cp.IsAttached.ToString(ci))));
                }
            }


            return elements;

        }*/

        public bool Active { get; set; } = true;
        public object AdditionalInfo { get; set; } = null;
        public string Description { get; set; } = "";
        public bool FlippedH { get; set; } = false;
        public bool FlippedV { get; set; } = false;
        //public bool IsEnergyStream { get; set; } = false;
        public ObjectType ObjectType { get; set; } = ObjectType.Rectangle;
        //public int Shape { get; set; } = 0;
        //public ShapeIcon ShapeOverride { get; set; } = ShapeIcon.DefaultShape;
        //public Status Status { get; set; } = Status.NotCalculated;
        public bool AutoSize { get; set; } = false;       
        //public bool IsConnector { get; set; } = false;
        public string Name { get; set; } = "";
        public string Tag { get; set; } = "";
        public int Width { get; set; } = 20;
        public int Height { get; set; } = 20;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public IPoint Position { get; set; } = new MyPoint();
        public bool Selected { get; set; } = false;

        /*
       // public IConnectionPoint EnergyConnector { get; set; } = new ConnectionPoint();
        public List<IConnectionPoint> InputConnectors { get; set; } = new List<IConnectionPoint>();
        public List<IConnectionPoint> OutputConnectors { get; set; } = new List<IConnectionPoint>();
        public List<IConnectionPoint> SpecialConnectors { get; set; } = new List<IConnectionPoint>();*/
        
        /*
        public IGraphicObject Clone()
        {
            GraphicObject instance = (GraphicObject)Activator.CreateInstance(this.GetType());
            //instance.LoadData(this.SaveData);
            return instance;
        }

        void IGraphicObject.Draw(object surface)
        {
            this.Draw1(surface);
        }
        public void Draw1(object surface)
        {
            Draw((System.Drawing.Graphics)surface);
        }*/
        
        /*[XmlIgnore]
        public ISimulationObject Owner { get; set; }

        [XmlIgnore, NonSerialized]
        private System.Windows.Forms.Form _editorform;

        public object Editor
        {
            get
            {
                return _editorform;
            }
            set
            {
                _editorform = (System.Windows.Forms.Form)value;
            }
        }

        bool IGraphicObject.HitTest(object zoomedSelection)
        {
            return this.HitTest1(zoomedSelection);
        }
        public bool HitTest1(object zoomedSelection)
        {
            return HitTest((System.Drawing.Point)zoomedSelection);
        }*/

        //public Action<object> DrawOverride { get; set; }
        //public Dictionary<string, IGraphicObjectExtension> Extensions { get; set; } = new Dictionary<string, IGraphicObjectExtension>();


    }
}
